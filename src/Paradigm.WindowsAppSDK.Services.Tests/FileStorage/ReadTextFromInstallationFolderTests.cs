namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadTextFromInstallationFolderTests : FileStorageTestsBase
    {
        [TestCase]
        public void ShouldReadContentFromFile()
        {
            //arrange
            var path = "test.txt";

            //act
            var content = this.Sut.ReadTextFromInstallationFolder(path);

            //assert
            Assert.That(content, Is.Not.Empty);
        }

        [TestCase]
        public async Task ShouldReadContentFromFileAsync()
        {
            //arrange
            var path = "test.txt";

            //act
            var content = await this.Sut.ReadTextFromInstallationFolderAsync(path);

            //assert
            Assert.That(content, Is.Not.Empty);
        }
    }

}
