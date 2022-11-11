using Microsoft.ApplicationInsights.Channel;
using System.Collections.Concurrent;

namespace Paradigm.WindowsAppSDK.Services.Tests.Telemetry
{
    internal class TestableTelemetryChannel : ITelemetryChannel
    {
        public ConcurrentBag<ITelemetry> SentTelemetries = new ConcurrentBag<ITelemetry>();

        public TestableTelemetryChannel()
        {

        }

        public bool? DeveloperMode { get; set; }

        public string? EndpointAddress { get; set; }

        public void Dispose()
        {
            
        }

        public void Flush()
        {
            
        }

        public void Send(ITelemetry item)
        {
            SentTelemetries.Add(item);
        }
    }

}
