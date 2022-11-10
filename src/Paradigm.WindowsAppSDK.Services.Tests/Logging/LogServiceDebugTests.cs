using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public class LogServiceDebugTests : LogServiceLogTestBase
    {
        public LogServiceDebugTests()
        {
        }

        protected override string LogPrefix => "DEBUG";

        protected override LogTypes LogType => LogTypes.Debug;

        protected override void Log(string message) => this.Sut.Debug(message);
    }
}
