using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Paradigm.WindowsAppSDK.Services.Telemetry;

namespace Paradigm.WindowsAppSDK.Services.Tests.Telemetry
{
    internal class TestableTelemetryService : TelemetryService
    {
        public new TelemetrySettings? Settings => base.Settings;

        public string? ClientConnectionString => base.DefaultTelemetryClient?.TelemetryConfiguration?.ConnectionString;

        public List<string> AdditionalConnectionStrings => base.AdditionalClientsDictionary.Keys.ToList();

        public new IDictionary<string, string> ExtraProperties => base.ExtraProperties;

        public new void AddExtraPropertiesTo(IDictionary<string, string> properties)
        {
            base.AddExtraPropertiesTo(properties);
        }

        public new void RenameProps(IDictionary<string, string> properties)
        {
            base.RenameProps(properties);
        }

        public ITelemetryChannel TelemetryChannel { get; private set; }

        public TestableTelemetryService() : base()
        {
            this.TelemetryChannel = new TestableTelemetryChannel();
        }

        protected override TelemetryConfiguration CreateTelemetryConfiguration(string connectionString)
        {
            if (TelemetryChannel is not null) TelemetryChannel.Dispose();
            TelemetryChannel = new TestableTelemetryChannel();
            var configuration = base.CreateTelemetryConfiguration(connectionString);
            configuration.TelemetryChannel = TelemetryChannel;
            return configuration;
        }
    }
}