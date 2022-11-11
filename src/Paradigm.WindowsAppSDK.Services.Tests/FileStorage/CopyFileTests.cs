using System.IO;

namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class CopyFileTests : FileStorageTestsBase
    {
        private static string FileName => "local-state-test.txt";
        private static string NewFileName  => $"{Path.GetFileNameWithoutExtension(FileName)}-copy{Path.GetExtension(FileName)}";

        [TearDown]
        public void TearDown()
        {
            var newFilePath = Path.Combine(this.LocalFolderPath, NewFileName);

            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
        }

        [TestCase]
        public void ShouldCopyTextFile()
        {
            //arrange

            //act
            var result = this.Sut.CopyFile(FileName, NewFileName);

            //assert
            Assert.That(result, Contains.Substring(NewFileName));
            Assert.That(result, Contains.Substring(LocalFolderPath));
        }
    }
}