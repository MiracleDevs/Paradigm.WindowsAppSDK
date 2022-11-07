using Microsoft.ApplicationInsights;
using Paradigm.WindowsAppSDK.Services.Telemetry;

namespace Paradigm.WindowsAppSDK.Services.Tests.Telemetry
{
    internal class TestableTelemetryService : TelemetryService
    {
        public new TelemetrySettings? Settings => base.Settings;

        public new TelemetryClient? TelemetriesClient => base.TelemetriesClient;

        public new IDictionary<string, string> ExtraProperties => base.ExtraProperties;

        public new void AddExtraPropertiesTo(IDictionary<string, string> properties)
        {
            base.AddExtraPropertiesTo(properties);
        }

        public new void RenameProps(IDictionary<string, string> properties)
        {
            base.RenameProps(properties);
        }
    }
}