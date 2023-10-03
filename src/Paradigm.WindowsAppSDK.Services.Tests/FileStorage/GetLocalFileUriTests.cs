using Paradigm.WindowsAppSDK.Services.FileStorage;

namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class GetLocalFileUriTests : FileStorageTestsBase
    {
        [TestCase("local-state-test.txt", true)]
        [TestCase("local-state-test.txt", false)]
        public void ShouldReturnUri(string path, bool useInstallationFolder)
        {
            //Arrange
            var settings = new FileStorageSettings
            {
                LocalBaseUri = "ms-appdata:///local",
                InstallationBaseUri = "ms-appx:///Assets"
            };

            this.Sut.Initialize(settings);

            var expectedResult = $"{(useInstallationFolder ? settings.InstallationBaseUri : settings.LocalBaseUri)}/{path}";

            //Act
            var result = this.Sut.GetLocalFileUri(path, useInstallationFolder: useInstallationFolder);

            //assert
            Assert.IsNotNull(result);
            Assert.That(result.AbsoluteUri, Is.EqualTo(expectedResult));
        }

        [TestCase("local-state-test.txt", true, false)]
        [TestCase("local-state-test.txt", false, false)]
        [TestCase("", true, true)]
        [TestCase("", false, false)]
        [TestCase(null, true, true)]
        [TestCase(null, false, false)]
        public void ShouldValidateEmptyPath(string? path, bool validateEmptyPath, bool expectedEmpty)
        {
            //Arrange
            var settings = new FileStorageSettings
            {
                LocalBaseUri = "ms-appdata:///local",
                InstallationBaseUri = "ms-appx:///Assets"
            };

            this.Sut.Initialize(settings);

            //Act
            var result = this.Sut.GetLocalFileUri(path, validateEmptyPath, false);

            //assert
            Assert.That(result, expectedEmpty ? Is.Null : Is.Not.Null);
        }
    }
}