﻿namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class GetFilesFromFolderTests : FileStorageTestsBase
    {
        [TestCase]
        public void ShouldReadFilesFromInstallationFolder()
        {
            //arrange
            var path = string.Empty;
            var filePath = "test.txt";
            
            //act
            var files = this.Sut.GetFilesFromFolder(path, true);

            //assert
            Assert.That(files, Is.Not.Empty);
            Assert.That(files.First(), Is.EqualTo(filePath));
        }

        [TestCase]
        public void ShouldReadFilesFromLocalFolder()
        {
            //arrange
            var path = string.Empty;
            var filePath = "local-state-test.txt";
            
            //act
            var files = this.Sut.GetFilesFromFolder(path, false);

            //assert
            Assert.That(files, Is.Not.Empty);
            Assert.That(files.Any(f => f == filePath), Is.True);
        }

        [TestCase]
        public void ShouldThrowExceptionIfLocalFolderPathIsNull()
        {
            //arrange
            var path = string.Empty;

            //act
            this.Sut.Initialize(localFolderPath: string.Empty, this.InstallationFolderPath);

            //assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.GetFilesFromFolder(path, false));
        }

        [TestCase]
        public void ShouldThrowExceptionIfInstallationFolderPathIsNull()
        {
            //arrange
            var path = string.Empty;

            //act
            this.Sut.Initialize(this.LocalFolderPath, installationFolderPath: string.Empty);

            //assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.GetFilesFromFolder(path, true));
        }
    }
}