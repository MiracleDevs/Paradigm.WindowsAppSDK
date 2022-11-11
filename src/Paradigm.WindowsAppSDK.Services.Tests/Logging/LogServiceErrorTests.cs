using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public class LogServiceErrorTests : LogServiceLogTestBase
    {
        public LogServiceErrorTests()
        {
        }

        protected override string LogPrefix => "ERROR";

        protected override LogTypes LogType => LogTypes.Error;

        protected override void Log(string message) => this.Sut.Error(message);

        [Test]
        public override void ShoulNotLogMessage()
        {
            //arrange
            string message = $"This is a test message from {this.GetType()}.";

            //act
            this.Sut.SetMinimumLogType(this.LogType);

            Log(message);

            var exists = File.Exists(Path.Combine(LogFolderPath, LogFileName));

            //assert
            Assert.That(exists, Is.True);
        }

        [Test]
        public void ShoulLogMessageWithException()
        {
            //arrange
            string message = $"This is an exception message from {this.GetType()}.";

            var exception = new Exception(message);

            //act
            this.Sut.SetMinimumLogType(this.LogType);

            this.Sut.Error(exception);

            var exists = File.Exists(Path.Combine(LogFolderPath, LogFileName));

            var content = File.ReadAllText(Path.Combine(LogFolderPath, LogFileName));

            //assert
            Assert.That(exists, Is.True);
            Assert.That(content.Contains(message));
            Assert.That(content.Contains(this.LogPrefix));
        }
    }
}
