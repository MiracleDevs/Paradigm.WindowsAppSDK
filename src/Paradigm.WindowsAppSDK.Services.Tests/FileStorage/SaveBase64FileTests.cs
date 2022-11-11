namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class SaveBase64FileTests : FileStorageTestsBase
    {
        [TestCase]
        public void ShouldSaveBase64File()
        {
            //arrange
            var fileName = "local-state-test.txt";
            var base64FileName = $"base64-{fileName}";

            //act
            var content = this.Sut.ReadAsBase64(fileName);

            var base64FilePath = this.Sut.SaveBase64File(base64FileName, content);

            //assert
            Assert.That(base64FilePath, Contains.Substring(LocalFolderPath));
            Assert.That(base64FilePath, Contains.Substring(base64FileName));
        }

        [TestCase]
        public async Task ShouldSaveBase64FileAsync()
        {
            //arrange
            var fileName = "local-state-test.txt";
            var base64FileName = $"base64-{fileName}";

            //act
            var content = await this.Sut.ReadAsBase64Async(fileName);
            var base64FilePath = await this.Sut.SaveBase64FileAsync(base64FileName, content);

            //assert
            Assert.That(base64FilePath, Contains.Substring(this.LocalFolderPath));
            Assert.That(base64FilePath, Contains.Substring(base64FileName));
        }
    }
}
