using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Paradigm.WindowsAppSDK.Services.Telemetry;

namespace Paradigm.WindowsAppSDK.Services.Tests.Telemetry
{
    internal class TestableTelemetryService : TelemetryService
    {
        public new TelemetrySettings? Settings => base.Settings;

        public new IDictionary<string, string> ExtraProperties => base.ExtraProperties;

        public new void AddExtraPropertiesTo(IDictionary<string, string> properties)
        {
            base.AddExtraPropertiesTo(properties);
        }

        public new void RenameProps(IDictionary<string, string> properties)
        {
            base.RenameProps(properties);
        }

        public ITelemetryChannel TelemetryChannel { get; }

        public TestableTelemetryService(): base()
        {
            this.TelemetryChannel = new TestableTelemetryChannel();
        }

        protected override void InitializeClient()
        {
            if (Settings == null)
                return;

            var config = new TelemetryConfiguration()
            {
                TelemetryChannel = TelemetryChannel,
                ConnectionString = Settings.ConnectionString
            };
            
            TelemetriesClient = new TelemetryClient(config);
        }
    }
}