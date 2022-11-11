namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadBytesFromInstallationFolderTests : FileStorageTestsBase
    {
        [TestCase]
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

        [TestCase]
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
    }
}
