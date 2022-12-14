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
        public void ShouldReturnStringValue(string key, string expected)
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
    }
}
