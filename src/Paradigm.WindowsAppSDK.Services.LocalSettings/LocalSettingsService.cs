using System.Text.Json;

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
        /// <returns></returns>
        public T? GetStoredSettings<T>()
        {
            if (SettingsContainer == null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            var storedSettings = SettingsContainer.ContainsKey(SettingsKey) ? SettingsContainer[SettingsKey].ToString() : default;
            return !string.IsNullOrWhiteSpace(storedSettings) ? JsonSerializer.Deserialize<T>(storedSettings) : default;
        }

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void StoreSettings<T>(T settings)
        {
            if (SettingsContainer == null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            if (settings == null)
                return;

            SettingsContainer[SettingsKey] = JsonSerializer.Serialize(settings);
        }

        /// <summary>
        /// Resets to default settings.
        /// </summary>
        public void ResetToDefaultSettings()
        {
            if (SettingsContainer == null)
                throw new ArgumentNullException(nameof(SettingsContainer));

            SettingsContainer.Clear();
        }
    }
}