using Paradigm.WindowsAppSDK.Services.Configuration;

namespace Paradigm.WindowsAppSDK.Services.Tests.Configuration
{
    public class ConfigurationServiceTest
    {
        private string ConfigurationFileContent { get; set; }

        [SetUp]
        public void Setup()
        {
            ConfigurationFileContent = File.ReadAllText(".\\Configuration\\test.json");
        }

        [TestCase("stringKey", "test")]
        [TestCase("invalidKey", null)]
        public void ShouldReturnStringValue(string key, string? expected)
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

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
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

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
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

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
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

            //act
            var result = service.GetObject(key, ObjectValueModelJsonContext.Default.ObjectValueModel);

            //assert
            Assert.That(result?.Prop2, Is.EqualTo(expected));
        }

        [TestCase("objectKey", DisplayConfigEnum.Percentage)]
        public void ShouldReturnEnumValue(string key, DisplayConfigEnum expected)
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

            //act
            var result = service.GetObject(key, ObjectValueModelJsonContext.Default.ObjectValueModel);

            //assert
            Assert.That(result?.Prop3, Is.EqualTo(expected));
        }

        [TestCase("arrayKey", 0, DisplayConfigEnum.Percentage)]
        [TestCase("arrayKey", 2, DisplayConfigEnum.Pixels)]
        public void ShouldReturnEnumValueFromArray(string key, int index, DisplayConfigEnum expected)
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

            //act
            var result = service.GetObject(key, ListObjectValueModelJsonContext.Default.ListObjectValueModel);

            //assert
            Assert.That(result?[index].Prop3, Is.EqualTo(expected));
        }

        [TestCase("arrayKey", 0, "two")]
        [TestCase("arrayKey", 2, "yellow")]
        public void ShouldReturnListValueFromArray(string key, int index, string expected)
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

            //act
            var result = service.GetObject(key, ListObjectValueModelJsonContext.Default.ListObjectValueModel);

            //assert
            Assert.That(result?[index].Prop4?[1], Is.EqualTo(expected));
        }

        [TestCase()]
        public void ShouldLoadContentFromMultipleFiles()
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);
            service.AddConfigurationContent(File.ReadAllText(".\\Configuration\\test2.json"));

            //act
            var stringValue1 = service.GetString("stringKey");
            var stringValue2 = service.GetString("stringKey2");
            var booleanValue1 = service.GetBoolean("booleanKey");
            var booleanValue2 = service.GetBoolean("booleanKey2");

            //assert
            Assert.That(stringValue1, Is.EqualTo("test"));
            Assert.That(stringValue2, Is.EqualTo("test2"));
            Assert.That(booleanValue1, Is.EqualTo(true));
            Assert.That(booleanValue2, Is.EqualTo(false));
        }

        [Test]
        public void ShouldThrowExceptionWhenSerializedContentIsInvalid()
        {
            //arrange
            var service = new ConfigurationService();
            var serializedContent = "invalid content";

            //act & assert
            Assert.Throws<System.Text.Json.JsonException>(() => service.AddConfigurationContent(serializedContent));
        }


        [TestCase(true, "newValue")]
        [TestCase(false, "test")]
        public void ShouldOverwriteKeys(bool overwriteExistingKeys, string expected)
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent, overwriteExistingKeys);
            service.AddConfigurationContent("{\"stringKey\":\"newValue\"}", overwriteExistingKeys);

            //act
            var result = service.GetString("stringKey");

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("stringKey", "test")]
        [TestCase("STRINGKEY", "test")]
        [TestCase("stringkey", "test")]
        public void ShouldIgnoreCase(string key, string? expected)
        {
            //arrange
            var service = new ConfigurationService();
            service.AddConfigurationContent(ConfigurationFileContent);

            //act
            var result = service.GetString(key);

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}