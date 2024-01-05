using Microsoft.ApplicationInsights;
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
        protected virtual TelemetryClient? DefaultTelemetryClient { get; set; }

        /// <summary>
        /// Gets the additional clients dictionary.
        /// </summary>
        /// <value>
        /// The additional clients dictionary.
        /// </value>
        protected Dictionary<string, TelemetryClient> AdditionalClientsDictionary { get; }

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
            AdditionalClientsDictionary = new Dictionary<string, TelemetryClient>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the service using the provided settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Initialize(TelemetrySettings settings)
        {
            if (DefaultTelemetryClient is not null)
                return;

            Settings = settings;
            InitializeClient();
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="preventDebounce">The prevent debounce.</param>
        /// <exception cref="InvalidOperationException">Telemetry was not initialized</exception>
        public void TrackEvent(string eventName, IDictionary<string, string> properties, bool preventDebounce = false)
        {
            if (DefaultTelemetryClient is null)
                throw new InvalidOperationException("Telemetry was not initialized");

            TrackEventInternal(DefaultTelemetryClient, eventName, properties, preventDebounce);
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="preventDebounce">The prevent debounce.</param>
        /// <exception cref="ArgumentNullException">connectionString</exception>
        public void TrackEvent(string connectionString, string eventName, IDictionary<string, string> properties, bool preventDebounce = false)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            TrackEventInternal(GetAdditionalClient(connectionString), eventName, properties, preventDebounce);
        }

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void TrackException(Exception ex)
        {
            if (DefaultTelemetryClient is null)
                throw new InvalidOperationException("Telemetry was not initialized");

            TrackExceptionInternal(DefaultTelemetryClient, ex);
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

            TrackExceptionInternal(GetAdditionalClient(connectionString), ex);
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
            DefaultTelemetryClient = new TelemetryClient(configuration);
        }

        /// <summary>
        /// Gets the additional client.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        private TelemetryClient GetAdditionalClient(string connectionString)
        {
            if (connectionString.Equals(DefaultTelemetryClient?.TelemetryConfiguration?.ConnectionString))
                return DefaultTelemetryClient;

            if (AdditionalClientsDictionary.ContainsKey(connectionString))
                return AdditionalClientsDictionary[connectionString];

            var configuration = CreateTelemetryConfiguration(connectionString);
            var client = new TelemetryClient(configuration);
            AdditionalClientsDictionary.Add(connectionString, client);
            return client;
        }

        /// <summary>
        /// Tracks the event internal.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="preventDebounce">if set to <c>true</c> [prevent debounce].</param>
        /// <exception cref="InvalidOperationException">Telemetry was not initialized</exception>
        private void TrackEventInternal(TelemetryClient client, string eventName, IDictionary<string, string> properties, bool preventDebounce = false)
        {
            if (Settings is null)
                throw new InvalidOperationException("Telemetry was not initialized");

            if (properties is null)
                properties = new Dictionary<string, string>();

            if (Settings.DebounceEnabled && !preventDebounce)
            {
                Debounce(eventName, () => Task.Run(() =>
                {
                    RenameProps(properties);
                    AddExtraPropertiesTo(properties);
                    AddDebounceCount(eventName, properties);
                    client.TrackEvent(eventName, properties);
                    client.Flush();
                }), 500);
            }
            else
            {
                Task.Run(() =>
                {
                    var props = properties.CloneDictionary();
                    RenameProps(props);
                    AddExtraPropertiesTo(props);
                    client.TrackEvent(eventName, props);
                    client.Flush();
                });
            }
        }

        /// <summary>
        /// Tracks the exception internal.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="ex">The ex.</param>
        /// <exception cref="InvalidOperationException">Telemetry was not initialized</exception>
        private void TrackExceptionInternal(TelemetryClient client, Exception ex)
        {
            if (Settings is null)
                throw new InvalidOperationException("Telemetry was not initialized");

            if (Settings.DebounceEnabled)
            {
                Debounce("exception", () => Task.Run(() =>
                {
                    var properties = new Dictionary<string, string>();
                    AddExtraPropertiesTo(properties);
                    AddDebounceCount("exception", properties);
                    client.TrackException(ex, properties);
                    client.Flush();
                }), 500);
            }
            else
            {
                Task.Run(() =>
                {
                    var properties = new Dictionary<string, string>();
                    AddExtraPropertiesTo(properties);
                    client.TrackException(ex, properties);
                    client.Flush();
                });
            }
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