using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Tests.Logging
{
    public class LogServiceTraceTests : LogServiceLogTestBase
    {
        public LogServiceTraceTests()
        {
        }

        protected override string LogPrefix => "TRACE";

        protected override LogTypes LogType => LogTypes.Trace;

        protected override void Log(string message) => this.Sut.Trace(message);
    }
}
