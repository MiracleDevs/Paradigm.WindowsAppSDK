﻿using Microsoft.ApplicationInsights.DataContracts;
using NUnit.Framework;
using Paradigm.WindowsAppSDK.Services.Telemetry;
using System.Diagnostics.Tracing;

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
            Assert.IsNotNull(TestService.Settings);
            Assert.That(TestService.Settings.ConnectionString, Is.EqualTo(TestConnectionString));
            Assert.IsTrue(TestService.Settings.DebounceEnabled);
            Assert.IsTrue(TestService.Settings.RenamePropertiesEnabled);
            Assert.IsNull(TestService.Settings.AllowedCustomProps);
            Assert.IsNotNull(TestService.ExtraProperties);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(0));
        }

        [TestCase(false, false, null)]
        [TestCase(true, true, new[] { "prop1", "prop2" })]
        public void ShouldInitializeServiceCustomSettings(bool debounceEnabled, bool renamePropertiesEnabled, string[]? allowedCustomProps)
        {
            //act
            TestService.Initialize(new TelemetrySettings(TestConnectionString, debounceEnabled, renamePropertiesEnabled, allowedCustomProps));

            //assert
            Assert.IsNotNull(TestService.Settings);
            Assert.That(TestService.Settings.ConnectionString, Is.EqualTo(TestConnectionString));
            Assert.That(TestService.Settings.DebounceEnabled, Is.EqualTo(debounceEnabled));
            Assert.That(TestService.Settings.RenamePropertiesEnabled, Is.EqualTo(renamePropertiesEnabled));
            Assert.That(TestService.Settings.AllowedCustomProps, Is.EqualTo(allowedCustomProps));
            new ArgumentNullException(nameof(TestService.TelemetryChannel));
            Assert.IsNotNull(TestService.ExtraProperties);
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
            Assert.IsNotNull(TestService.ExtraProperties);
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
            Assert.IsNotNull(TestService.ExtraProperties);
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

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");

            //act
            TestService.AddExtraPropertiesTo(properties);

            //assert
            Assert.IsNotNull(TestService.ExtraProperties);
            Assert.That(TestService.ExtraProperties.Count, Is.EqualTo(3));
            Assert.That(properties.Count, Is.EqualTo(5));
        }

        [Test]
        public void ShouldReplaceWithExtraProperties()
        {
            //arrange
            TestService.AddExtraProperty("extraProp1", "propvalue1");
            TestService.AddExtraProperty("extraProp2", "propvalue2");

            var properties = new Dictionary<string, string>();
            properties.Add("extraProp1", "tempvalue1");
            properties.Add("extraProp2", "tempvalue2");
            properties.Add("prop1", "1");

            //act
            TestService.AddExtraPropertiesTo(properties);

            //assert
            Assert.IsNotNull(TestService.ExtraProperties);
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

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");

            //act
            TestService.RenameProps(properties);

            //assert
            Assert.IsNotNull(TestService.ExtraProperties);
            Assert.That(properties.Count, Is.EqualTo(2));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("value1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("value2"));
        }

        [Test]
        public void ShouldNotRenameAllowedCustomProperties()
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(TestConnectionString, true, true, new[] { "prop1", "prop2" }));

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");
            properties.Add("prop3", "3");

            //act
            TestService.RenameProps(properties);

            //assert
            Assert.IsNotNull(TestService.ExtraProperties);
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

            var properties = new Dictionary<string, string>();
            properties.Add("prop1", "1");
            properties.Add("prop2", "2");

            //act
            TestService.RenameProps(properties);

            //assert
            Assert.IsNotNull(TestService.ExtraProperties);
            Assert.That(properties.Count, Is.EqualTo(2));
            Assert.That(properties.ElementAt(0).Key, Is.EqualTo("prop1"));
            Assert.That(properties.ElementAt(1).Key, Is.EqualTo("prop2"));
        }

        [TestCase(true, 600)]
        [TestCase(false, 500)]
        public async Task ShouldTrackEvent(bool debounce, int delay)
        {
            //arrange
            var eventCount = 10;
            var expectedCount = debounce ? 1 : eventCount;

            TestService.Initialize(new TelemetrySettings(TestConnectionString, debounce, false, null));
            
            var eventName = "test-event";

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            var events = Enumerable.Repeat(eventName, eventCount).ToList();

            //act
            events.ForEach(e => TestService.TrackEvent(eventName, properties));

            //Assert
            await Task.Delay(delay);

            Assert.That(((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries, Has.Count.EqualTo(expectedCount));
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
            Enumerable.Repeat(eventName,count).ToList().ForEach(e => TestService.TrackEvent(e, properties));

            await Task.Delay(1000);
            var sentTelemetries = ((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries;
            
            var telemetry = sentTelemetries
                .ToList()
                .ConvertAll(t => t as EventTelemetry)
                .FirstOrDefault(t => t !=null && t.Properties.Any(p => p.Key =="count"));
            
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
            foreach(var e in events)
            {
                await Task.Delay(2000);
                TestService.TrackEvent(e, properties);
            }
            
            await Task.Delay(1000);
            var sentTelemetries = ((TestableTelemetryChannel)TestService.TelemetryChannel).SentTelemetries;

            var telemetryWithDebounceCount = sentTelemetries
                .ToList()
                .ConvertAll(t => t as EventTelemetry)
                .Any(t => t != null && t.Properties.Any(p => p.Key == "count"));

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

        [TestCase(TestConnectionString)]
        [TestCase(AlternateTestConnectionString)]
        public void ShouldTrackEventWithConnectionString(string connectionString)
        {
            //arrange
            TestService.Initialize(new TelemetrySettings(string.Empty, false, false, null));

            var properties = new Dictionary<string, string>
            {
                { "prop1", "1" },
                { "prop2", "2" }
            };

            //act
            TestService.TrackEvent(connectionString, "test-event", properties);

            //Assert
            Assert.That(TestService.ClientConnectionString, Is.EqualTo(connectionString));
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
            Assert.That(TestService.ClientConnectionString, Is.EqualTo(connectionString));
        }
    }
}