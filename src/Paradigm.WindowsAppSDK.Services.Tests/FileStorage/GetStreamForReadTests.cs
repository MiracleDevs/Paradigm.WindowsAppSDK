namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class GetStreamForReadTests : FileStorageTestsBase
    {
        [TestCase]
        public void GetStreamForRead()
        {
            //arrange
            var fileName = "local-state-test.txt";

            //act
            using var stream = this.Sut.GetStreamForRead(fileName);
            
            //assert
            Assert.That(stream, Is.Not.Null);
            Assert.That(stream.Length, Is.GreaterThan(0));
        }

        [TestCase]
        public void GetEmptyStreamForReadWhenFileIsNotFound()
        {
            //arrange
            var fileName = "not-found-local-state-test.txt";

            //act
            using var stream = this.Sut.GetStreamForRead(fileName);

            //assert
            Assert.That(stream, Is.Null);
        }

    }
}
