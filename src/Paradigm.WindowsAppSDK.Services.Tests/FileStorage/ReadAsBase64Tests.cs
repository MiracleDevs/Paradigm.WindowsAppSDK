namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadAsBase64Tests : FileStorageTestsBase
    {
        [Test]
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

        [Test]
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

        [Test]
        public void ShouldReturnEmtptyBase64StringIfFileDoesNotExists()
        {
            //arrange
            var path = "not-found-local-state-test.txt";

            //act
            var content = this.Sut.ReadAsBase64(path);

            //assert
            Assert.That(content, Is.Null);
        }

        [Test]
        public async Task ShouldReturnEmtptyBase64StringIfFileDoesNotExistsAsync()
        {
            //arrange
            var path = "not-found-local-state-test.txt";

            //act
            var content = await this.Sut.ReadAsBase64Async(path);

            //assert
            Assert.That(content, Is.Null);
        }
    }
}
