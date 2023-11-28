using NUnit.Framework.Constraints;
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
                Paragraph = "paragraph text",
                Items = new List<LocalizableChildModel>(),
                AllItems = new Dictionary<string, LocalizableChildModel>()
            };
            localizableModel.Items.Add(new LocalizableChildModel() { Id = "test", Description = "test", Name = "test" });
            localizableModel.AllItems.Add("test", new LocalizableChildModel() { Id = "test", Description = "test", Name = "test" });

            //act
            var result = TestService.ExtractLocalizableStrings(localizableModel, TestPrefix);


            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result.ElementAt(0).Key, Does.StartWith(TestPrefix));
            Assert.That(result.ElementAt(0).Key, Is.EqualTo($"{TestPrefix}.{nameof(LocalizableModel.Heading)}"));
            Assert.That(result.ElementAt(0).Value, Is.EqualTo(localizableModel.Heading));
            Assert.That(result.ElementAt(1).Key, Does.StartWith(TestPrefix));
            Assert.That(result.ElementAt(1).Key, Is.EqualTo($"{TestPrefix}.{nameof(LocalizableModel.Paragraph)}"));
            Assert.That(result.ElementAt(1).Value, Is.EqualTo(localizableModel.Paragraph));

            Assert.That(result.ElementAt(2).Key, Does.StartWith($"{TestPrefix}.{nameof(LocalizableModel.Items)}.0"));
            Assert.That(result.ElementAt(2).Value, Is.Not.Null);
            Assert.That(result.ElementAt(2).Value, Is.EqualTo(localizableModel.Items.First().Name));
            
            Assert.That(result.ElementAt(4).Key, Does.StartWith($"{TestPrefix}.{nameof(LocalizableModel.AllItems)}.test"));
            Assert.That(result.ElementAt(4).Value, Is.Not.Null);
            Assert.That(result.ElementAt(4).Value, Is.EqualTo(localizableModel.AllItems.First().Value.Name));
        }

        [Test]
        public void ShouldApplyLocalizableStrings()
        {
            //arrange
            var localizableModel = new LocalizableModel
            {
                Id = "test",
                Heading = "heading text",
                Paragraph = "paragraph text",
                Items = new List<LocalizableChildModel>(),
                AllItems = new Dictionary<string, LocalizableChildModel>()
            };
            localizableModel.Items.Add(new LocalizableChildModel() { Id = "test", Description = "test", Name = "test" });
            localizableModel.AllItems.Add("test", new LocalizableChildModel() { Id = "test", Description = "test", Name = "test" });


            var modelTranslations = new Dictionary<string, string>();
            modelTranslations.Add($"{TestPrefix}.{nameof(LocalizableModel.Heading)}", "localized heading text");
            modelTranslations.Add($"{TestPrefix}.{nameof(LocalizableModel.Paragraph)}", "localized paragraph text");

            modelTranslations.Add($"{TestPrefix}.{nameof(LocalizableModel.Items)}.0.{nameof(LocalizableChildModel.Name)}", "localized first child name");
            modelTranslations.Add($"{TestPrefix}.{nameof(LocalizableModel.AllItems)}.test.{nameof(LocalizableChildModel.Name)}", "localized first child name");

            //act
            var result = TestService.ApplyLocalizableStrings(localizableModel, modelTranslations, TestPrefix);

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(localizableModel.Id));
            Assert.That(result.Heading, Is.EqualTo(modelTranslations[$"{TestPrefix}.{nameof(LocalizableModel.Heading)}"]));
            Assert.That(result.Paragraph, Is.EqualTo(modelTranslations[$"{TestPrefix}.{nameof(LocalizableModel.Paragraph)}"]));
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Items.First().Name, Is.EqualTo(modelTranslations[$"{TestPrefix}.{nameof(LocalizableModel.Items)}.0.{nameof(LocalizableChildModel.Name)}"]));
            Assert.That(result.AllItems, Is.Not.Empty);
            Assert.That(result.AllItems, Is.Not.Null);
            Assert.That(result.AllItems.First().Value.Name, Is.EqualTo(modelTranslations[$"{TestPrefix}.{nameof(LocalizableModel.AllItems)}.test.{nameof(LocalizableChildModel.Name)}"]));
        }
    }
}