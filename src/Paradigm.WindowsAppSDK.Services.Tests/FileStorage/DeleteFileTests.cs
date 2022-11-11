namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class DeleteFileTests : FileStorageTestsBase
    {
        [TestCase]
        public void ShouldDeleteFile()
        {
            //arrange
            var fileName = "local-state-test.txt";
            var newFileName = $"new-{fileName}";
            
            //act
            this.Sut.CopyFile(fileName, newFileName);
            
            this.Sut.DeleteFile(newFileName);
            
            var exists = this.Sut.FileExists(newFileName);

            //assert
            Assert.That(exists, Is.False);
        }
    }
}