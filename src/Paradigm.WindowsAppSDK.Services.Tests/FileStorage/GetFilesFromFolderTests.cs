using Paradigm.WindowsAppSDK.Services.FileStorage;

namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class GetFilesFromFolderTests : FileStorageTestsBase
    {
        [Test]
        public void ShouldReadFilesFromInstallationFolder()
        {
            //arrange
            var path = string.Empty;
            var filePath = "test.txt";

            //act
            var files = this.Sut.GetFilesFromFolder(path, true);

            //assert
            Assert.That(files, Is.Not.Empty);
            Assert.That(files.First(), Is.EqualTo(filePath));
        }

        [Test]
        public void ShouldReadFilesFromLocalFolder()
        {
            //arrange
            var path = string.Empty;
            var filePath = "local-state-test.txt";

            //act
            var files = this.Sut.GetFilesFromFolder(path, false);

            //assert
            Assert.That(files, Is.Not.Empty);
            Assert.That(files.Any(f => f == filePath), Is.True);
        }

        [Test]
        public void ShouldThrowExceptionIfLocalFolderPathIsNull()
        {
            //arrange
            var path = string.Empty;

            //act
            this.Sut.Initialize(new FileStorageSettings
            {
                LocalFolderPath = string.Empty,
                InstallationFolderPath = this.InstallationFolderPath
            });

            //assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.GetFilesFromFolder(path, false));
        }

        [Test]
        public void ShouldThrowExceptionIfInstallationFolderPathIsNull()
        {
            //arrange
            var path = string.Empty;

            //act
            this.Sut.Initialize(new FileStorageSettings
            {
                LocalFolderPath = this.LocalFolderPath,
                InstallationFolderPath = string.Empty
            });

            //assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.GetFilesFromFolder(path, true));
        }
    }
}