using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Telemetry
{
    public interface ITelemetryService : IService
    {
        /// <summary>
        /// Initializes the service using the provided settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void Initialize(TelemetrySettings settings);

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        void TrackEvent(string eventName, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        void TrackEvent(string connectionString, string eventName, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void TrackException(Exception ex);

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="ex">The ex.</param>
        void TrackException(string connectionString, Exception ex);

        /// <summary>
        /// Adds the extra property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        void AddExtraProperty(string name, string value);
    }
}