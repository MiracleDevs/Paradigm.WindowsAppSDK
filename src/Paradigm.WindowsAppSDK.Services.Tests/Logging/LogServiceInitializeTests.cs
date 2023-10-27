using Paradigm.WindowsAppSDK.Services.Logging;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public class LogServiceInitializeTests
    {

        protected ILogService Sut { get; private set; }

        protected virtual string LogFolderPath => Path.Combine(TestContext.CurrentContext.TestDirectory, "Logs");

        protected virtual string LogFileName => $"{GetType().FullName}.txt";

        protected virtual int? LogFileMaxSize => 10; //10kb

        public LogServiceInitializeTests()
        {

        }

        [SetUp]
        public virtual void Setup()
        {
            this.Sut = new LogService();

            if (!Directory.Exists(LogFolderPath))
            {
                Directory.CreateDirectory(LogFolderPath);
            }
        }

        [TearDown]
        public virtual void TearDown()
        {
            var path = Path.Combine(this.LogFolderPath, this.LogFileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if (Directory.Exists(LogFolderPath))
            {
                Directory.Delete(LogFolderPath, true);
            }
        }

        [Test]
        public virtual void ShoulThrowExceptionIfFolderIsNullOrWhiteSpace()
        {
            //arrange
            //act & assert
            Assert.Throws<ArgumentNullException>(() => Sut.Initialize(new LogSettings(string.Empty, this.LogFileMaxSize, LogFileName)));
        }

        [Test]
        public virtual void ShoulDeletePreviousLogFileIfExists()
        {
            //arrange
            var message = "This is a test message";
            var messages = Enumerable.Repeat(message, LogFileMaxSize.GetValueOrDefault() * 10).Select(msg => msg);
            var settings = new LogSettings(LogFolderPath, LogFileMaxSize, LogFileName);

            //act
            Sut.Initialize(settings);

            foreach (var item in messages)
            {
                Sut.Information(item);
            }

            Sut.Initialize(settings);

            var exists = File.Exists(Path.Combine(LogFolderPath, LogFileName));

            //assert
            Assert.That(exists, Is.False);
        }

        [Test]
        public virtual void ShoulArchivePreviousLogFileIfExists()
        {
            //arrange
            var message = "This is a test message";
            var messages = Enumerable.Repeat(message, LogFileMaxSize.GetValueOrDefault() * 10).Select(msg => msg);
            var settings = new LogSettings(LogFolderPath, LogFileMaxSize, LogFileName, true);

            //act
            Sut.Initialize(settings);

            foreach (var item in messages)
            {
                Sut.Information(item);
            }

            Sut.Initialize(settings);

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(LogFileName);
            var existingLogFilesCount = Directory.GetFiles(LogFolderPath, $"{fileNameWithoutExtension}*").Length;
            var archivedFileName = $"{fileNameWithoutExtension}_{existingLogFilesCount}.txt";
            var originalFileExists = File.Exists(Path.Combine(LogFolderPath, LogFileName));
            var archivedFileExists = File.Exists(Path.Combine(LogFolderPath, archivedFileName));

            //assert
            Assert.That(originalFileExists, Is.False);
            Assert.That(archivedFileExists, Is.True);
        }
    }
}