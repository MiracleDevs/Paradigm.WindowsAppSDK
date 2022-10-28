namespace Paradigm.WindowsAppSDK.Services.LegacyConfiguration
{
    /// <summary>
    /// Implements the legacy configuration service to load the legacy configuration json file values.
    /// </summary>
    /// <seealso cref="ILegacyConfigurationService" />
    public class LegacyConfigurationService : ILegacyConfigurationService
    {
        #region Properties

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <value>
        /// The configurations.
        /// </value>
        private Dictionary<string, object>? Configurations { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyConfigurationService"/> class.
        /// </summary>
        /// <param name="fileStorage">The file storage.</param>
        public LegacyConfigurationService()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            await LoadConfigurationsAsync();
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string? GetString(string key)
        {
            return this.GetValueFromConfigurations(key)?.ToString();
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool? GetBoolean(string key)
        {
            var value = this.GetValueFromConfigurations(key);
            return value != null ? Convert.ToBoolean(value) : default(bool?);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public double? GetDouble(string key)
        {
            var value = this.GetValueFromConfigurations(key);
            return value != null ? Convert.ToDouble(value) : default(double?);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the value from configurations.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">key</exception>
        /// <exception cref="InvalidOperationException">Configuration was not initialized</exception>
        private object? GetValueFromConfigurations(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            if (this.Configurations == null)
                throw new InvalidOperationException("Configuration was not initialized");

            Configurations.TryGetValue(key, out var value);

            return value;
        }

        /// <summary>
        /// Loads the configurations asynchronous.
        /// </summary>
        private async Task LoadConfigurationsAsync()
        {
            try
            {
                //const string configFileName = "config.json";
                //var alternativePath = Path.Combine("IdleContent", configFileName);
                //var content = string.Empty;

                //if (this.FileStorageService.FileExists(configFileName))
                //    content = await this.FileStorageService.ReadLocalTextAsync(configFileName);

                //else if (this.FileStorageService.FileExists(Path.ChangeExtension(configFileName, "jsonc")))
                //    content = await this.FileStorageService.ReadLocalTextAsync(Path.ChangeExtension(configFileName, "jsonc"));

                //else if (this.FileStorageService.FileExists(alternativePath))
                //    content = await this.FileStorageService.ReadLocalTextAsync(alternativePath);

                //else if (this.FileStorageService.FileExists(Path.ChangeExtension(alternativePath, "jsonc")))
                //    content = await this.FileStorageService.ReadLocalTextAsync(Path.ChangeExtension(alternativePath, "jsonc"));

                //else
                //    content = await this.FileStorageService.ReadContentFromApplicationUriAsync(Path.Combine("Configuration", configFileName));

                //var deserializerOptions = new JsonSerializerOptions();
                //deserializerOptions.Converters.Add(new DictionaryStringObjectJsonConverter());
                //this.Configurations = JsonSerializer.Deserialize<Dictionary<string, object>>(content, deserializerOptions);
            }
            catch
            {
                this.Configurations = new Dictionary<string, object>();
            }
        }

        #endregion
    }
}
