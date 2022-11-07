using Newtonsoft.Json.Linq;
using Paradigm.WindowsAppSDK.Services.Telemetry;

namespace Paradigm.WindowsAppSDK.Services.Tests.Telemetry
{
    public class TelemetryServiceTest
    {
        private const string TestConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://eastus2-3.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus2.livediagnostics.monitor.azure.com/";

        [Test]
        public void ShouldInitializeServiceWithDefaultSettings()
        {
            //arrange
            var service = new TestableTelemetryService();

            //act
            service.Initialize(new TelemetrySettings(TestConnectionString));

            //assert
            Assert.IsNotNull(service.Settings);
            Assert.That(service.Settings.ConnectionString, Is.EqualTo(TestConnectionString));
            Assert.IsTrue(service.Settings.DebounceEnabled);
            Assert.IsTrue(service.Settings.RenamePropertiesEnabled);
            Assert.IsNull(service.Settings.AllowedCustomProps);
            Assert.IsNotNull(service.TelemetriesClient);
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(service.ExtraProperties.Count, Is.EqualTo(0));
        }

        [TestCase(false, false, null)]
        [TestCase(true, true, new[] { "prop1", "prop2" })]
        public void ShouldInitializeServiceCustomSettings(bool debounceEnabled, bool renamePropertiesEnabled, string[]? allowedCustomProps)
        {
            //arrange
            var service = new TestableTelemetryService();

            //act
            service.Initialize(new TelemetrySettings(TestConnectionString, debounceEnabled, renamePropertiesEnabled, allowedCustomProps));

            //assert
            Assert.IsNotNull(service.Settings);
            Assert.That(service.Settings.ConnectionString, Is.EqualTo(TestConnectionString));
            Assert.That(service.Settings.DebounceEnabled, Is.EqualTo(debounceEnabled));
            Assert.That(service.Settings.RenamePropertiesEnabled, Is.EqualTo(renamePropertiesEnabled));
            Assert.That(service.Settings.AllowedCustomProps, Is.EqualTo(allowedCustomProps));
            Assert.IsNotNull(service.TelemetriesClient);
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(service.ExtraProperties.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldAddExtraProperty()
        {
            //arrange
            const string key = "propKey";
            const string value = "propValue";
            var service = new TestableTelemetryService();

            //act
            service.AddExtraProperty(key, value);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(service.ExtraProperties.Count, Is.EqualTo(1));
            Assert.That(service.ExtraProperties[key], Is.EqualTo(value));
        }

        [Test]
        public void ShouldReplaceExtraProperty()
        {
            //arrange
            const string key = "propKey";
            const string value = "propValue";
            const string newValue = "propValueNew";
            var service = new TestableTelemetryService();

            //act
            service.AddExtraProperty(key, value);
            service.AddExtraProperty(key, newValue);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(service.ExtraProperties.Count, Is.EqualTo(1));
            Assert.That(service.ExtraProperties[key], Is.EqualTo(newValue));
        }

        [Test]
        public void ShouldAddExtraProperties()
        {
            //arrange
            var service = new TestableTelemetryService();
            service.AddExtraProperty("extraProp1", "propvalue1");
            service.AddExtraProperty("extraProp2", "propvalue2");
            service.AddExtraProperty("extraProp3", "propvalue3");

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");

            //act
            service.AddExtraPropertiesTo(properties);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(service.ExtraProperties.Count, Is.EqualTo(3));
            Assert.That(properties.Count, Is.EqualTo(5));
        }

        [Test]
        public void ShouldReplaceWithExtraProperties()
        {
            //arrange
            var service = new TestableTelemetryService();
            service.AddExtraProperty("extraProp1", "propvalue1");
            service.AddExtraProperty("extraProp2", "propvalue2");

            var properties = new Dictionary<string, string>();
            properties.Add("extraProp1", "tempvalue1");
            properties.Add("extraProp2", "tempvalue2");
            properties.Add("prop1", "1");

            //act
            service.AddExtraPropertiesTo(properties);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(service.ExtraProperties.Count, Is.EqualTo(2));
            Assert.That(properties.Count, Is.EqualTo(3));
            Assert.That(properties["extraProp1"], Is.EqualTo("propvalue1"));
            Assert.That(properties["extraProp2"], Is.EqualTo("propvalue2"));
        }

        [Test]
        public void ShouldRenameProperties()
        {
            //arrange
            var service = new TestableTelemetryService();
            service.Initialize(new TelemetrySettings(TestConnectionString));

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");

            //act
            service.RenameProps(properties);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(properties.Count, Is.EqualTo(2));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("value1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("value2"));
        }

        [Test]
        public void ShouldNotRenameAllowedCustomProperties()
        {
            //arrange
            var service = new TestableTelemetryService();
            service.Initialize(new TelemetrySettings(TestConnectionString, true, true, new[] { "prop1", "prop2" }));

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");
            properties.Add("prop3", "3");

            //act
            service.RenameProps(properties);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(properties.Count, Is.EqualTo(3));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("prop1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("prop2"));
            Assert.That(properties.ElementAt(2).Key, Is.EqualTo("value1"));
        }

        [Test]
        public void ShouldNotRenameProperties()
        {
            //arrange
            var service = new TestableTelemetryService();
            service.Initialize(new TelemetrySettings(TestConnectionString, true, false, null));

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");

            //act
            service.RenameProps(properties);

            //assert
            Assert.IsNotNull(service.ExtraProperties);
            Assert.That(properties.Count, Is.EqualTo(2));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("prop1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("prop2"));
        }
    }
}