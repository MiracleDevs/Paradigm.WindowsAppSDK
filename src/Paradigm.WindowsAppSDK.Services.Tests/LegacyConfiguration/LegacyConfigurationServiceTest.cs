using Paradigm.WindowsAppSDK.Services.LegacyConfiguration;

namespace Paradigm.WindowsAppSDK.Services.Tests.LegacyConfiguration
{
    public class LegacyConfigurationServiceTest
    {
        private string ConfigurationFileContent { get; set; }

        [SetUp]
        public void Setup()
        {
            ConfigurationFileContent = File.ReadAllText(".\\LegacyConfiguration\\test.json");
        }

        [TestCase("stringKey", "test")]
        [TestCase("invalidKey", null)]
        public void ShouldReturnStringValue(string key, string? expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetString(key);

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("booleanKey", true)]
        [TestCase("invalidKey", null)]
        public void ShouldReturnBooleanValue(string key, bool? expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetBoolean(key);

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("doubleKey", 100.5)]
        [TestCase("invalidKey", null)]
        public void ShouldReturnDoubleValue(string key, double? expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetDouble(key);

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("objectKey", "value2")]
        [TestCase("invalidKey", null)]
        public void ShouldReturnObjectValue(string key, string? expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetObject<ObjectValueModel>(key);

            //assert
            Assert.That(result?.Prop2, Is.EqualTo(expected));
        }

        [TestCase("objectKey", DisplayConfigEnum.Percentage)]
        public void ShouldReturnEnumValue(string key, DisplayConfigEnum expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetObject<ObjectValueModel>(key);

            //assert
            Assert.That(result?.Prop3, Is.EqualTo(expected));
        }

        [TestCase("arrayKey", 0, DisplayConfigEnum.Percentage)]
        [TestCase("arrayKey", 2, DisplayConfigEnum.Pixels)]
        public void ShouldReturnEnumValueFromArray(string key, int index, DisplayConfigEnum expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetObject<List<ObjectValueModel>>(key);

            //assert
            Assert.That(result?[index].Prop3, Is.EqualTo(expected));
        }

        [TestCase("arrayKey", 0, "two")]
        [TestCase("arrayKey", 2, "yellow")]
        public void ShouldReturnListValueFromArray(string key, int index, string expected)
        {
            //arrange
            var service = new LegacyConfigurationService();
            service.Initialize(ConfigurationFileContent);

            //act
            var result = service.GetObject<List<ObjectValueModel>>(key);

            //assert
            Assert.That(result?[index].Prop4?[1], Is.EqualTo(expected));
        }
    }
}