namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class SaveFileTests : FileStorageTestsBase
    {
        [Test]
        public async Task ShouldSaveTextFile()
        {
            //arrange
            var content = $"Text content from {this.GetType().FullName}";

            var path = "local-state-test.txt";

            //act
            this.Sut.SaveFile(path, content);

            var fileContent = await this.Sut.ReadLocalTextAsync(path);

            //assert
            Assert.That(fileContent, Is.EqualTo(content));
        }

        [Test]
        public async Task ShouldSaveTextFileAsync()
        {
            //arrange
            var content = $"Text content from {this.GetType().FullName}";

            var path = "local-state-test.txt";

            //act
            await this.Sut.SaveFileAsync(path, content);

            var fileContent = await this.Sut.ReadLocalTextAsync(path);

            //assert
            Assert.That(fileContent, Is.EqualTo(content));
        }
    }
}