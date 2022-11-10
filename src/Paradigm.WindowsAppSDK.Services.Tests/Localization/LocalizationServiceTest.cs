using Paradigm.WindowsAppSDK.Services.Localization;

namespace Paradigm.WindowsAppSDK.Services.Tests.Localization
{
    public class LocalizationServiceTest
    {
        private const string TestPrefix = "testPrefix";

        private ILocalizationService TestService { get; set; }

        [SetUp]
        public void Setup()
        {
            TestService = new LocalizationService();
        }

        [Test]
        public void ShouldExtractLocalizableStrings()
        {
            //arrange
            var localizableModel = new LocalizableModel
            {
                Id = "test",
                Heading = "heading text",
                Paragraph = "paragraph text"
            };

            //act
            var result = TestService.ExtractLocalizableStrings(localizableModel, TestPrefix);

            //assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Key, Does.StartWith(TestPrefix));
            Assert.That(result.ElementAt(0).Key, Is.EqualTo($"{TestPrefix}.{nameof(LocalizableModel.Heading)}"));
            Assert.That(result.ElementAt(0).Value, Is.EqualTo(localizableModel.Heading));
            Assert.That(result.ElementAt(1).Key, Does.StartWith(TestPrefix));
            Assert.That(result.ElementAt(1).Key, Is.EqualTo($"{TestPrefix}.{nameof(LocalizableModel.Paragraph)}"));
            Assert.That(result.ElementAt(1).Value, Is.EqualTo(localizableModel.Paragraph));
        }

        [Test]
        public void ShouldApplyLocalizableStrings()
        {
            //arrange
            var localizableModel = new LocalizableModel
            {
                Id = "test",
                Heading = "heading text",
                Paragraph = "paragraph text"
            };

            var modelTranslations = new Dictionary<string, string>();
            modelTranslations.Add($"{TestPrefix}.{nameof(LocalizableModel.Heading)}", "localized heading text");
            modelTranslations.Add($"{TestPrefix}.{nameof(LocalizableModel.Paragraph)}", "localized paragraph text");

            //act
            var result = TestService.ApplyLocalizableStrings(localizableModel, modelTranslations, TestPrefix);

            //assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(localizableModel.Id));
            Assert.That(result.Heading, Is.EqualTo(modelTranslations[$"{TestPrefix}.{nameof(LocalizableModel.Heading)}"]));
            Assert.That(result.Paragraph, Is.EqualTo(modelTranslations[$"{TestPrefix}.{nameof(LocalizableModel.Paragraph)}"]));
        }
    }
}