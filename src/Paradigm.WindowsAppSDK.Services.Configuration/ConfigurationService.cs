using Paradigm.WindowsAppSDK.Services.Configuration.JsonConverters;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Paradigm.WindowsAppSDK.Services.Configuration
{
    /// <summary>
    /// Implements the configuration service to load the configuration json file values.
    /// </summary>
    /// <seealso cref="IConfigurationService" />
    public class ConfigurationService : IConfigurationService
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
        /// Initializes a new instance of the <see cref="ConfigurationService"/> class.
        /// </summary>
        /// <param name="fileStorage">The file storage.</param>
        public ConfigurationService()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the provided configuration content to the configurations dictionary.
        /// </summary>
        /// <param name="serializedContent">Content of the serialized.</param>
        /// <param name="overwriteExistingKeys">if set to <c>true</c> [overwrite existing keys].</param>
        public void AddConfigurationContent(string serializedContent, bool overwriteExistingKeys = false)
        {
            if (Configurations is null)
                Configurations = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            try
            {
                var serializerOptions = new JsonSerializerOptions
                {
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    TypeInfoResolver = ListObjectJsonContext.Default
                };
                serializerOptions.Converters.Add(new DictionaryStringObjectJsonConverter());

                var newConfigurations = JsonSerializer.Deserialize<Dictionary<string, object>>(serializedContent, serializerOptions);
                if (newConfigurations is null)
                    return;

                foreach (var key in newConfigurations.Keys)
                {
                    if (!Configurations.ContainsKey(key))
                        Configurations.Add(key, newConfigurations[key]);
                    else if (overwriteExistingKeys)
                        Configurations[key] = newConfigurations[key];
                }
            }
            catch
            {
                throw;
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

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="jsonTypeInfo">The json type information.</param>
        /// <returns></returns>
        public T? GetObject<T>(string key, JsonTypeInfo<T> jsonTypeInfo) where T : class
        {
            try
            {
                var value = GetValueFromConfigurations(key);
                if (value is null)
                    return default;

                string? serializedValue;

                try
                {
                    serializedValue = JsonSerializer.Serialize(value, ListObjectJsonContext.Default.DictionaryStringObject);
                }
                catch (InvalidCastException)
                {
                    serializedValue = JsonSerializer.Serialize(value, ListObjectJsonContext.Default.ListObject);
                }

                return !string.IsNullOrWhiteSpace(serializedValue)
                    ? JsonSerializer.Deserialize(serializedValue, jsonTypeInfo)
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