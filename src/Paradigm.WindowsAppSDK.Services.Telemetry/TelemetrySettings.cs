namespace Paradigm.WindowsAppSDK.Services.Telemetry
{
    public class TelemetrySettings
    {
        #region Properties

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; }

        /// <summary>
        /// Gets a value indicating whether [debounce enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debounce enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool DebounceEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether [rename properties enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rename properties enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool RenamePropertiesEnabled { get; }

        /// <summary>
        /// Gets the allowed custom props.
        /// </summary>
        /// <value>
        /// The allowed custom props.
        /// </value>
        public string[]? AllowedCustomProps { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetrySettings"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public TelemetrySettings(string connectionString) : this(connectionString, true, true, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetrySettings" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="debounceEnabled">if set to <c>true</c> [debounce enabled].</param>
        /// <param name="renamePropertiesEnabled">if set to <c>true</c> [rename properties enabled].</param>
        /// <param name="allowedCustomProps">The allowed props names.</param>
        public TelemetrySettings(string connectionString, bool debounceEnabled, bool renamePropertiesEnabled, string[]? allowedCustomProps)
        {
            ConnectionString = connectionString;
            DebounceEnabled = debounceEnabled;
            RenamePropertiesEnabled = renamePropertiesEnabled;
            AllowedCustomProps = allowedCustomProps;
        }

        #endregion
    }
}