namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadLocalTextTests : FileStorageTestsBase
    {
        [Test]
        public void ShouldReadLocalTextFromFile()
        {
            //arrange
            var path = "local-state-test.txt";

            //act
            var content = this.Sut.ReadLocalText(path);

            //assert
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);
        }

        [Test]
        public async Task ShouldReadLocalTextFromFileAsync()
        {
            //arrange
            var path = "local-state-test.txt";

            //act
            var content = await this.Sut.ReadLocalTextAsync(path);

            //assert
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);
        }

        [Test]
        public void ShouldThrowExceptionIfLocalFolderPathIsNull()
        {
            //arrange
            var path = "local-state-test.txt";

            //act && assert
            this.Sut.Initialize(localFolderPath: string.Empty, this.InstallationFolderPath);
            Assert.Throws<ArgumentNullException>(() => Sut.ReadLocalText(path));
        }

        [Test]
        public void ShouldThrowExceptionIfLocalFolderPathIsNullAsync()
        {
            //arrange
            var path = "local-state-test.txt";

            //act && assert
            this.Sut.Initialize(localFolderPath: string.Empty, InstallationFolderPath);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await Sut.ReadLocalTextAsync(path));
        }
    }
}
