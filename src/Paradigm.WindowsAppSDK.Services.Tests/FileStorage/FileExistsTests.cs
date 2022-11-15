using Paradigm.WindowsAppSDK.Services.FileStorage;

namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class FileExistsTests : FileStorageTestsBase
    {
        [TestCase("local-state-test.txt", true)]
        [TestCase("not-found.txt", false)]
        public void ShouldExistTextFileInitializeWithLocalState(string path, bool expected)
        {
            //act
            this.Sut.Initialize(new FileStorageSettings
            {
                LocalFolderPath = this.LocalFolderPath,
                InstallationFolderPath = this.InstallationFolderPath
            });

            //assert
            Assert.That(this.Sut.FileExists(path), Is.EqualTo(expected));
        }

        [Test]
        public void ShouldExistTextFileInitializeWithRootedPathInLocalState()
        {
            //arrange
            var path = Path.Combine(this.LocalFolderPath, "local-state-test.txt");

            //act
            this.Sut.Initialize(new FileStorageSettings
            {
                LocalFolderPath = this.LocalFolderPath,
                InstallationFolderPath = this.InstallationFolderPath
            });

            //assert
            Assert.That(this.Sut.FileExists(path), Is.EqualTo(true));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionIfLocalFolderPathIsUnspecified()
        {
            //arrange
            var path = "local-state-test.txt";

            //act
            this.Sut.Initialize(new FileStorageSettings
            {
                LocalFolderPath = string.Empty,
                InstallationFolderPath = this.InstallationFolderPath
            });

            //assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.FileExists(path));
        }
    }
}