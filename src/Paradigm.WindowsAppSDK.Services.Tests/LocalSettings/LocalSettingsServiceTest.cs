using Paradigm.WindowsAppSDK.Services.LocalSettings;

namespace Paradigm.WindowsAppSDK.Services.Tests.LocalSettings
{
    public class LocalSettingsServiceTest
    {
        private ILocalSettingsService TestService { get; set; }

        [SetUp]
        public void Setup()
        {
            TestService = new LocalSettingsService();
            TestService.Initialize(new Dictionary<string, object>());
        }

        [Test]
        public void ShouldStoreAndRetrieveObject()
        {
            //act
            TestService.StoreSettings(new SettingsModel
            {
                StringValue = "test",
                NumericValue = 5,
                BooleanValue = true
            });

            var result = TestService.GetStoredSettings<SettingsModel>();

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StringValue, Is.EqualTo("test"));
            Assert.That(result.NumericValue, Is.EqualTo(5));
            Assert.That(result.BooleanValue, Is.True);
        }

        [Test]
        public void ShouldStoreAndRetrievePrimitiveValue()
        {
            //act
            TestService.StoreSettings("test");
            var result = TestService.GetStoredSettings<string>();

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("test"));
        }

        [Test]
        public void ShouldReturnNull()
        {
            //act
            var result = TestService.GetStoredSettings<object>();

            //assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ShouldReplaceStoredValue()
        {
            //act
            TestService.StoreSettings(new SettingsModel
            {
                StringValue = "test1",
                NumericValue = 1,
                BooleanValue = true
            });

            var result1 = TestService.GetStoredSettings<SettingsModel>();

            TestService.StoreSettings(new SettingsModel
            {
                StringValue = "test2",
                NumericValue = 2,
                BooleanValue = false
            });

            var result2 = TestService.GetStoredSettings<SettingsModel>();

            //assert
            Assert.That(result1, Is.Not.Null);
            Assert.That(result1.StringValue, Is.EqualTo("test1"));
            Assert.That(result1.NumericValue, Is.EqualTo(1));
            Assert.That(result1.BooleanValue, Is.True);

            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.StringValue, Is.EqualTo("test2"));
            Assert.That(result2.NumericValue, Is.EqualTo(2));
            Assert.That(result2.BooleanValue, Is.False);
        }

        [Test]
        public void ShouldClearStoredValue()
        {
            //act
            TestService.StoreSettings("test");
            TestService.ResetToDefaultSettings();
            var result = TestService.GetStoredSettings<string>();

            //assert
            Assert.That(result, Is.Null);
        }
    }
}
