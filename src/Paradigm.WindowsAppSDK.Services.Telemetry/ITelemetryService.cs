﻿using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Telemetry
{
    public interface ITelemetryService : IService
    {
        /// <summary>
        /// Gets the current session identifier.
        /// </summary>
        /// <value>
        /// The current session identifier.
        /// </value>
        string? CurrentSessionId { get; }

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
        /// <param name="preventDebounce">The prevent debounce.</param>
        void TrackEvent(string eventName, IDictionary<string, string> properties, bool preventDebounce = false);

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="preventDebounce">The prevent debounce.</param>
        void TrackEvent(string connectionString, string eventName, IDictionary<string, string> properties, bool preventDebounce = false);

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

        /// <summary>
        /// Sets the session identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        void SetSessionId(string? sessionId);
    }
}