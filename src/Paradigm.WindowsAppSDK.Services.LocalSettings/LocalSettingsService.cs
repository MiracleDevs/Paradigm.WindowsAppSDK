using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Paradigm.WindowsAppSDK.Services.LocalSettings
{
    public class LocalSettingsService : ILocalSettingsService
    {
        private const string SettingsKey = "local-settings";

        /// <summary>
        /// Gets or sets the settings container.
        /// </summary>
        /// <value>
        /// The settings container.
        /// </value>
        private IDictionary<string, object>? SettingsContainer { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="settingsContainer">The settings container.</param>
        public void Initialize(IDictionary<string, object> settingsContainer)
        {
            SettingsContainer = settingsContainer;
        }

        /// <summary>
        /// Gets the stored settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">SettingsContainer</exception>
        public T? GetStoredSettings<T>()
        {
            if (SettingsContainer is null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            var storedSettings = SettingsContainer.ContainsKey(SettingsKey) ? SettingsContainer[SettingsKey].ToString() : default;
            return !string.IsNullOrWhiteSpace(storedSettings) ? JsonSerializer.Deserialize<T>(storedSettings) : default;
        }

        /// <summary>
        /// Gets the stored settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonTypeInfo">The json type information.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">SettingsContainer</exception>
        public T? GetStoredSettings<T>(JsonTypeInfo<T> jsonTypeInfo)
        {
            if (SettingsContainer is null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            var storedSettings = SettingsContainer.ContainsKey(SettingsKey) ? SettingsContainer[SettingsKey].ToString() : default;
            return !string.IsNullOrWhiteSpace(storedSettings) ? JsonSerializer.Deserialize<T>(storedSettings, jsonTypeInfo) : default;
        }

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">SettingsContainer</exception>
        public void StoreSettings<T>(T settings)
        {
            if (SettingsContainer is null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            if (settings is null)
                return;

            SettingsContainer[SettingsKey] = JsonSerializer.Serialize(settings);
        }

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings">The settings.</param>
        /// <param name="jsonTypeInfo">The json type information.</param>
        /// <exception cref="ArgumentNullException">SettingsContainer</exception>
        public void StoreSettings<T>(T settings, JsonTypeInfo<T> jsonTypeInfo)
        {
            if (SettingsContainer is null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            if (settings is null)
                return;

            SettingsContainer[SettingsKey] = JsonSerializer.Serialize(settings, jsonTypeInfo);
        }

        /// <summary>
        /// Resets to default settings.
        /// </summary>
        public void ResetToDefaultSettings()
        {
            if (SettingsContainer is null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            SettingsContainer.Clear();
        }
    }
}