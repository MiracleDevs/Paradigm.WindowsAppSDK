using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public class LogServiceInfoTests : LogServiceLogTestBase
    {
        public LogServiceInfoTests()
        {
        }

        protected override string LogPrefix => "INFO";

        protected override LogTypes LogType => LogTypes.Info;

        protected override void Log(string message) => this.Sut.Information(message);
    }
}
