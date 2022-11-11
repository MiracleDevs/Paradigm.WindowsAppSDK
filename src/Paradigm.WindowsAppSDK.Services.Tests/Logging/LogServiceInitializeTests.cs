using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.Logging.Enums;
using System.ComponentModel;

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

            if (Directory.Exists(LogFolderPath)) {
                Directory.Delete(LogFolderPath, true);
            }
        }

        [Test]
        public virtual void ShoulThrowExceptionIfFolderIsNullOrWhiteSpace()
        {
            //arrange
            //act & assert
            Assert.Throws<ArgumentNullException>(() => this.Sut.Initialize(LogTypes.Info, logFolderPath : null, this.LogFileMaxSize, LogFileName));
        }        

        [Test]
        public virtual void ShoulDeletePreviousLogFileIfExists()
        {
            //arrange
            var message = "This is a test message";

            var messages = Enumerable.Repeat(message, LogFileMaxSize.GetValueOrDefault()*10).Select(msg => msg);

            //act
            this.Sut.Initialize(LogTypes.Info, LogFolderPath, this.LogFileMaxSize, LogFileName);

            foreach(var item in messages)
            {
                this.Sut.Information(item);
            }
            
            this.Sut.Initialize(LogTypes.Info, LogFolderPath, this.LogFileMaxSize, LogFileName);

            var exists = File.Exists(Path.Combine(LogFolderPath, LogFileName));

            //assert
            Assert.That(exists, Is.False);
        }
    }
}
