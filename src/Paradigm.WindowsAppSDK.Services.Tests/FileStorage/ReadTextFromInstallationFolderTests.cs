namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadTextFromInstallationFolderTests : FileStorageTestsBase
    {
        [Test]
        public void ShouldReadContentFromFile()
        {
            //arrange
            var path = "test.txt";

            //act
            var content = this.Sut.ReadTextFromInstallationFolder(path);

            //assert
            Assert.That(content, Is.Not.Empty);
        }

        [Test]
        public async Task ShouldReadContentFromFileAsync()
        {
            //arrange
            var path = "test.txt";

            //act
            var content = await this.Sut.ReadTextFromInstallationFolderAsync(path);

            //assert
            Assert.That(content, Is.Not.Empty);
        }

        [Test]
        public void ShouldThrowExceptionIInstallationFolderPathIsEmpty()
        {
            //arrange
            var path = "test.txt";

            this.Sut.Initialize(this.LocalFolderPath, installationFolderPath: string.Empty);

            //act && assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.ReadTextFromInstallationFolder(path));
        }

        [Test]
        public void ShouldThrowExceptionIInstallationFolderPathIsEmptyAsync()
        {
            //arrange
            var path = "test.txt";

            this.Sut.Initialize(this.LocalFolderPath, installationFolderPath: string.Empty);

            //act && assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await this.Sut.ReadTextFromInstallationFolderAsync(path));
        }
    }
}
