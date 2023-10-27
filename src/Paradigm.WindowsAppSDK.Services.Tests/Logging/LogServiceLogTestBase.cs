using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public abstract class LogServiceLogTestBase
    {
        protected abstract string LogPrefix { get; }

        protected ILogService Sut { get; private set; }

        protected virtual string LogFolderPath => Path.Combine(TestContext.CurrentContext.TestDirectory, "Logs");

        protected virtual string LogFileName => GetValidFileName($"{GetType().FullName}.txt");

        protected abstract LogTypes LogType { get; }

        protected virtual int? LogFileMaxSize => 1024 * 1024; //1Mb


        [SetUp]
        public virtual void Setup()
        {
            this.Sut = new LogService();

            if (!Directory.Exists(LogFolderPath))
                Directory.CreateDirectory(LogFolderPath);

            this.Sut.Initialize(new LogSettings(LogFolderPath, LogFileMaxSize, LogFileName));
            this.Sut.SetMinimumLogType(LogType);
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

        protected static string GetValidFileName(string fileName, char replaceChar = '_')
        {
            foreach (char c in Path.GetInvalidPathChars())
                fileName = fileName.Replace(c, replaceChar);

            return fileName;
        }

        [Test]
        public virtual void ShoulLogMessage()
        {
            //arrange
            string message = $"This is a test {this.LogPrefix.ToLower()} message from {this.GetType()}.";

            //act
            this.Sut.SetMinimumLogType(this.LogType);
            Log(message);

            var exists = File.Exists(Path.Combine(LogFolderPath, LogFileName));

            var content = File.ReadAllText(Path.Combine(LogFolderPath, LogFileName));

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(exists);
                Assert.That(content.Contains(message));
                Assert.That(content.Contains(this.LogPrefix));
            });
        }

        protected abstract void Log(string message);

        [Test]
        public virtual void ShoulNotLogMessage()
        {
            //arrange
            string message = $"This is a test message from {this.GetType()}.";

            //act
            this.Sut.SetMinimumLogType(this.LogType + 1);

            Log(message);

            var exists = File.Exists(Path.Combine(LogFolderPath, LogFileName));

            //assert
            Assert.That(exists, Is.False);
        }

        [Test]
        public virtual void ShouldThrowExceptionIfMessageIsNull()
        {
            //act && assert
            Assert.Throws<ArgumentNullException>(() => Log(message: null));
        }

        [Test]
        public virtual void ShouldThrowExceptionIfMessageIsEmpty()
        {
            //act && assert
            Assert.Throws<ArgumentNullException>(() => Log(message: string.Empty));
        }

        [Test]
        public virtual void ShouldThrowExceptionIfMessageIsWhiteSpace()
        {
            //act && assert
            Assert.Throws<ArgumentNullException>(() => Log(message: " "));
        }
    }
}