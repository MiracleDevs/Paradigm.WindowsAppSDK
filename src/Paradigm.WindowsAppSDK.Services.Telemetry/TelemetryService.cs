﻿using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Paradigm.WindowsAppSDK.Services.Telemetry.Extensions;

namespace Paradigm.WindowsAppSDK.Services.Telemetry
{
    public class TelemetryService : ITelemetryService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        protected TelemetrySettings? Settings { get; private set; }

        /// <summary>
        /// Gets or sets the telemetries client.
        /// </summary>
        /// <value>
        /// The telemetries client.
        /// </value>
        protected virtual TelemetryClient? TelemetriesClient { get; set; }

        /// <summary>
        /// Gets the extra properties.
        /// </summary>
        /// <value>
        /// The extra properties.
        /// </value>
        protected IDictionary<string, string> ExtraProperties { get; }

        /// <summary>
        /// Gets the timers dictionary.
        /// </summary>
        /// <value>
        /// The timers dictionary.
        /// </value>
        private Dictionary<string, (System.Timers.Timer, int)> TimersDictionary { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryService"/> class.
        /// </summary>
        public TelemetryService()
        {
            ExtraProperties = new Dictionary<string, string>();
            TimersDictionary = new Dictionary<string, (System.Timers.Timer, int)>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the service using the provided settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Initialize(TelemetrySettings settings)
        {
            if (TelemetriesClient is not null)
                return;

            Settings = settings;
            InitializeClient();
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        public void TrackEvent(string eventName, IDictionary<string, string> properties)
        {
            if (Settings is null || TelemetriesClient is null)
                throw new InvalidOperationException("Telemetry was not initialized");

            if (properties is null)
                properties = new Dictionary<string, string>();

            if (Settings.DebounceEnabled)
            {
                Debounce(eventName, () => Task.Run(() =>
                {
                    RenameProps(properties);
                    AddExtraPropertiesTo(properties);
                    AddDebounceCount(eventName, properties);
                    TelemetriesClient.TrackEvent(eventName, properties);
                    TelemetriesClient.Flush();
                }), 500);
            }
            else
            {
                Task.Run(() =>
                {
                    var props = properties.CloneDictionary();
                    RenameProps(props);
                    AddExtraPropertiesTo(props);
                    TelemetriesClient.TrackEvent(eventName, props);
                    TelemetriesClient.Flush();
                });
            }
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        public void TrackEvent(string connectionString, string eventName, IDictionary<string, string> properties)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            ResetClient(connectionString);
            TrackEvent(eventName, properties);
        }

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void TrackException(Exception ex)
        {
            if (Settings is null || TelemetriesClient is null)
                throw new InvalidOperationException("Telemetry was not initialized");

            if (Settings.DebounceEnabled)
            {
                Debounce("exception", () => Task.Run(() =>
                {
                    var properties = new Dictionary<string, string>();
                    AddExtraPropertiesTo(properties);
                    AddDebounceCount("exception", properties);
                    TelemetriesClient.TrackException(ex, properties);
                    TelemetriesClient.Flush();
                }), 500);
            }
            else
            {
                Task.Run(() =>
                {
                    var properties = new Dictionary<string, string>();
                    AddExtraPropertiesTo(properties);
                    TelemetriesClient.TrackException(ex, properties);
                    TelemetriesClient.Flush();
                });
            }
        }

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="ex">The ex.</param>
        public void TrackException(string connectionString, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            ResetClient(connectionString);
            TrackException(ex);
        }

        /// <summary>
        /// Adds the extra property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddExtraProperty(string name, string value)
        {
            if (ExtraProperties.ContainsKey(name)) ExtraProperties.Remove(name);
            ExtraProperties.Add(name, value);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adds the extra properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected void AddExtraPropertiesTo(IDictionary<string, string> properties)
        {
            if (ExtraProperties.Count == 0)
                return;

            foreach (var item in ExtraProperties)
            {
                if (properties.ContainsKey(item.Key)) properties.Remove(item.Key);
                properties.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Renames the props.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected void RenameProps(IDictionary<string, string> properties)
        {
            if (Settings is null || !Settings.RenamePropertiesEnabled)
                return;

            var i = 0;
            IDictionary<string, string> retProperties = new Dictionary<string, string>();

            if (properties.Count <= 0)
                return;

            foreach (var prop in properties)
            {
                retProperties.Add(prop);
            }

            foreach (var prop in retProperties)
            {
                if (Settings.AllowedCustomProps?.Contains(prop.Key, StringComparer.OrdinalIgnoreCase) ?? false)
                    continue;

                i++;
                properties.Remove(prop);

                properties.Add($"value{i}", prop.Value);
            }
        }

        /// <summary>
        /// Creates the telemetry configuration.
        /// </summary>
        /// <returns></returns>
        protected virtual TelemetryConfiguration CreateTelemetryConfiguration(string connectionString)
        {
            var configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString = connectionString;
            return configuration;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the client.
        /// </summary>
        private void InitializeClient()
        {
            if (string.IsNullOrWhiteSpace(Settings?.ConnectionString))
                return;

            var configuration = CreateTelemetryConfiguration(Settings.ConnectionString);
            TelemetriesClient = new TelemetryClient(configuration);
        }

        /// <summary>
        /// Resets the client.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private void ResetClient(string connectionString)
        {
            if (connectionString.Equals(TelemetriesClient?.TelemetryConfiguration?.ConnectionString))
                return;

            var configuration = CreateTelemetryConfiguration(connectionString);
            TelemetriesClient = new TelemetryClient(configuration);
        }

        /// <summary>
        /// Debounces the specified event name.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="action">The action.</param>
        /// <param name="intervalMilliseconds">The interval milliseconds.</param>
        private void Debounce(string eventName, Action action, double intervalMilliseconds)
        {
            if (!TimersDictionary.ContainsKey(eventName))
            {
                var timer = new System.Timers.Timer(intervalMilliseconds);
                timer.AutoReset = false;
                timer.Elapsed += (s, e) =>
                {
                    action();
                    timer.Dispose();
                };

                TimersDictionary.Add(eventName, (timer, 0));
            }

            var debounceTimer = TimersDictionary[eventName];
            debounceTimer.Item2++;
            TimersDictionary[eventName] = debounceTimer;

            if (debounceTimer.Item1.Enabled)
                debounceTimer.Item1.Stop();

            debounceTimer.Item1.Start();
        }

        /// <summary>
        /// Adds the debounce count.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        private void AddDebounceCount(string eventName, IDictionary<string, string> properties)
        {
            var debounceTimer = TimersDictionary[eventName];
            if (debounceTimer.Item2 > 1)
            {
                properties.Add("count", debounceTimer.Item2.ToString());
                debounceTimer.Item2 = 0;
            }
            debounceTimer.Item1.Dispose();
            TimersDictionary.Remove(eventName);
        }

        #endregion
    }
}