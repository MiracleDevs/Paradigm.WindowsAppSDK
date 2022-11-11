namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadAsBase64Tests : FileStorageTestsBase
    {
        [TestCase]
        public void ShouldReadAsBase64FromTextFile()
        {
            //arrange
            var path = "local-state-test.txt";

            //act
            var content = this.Sut.ReadAsBase64(path);

            //assert
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);
        }

        [TestCase]
        public async Task ShouldReadAsBase64FromTextFileAsync()
        {
            //arrange
            var path = "local-state-test.txt";

            //act
            var content = await this.Sut.ReadAsBase64Async(path);

            //assert
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);
        }
    }
}
