using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public class LogServiceWarningTests : LogServiceLogTestBase
    {
        public LogServiceWarningTests()
        {
        }

        protected override string LogPrefix => "WARN";

        protected override LogTypes LogType => LogTypes.Warning;

        protected override void Log(string message) => this.Sut.Warning(message);
    }
}
