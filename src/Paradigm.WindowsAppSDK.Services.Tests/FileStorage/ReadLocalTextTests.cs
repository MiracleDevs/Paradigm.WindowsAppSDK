﻿namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public class ReadLocalTextTests : FileStorageTestsBase
    {
        [TestCase]
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

        [TestCase]
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
    }
}
