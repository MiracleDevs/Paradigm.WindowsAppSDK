namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class FileExistsTests : FileStorageTestsBase
    {
        [TestCase("local-state-test.txt", true)]
        [TestCase("not-found.txt", false)]
        public void ShouldExistTextFileInitializeWithLocalState(string path, bool expected)
        {
            //arrange

            //act
            this.Sut.Initialize(this.LocalFolderPath, this.InstallationFolderPath);

            //assert
            Assert.That(this.Sut.FileExists(path), Is.EqualTo(expected));
        }

        [TestCase]
        public void ShouldExistTextFileInitializeWithRootedPathInLocalState()
        {
            //arrange
            var path = Path.Combine(this.LocalFolderPath, "local-state-test.txt");

            //act
            this.Sut.Initialize(this.LocalFolderPath, this.InstallationFolderPath);

            //assert
            Assert.That(this.Sut.FileExists(path), Is.EqualTo(true));
        }

        [TestCase]
        public void ShouldThrowArgumentNullExceptionIfLocalFolderPathIsUnspecified()
        {
            //arrange
            var path = "local-state-test.txt";

            //act
            this.Sut.Initialize(localFolderPath: null, this.InstallationFolderPath);

            //assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.FileExists(path));
        }
    }
}