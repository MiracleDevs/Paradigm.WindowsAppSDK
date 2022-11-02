using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

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
        private TelemetrySettings? Settings { get; set; }

        /// <summary>
        /// Gets or sets the telemetries client.
        /// </summary>
        /// <value>
        /// The telemetries client.
        /// </value>
        private TelemetryClient? TelemetriesClient { get; set; }

        /// <summary>
        /// Gets the extra properties.
        /// </summary>
        /// <value>
        /// The extra properties.
        /// </value>
        private IDictionary<string, string> ExtraProperties { get; }

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
            if (TelemetriesClient != null)
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
            if (Settings == null || TelemetriesClient == null)
                throw new InvalidOperationException("Telemetry was not initialized");

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
                    RenameProps(properties);
                    AddExtraPropertiesTo(properties);
                    TelemetriesClient.TrackEvent(eventName, properties);
                    TelemetriesClient.Flush();
                });
            }
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

        #region Private Methods

        /// <summary>
        /// Initializes the client.
        /// </summary>
        private void InitializeClient()
        {
            if (Settings == null)
                return;

            var configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString = Settings.ConnectionString;
            TelemetriesClient = new TelemetryClient(configuration);
        }

        /// <summary>
        /// Adds the extra properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        private void AddExtraPropertiesTo(IDictionary<string, string> properties)
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
        private void RenameProps(IDictionary<string, string> properties)
        {
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
                if (Settings?.AllowedCustomProps?.Contains(prop.Key, StringComparer.OrdinalIgnoreCase) ?? false)
                    continue;

                i++;
                properties.Remove(prop);
                properties.Add($"value{i}", prop.Value);
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
            if (debounceTimer.Item2 > 1) properties.Add("count", debounceTimer.Item2.ToString());
            TimersDictionary.Remove(eventName);
        }

        #endregion
    }
}