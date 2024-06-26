using Paradigm.WindowsAppSDK.Services.LegacyConfiguration.JsonConverters;
using System.Text.Json;

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
        /// <param name="serializedContent">Content of the serialized.</param>
        public void Initialize(string serializedContent)
        {
            try
            {
                var deserializerOptions = new JsonSerializerOptions();
                deserializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                deserializerOptions.Converters.Add(new DictionaryStringObjectJsonConverter());
                Configurations = JsonSerializer.Deserialize<Dictionary<string, object>>(serializedContent, deserializerOptions);
            }
            catch
            {
                Configurations = new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string? GetString(string key)
        {
            return GetValueFromConfigurations(key)?.ToString();
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool? GetBoolean(string key)
        {
            try
            {
                var value = GetValueFromConfigurations(key);
                return value is not null ? Convert.ToBoolean(value) : default(bool?);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public double? GetDouble(string key)
        {
            try
            {
                var value = GetValueFromConfigurations(key);
                return value is not null ? Convert.ToDouble(value) : default(double?);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public T? GetObject<T>(string key, JsonSerializerOptions? options = null) where T : class
        {
            try
            {
                var value = GetValueFromConfigurations(key);
                if (value is null)
                    return default;

                var serializedValue = JsonSerializer.Serialize(value);

                return !string.IsNullOrWhiteSpace(serializedValue)
                    ? JsonSerializer.Deserialize<T>(serializedValue, options)
                    : default;
            }
            catch
            {
                return default;
            }
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

            if (Configurations is null)
                throw new InvalidOperationException("Configuration was not initialized");

            Configurations.TryGetValue(key, out var value);

            return value;
        }

        #endregion
    }
}