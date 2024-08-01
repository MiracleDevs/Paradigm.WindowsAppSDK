using Microsoft.ApplicationInsights.DataContracts;
using Paradigm.WindowsAppSDK.Services.Telemetry;

namespace Paradigm.WindowsAppSDK.Services.Tests.Telemetry
{
    public class TelemetryServiceTest
    {
        private const string TestConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://eastus2-3.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus2.livediagnostics.monitor.azure.com/";
        private const string AlternateTestConnectionString = "InstrumentationKey=00000000-0000-0000-0000-111111111111;IngestionEndpoint=https://eastus2-3.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus2.livediagnostics.monitor.azure.com/";

        private TestableTelemetryService TestService { get; set; }

        [SetUp]
        public void Setup()
        {
            TestService = new TestableTelemetryService();
        }

        [Test]
        public void ShouldInitializeServiceWithDefaultSettings()
        {
            //act
            TestService.Initialize(new TelemetrySettings(TestConnectionString));

            //assert
            Assert.That(TestService.Settings, Is.Not.Null);
            Assert.That(TestService.Settings.ConnectionString, Is.EqualTo(TestConnectionString));
            Assert.That(TestService.Settings.DebounceEnabled, Is.True);
            Assert.That(TestService.Settings.RenamePropertiesEnabled, Is.True);
            Assert.That(TestService.Settings.AllowedCustomProps, Is.Null);
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(0));
        }

        [TestCase(false, false, null)]
        [TestCase(true, true, new[] { "prop1", "prop2" })]
        public void ShouldInitializeServiceCustomSettings(bool debounceEnabled, bool renamePropertiesEnabled, string[]? allowedCustomProps)
        {
            //act
            TestService.Initialize(new TelemetrySettings(TestConnectionString, debounceEnabled, renamePropertiesEnabled, allowedCustomProps));

            //assert
            Assert.That(TestService.Settings, Is.Not.Null);
            Assert.That(TestService.Settings.ConnectionString, Is.EqualTo(TestConnectionString));
            Assert.That(TestService.Settings.DebounceEnabled, Is.EqualTo(debounceEnabled));
            Assert.That(TestService.Settings.RenamePropertiesEnabled, Is.EqualTo(renamePropertiesEnabled));
            Assert.That(TestService.Settings.AllowedCustomProps, Is.EqualTo(allowedCustomProps));
            new ArgumentNullException(nameof(TestService.TelemetryChannel));
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldAddExtraProperty()
        {
            //arrange
            const string key = "propKey";
            const string value = "propValue";

            //act
            TestService.AddExtraProperty(key, value);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(1));
            Assert.That(TestService.ExtraProperties[key], Is.EqualTo(value));
        }

        [Test]
        public void ShouldReplaceExtraProperty()
        {
            //arrange
            const string key = "propKey";
            const string value = "propValue";
            const string newValue = "propValueNew";

            //act
            TestService.AddExtraProperty(key, value);
            TestService.AddExtraProperty(key, newValue);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(1));
            Assert.That(TestService.ExtraProperties[key], Is.EqualTo(newValue));
        }

        [Test]
        public void ShouldAddExtraProperties()
        {
            //arrange
            TestService.AddExtraProperty("extraProp1", "propvalue1");
            TestService.AddExtraProperty("extraProp2", "propvalue2");
            TestService.AddExtraProperty("extraProp3", "propvalue3");

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.AddExtraPropertiesTo(properties);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(3));
            Assert.That(properties.Count, Is.EqualTo(5));
        }

        [Test]
        public void ShouldReplaceWithExtraProperties()
        {
            //arrange
            TestService.AddExtraProperty("extraProp1", "propvalue1");
            TestService.AddExtraProperty("extraProp2", "propvalue2");

            var properties = new Dictionary<string, string>
            {
                { "extraProp1", "tempvalue1" },
                { "extraProp2", "tempvalue2" },
                { "prop1", "1" }
            };

            //act
            TestService.AddExtraPropertiesTo(properties);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(2));
            Assert.That(properties.Count, Is.EqualTo(3));
            Assert.That(properties["extraProp1"], Is.EqualTo("propvalue1"));
            Assert.That(properties["extraProp2"], Is.EqualTo("propvalue2"));
        }

        [Test]
        public void ShouldRenameProperties()
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(TestConnectionString));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.RenameProps(properties);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(properties.Count, Is.EqualTo(2));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("value1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("value2"));
        }

        [Test]
        public void ShouldNotRenameAllowedCustomProperties()
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(TestConnectionString, true, true, new[] { "prop1", "prop2" }));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" },
                { "prop3", "3" }
            };

            //act
            TestService.RenameProps(properties);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(properties.Count, Is.EqualTo(3));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("prop1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("prop2"));
            Assert.That(properties.ElementAt(2).Key, Is.EqualTo("value1"));
        }

        [Test]
        public void ShouldNotRenameProperties()
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(TestConnectionString, true, false, null));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.RenameProps(properties);

            //assert
            Assert.That(TestService.ExtraProperties, Is.Not.Null);
            Assert.That(properties.Count, Is.EqualTo(2));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("prop1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("prop2"));
        }

        [TestCase(true, 600, true)]
        [TestCase(true, 600, false)]
        [TestCase(false, 500, false)]
        public async Task ShouldTrackEvent(bool debounce, int delay, bool preventDebounce)
        {
            //arrange
            var eventCount = 10;
            var expectedCount = debounce && !preventDebounce ? 1 : eventCount;

            TestService.Initialize(new TelemetrySettings(TestConnectionString, debounce, true, null));

            var eventName = "test-event";

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            var events = Enumerable.Repeat(eventName, eventCount).ToList();

            //act
            events.ForEach(e => TestService.TrackEvent(eventName, properties, preventDebounce));

            //Assert
            await Task.Delay(delay);

            Assert.That(((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries, Has.Count.EqualTo(expectedCount));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task ShouldTrackEventWithoutProperties(bool debounce)
        {
            //arrange
            var eventName = "test-event";
            TestService.Initialize(new TelemetrySettings(TestConnectionString, debounce, true, null));

            //act
            TestService.TrackEvent(eventName, null);
            await Task.Delay(1000);

            //Assert
            Assert.That(((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task ShouldTrackEventsDebouncing()
        {
            //arrange
            var count = 5;
            var expectedCount = 1;
            var eventName = "test-event";

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            TestService.Initialize(new TelemetrySettings(TestConnectionString));

            //act
            Enumerable.Repeat(eventName, count).ToList().ForEach(e => TestService.TrackEvent(e, properties));

            await Task.Delay(1000);
            var sentTelemetries = ((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries;

            var telemetry = sentTelemetries
                .ToList()
                .ConvertAll(t => t as EventTelemetry)
                .FirstOrDefault(t => t != null && t.Properties.Any(p => p.Key == "count"));

            var countProperty = telemetry?.Properties.FirstOrDefault(p => p.Key == "count");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(sentTelemetries, Has.Count.EqualTo(expectedCount));
                Assert.That(countProperty, Is.Not.Null);
                Assert.That(countProperty?.Value, Is.EqualTo(count.ToString()));
            });
        }

        [Test]
        public async Task ShouldTrackSeparateEvents()
        {
            //arrange
            var count = 5;
            var expectedCount = 5;
            var eventName = "test-event";

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            TestService.Initialize(new TelemetrySettings(TestConnectionString));

            //act
            var events = Enumerable.Repeat(eventName, count).ToList();
            foreach (var e in events)
            {
                await Task.Delay(2000);
                TestService.TrackEvent(e, properties);
            }

            await Task.Delay(1000);
            var sentTelemetries = ((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries;

            var telemetryWithDebounceCount = sentTelemetries
                .ToList()
                .ConvertAll(t => t as EventTelemetry)
                .Any(t => t is not null && t.Properties.Any(p => p.Key == "count"));

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(sentTelemetries, Has.Count.EqualTo(expectedCount));
                Assert.That(telemetryWithDebounceCount, Is.False);
            });

        }

        [TestCase(true, 600)]
        [TestCase(false, 500)]
        public async Task ShouldTrackException(bool debounce, int delay)
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(TestConnectionString, debounceEnabled: debounce, false, null));
            ArgumentNullException ex = new(nameof(TestService.TelemetryChannel));

            //act
            TestService.TrackException(ex);

            //Assert
            await Task.Delay(delay);

            Assert.That(((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries, Has.Count.EqualTo(1));
        }

        [TestCase(TestConnectionString, null)]
        [TestCase(TestConnectionString, "sessionIdValue")]
        [TestCase(AlternateTestConnectionString, null)]
        [TestCase(AlternateTestConnectionString, "sessionIdValue")]
        public void ShouldTrackEventWithConnectionString(string connectionString, string? sessionId)
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(string.Empty, false, false, null));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.SetSessionId(sessionId);
            TestService.TrackEvent(connectionString, "test-event", properties);

            //Assert
            Assert.That(TestService.AdditionalConnectionStrings.Count, Is.EqualTo(1));
            Assert.That(TestService.AdditionalConnectionStrings[0], Is.EqualTo(connectionString));
            Assert.That(TestService.CurrentSessionId, Is.EqualTo(sessionId));
        }

        [Test]
        public void ShouldRegisterMultipleConnectionStrings()
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(string.Empty, false, false, null));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.TrackEvent(TestConnectionString, "test-event", properties);
            TestService.TrackEvent(AlternateTestConnectionString, "test-event2", properties);

            //Assert
            Assert.That(TestService.AdditionalConnectionStrings.Count, Is.EqualTo(2));
            Assert.That(TestService.AdditionalConnectionStrings[0], Is.EqualTo(TestConnectionString));
            Assert.That(TestService.AdditionalConnectionStrings[1], Is.EqualTo(AlternateTestConnectionString));
        }

        [Test]
        public void ShouldUseDefaultClient()
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(TestConnectionString, false, false, null));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.TrackEvent(TestConnectionString, "test-event", properties);
            TestService.TrackEvent(AlternateTestConnectionString, "test-event2", properties);

            //Assert
            Assert.That(TestService.AdditionalConnectionStrings.Count, Is.EqualTo(1));
            Assert.That(TestService.AdditionalConnectionStrings[0], Is.EqualTo(AlternateTestConnectionString));
        }

        [TestCase(TestConnectionString)]
        [TestCase(AlternateTestConnectionString)]
        public void ShouldTrackExceptionWithConnectionString(string connectionString)
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(string.Empty, false, false, null));
            ArgumentNullException ex = new(nameof(TestService.TelemetryChannel));

            //act
            TestService.TrackException(connectionString, ex);

            //Assert
            Assert.That(TestService.AdditionalConnectionStrings.Count, Is.EqualTo(1));
            Assert.That(TestService.AdditionalConnectionStrings[0], Is.EqualTo(connectionString));
        }
    }
}