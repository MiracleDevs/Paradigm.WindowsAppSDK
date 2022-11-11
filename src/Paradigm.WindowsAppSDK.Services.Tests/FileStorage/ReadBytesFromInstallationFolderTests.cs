namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadBytesFromInstallationFolderTests : FileStorageTestsBase
    {
        [Test]
        public void ShouldReadBytesFromFile()
        {
            //arrange
            var path = "test.txt";

            //act
            var content = this.Sut.ReadBytesFromInstallationFolder(path);

            //assert
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);
        }

        [Test]
        public async Task ShouldReadBytesFromFileAsync()
        {
            //arrange
            var path = "test.txt";

            //act
            var content = await this.Sut.ReadBytesFromInstallationFolderAsync(path);

            //assert
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);
        }

        [Test]
        public void ShouldThrowExceptionIInstallationFolderPathIsEmpty()
        {
            //arrange
            var path = "test.txt";

            this.Sut.Initialize(this.LocalFolderPath, installationFolderPath: string.Empty);

            //act && assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.ReadBytesFromInstallationFolder(path));
        }

        [Test]
        public void ShouldThrowExceptionIInstallationFolderPathIsEmptyAsync()
        {
            //arrange
            var path = "test.txt";

            this.Sut.Initialize(this.LocalFolderPath, installationFolderPath: string.Empty);

            //act && assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await this.Sut.ReadBytesFromInstallationFolderAsync(path));
        }
    }
}
