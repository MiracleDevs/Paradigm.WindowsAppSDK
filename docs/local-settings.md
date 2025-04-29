# Local Settings Service

The `Paradigm.WindowsAppSDK.Services.LocalSettings` package provides a persistent storage mechanism for application settings in Windows App SDK applications, allowing applications to save and retrieve user preferences and application state across sessions.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.LocalSettings
```

## Key Components

### ILocalSettingsService

The core interface that defines methods for saving and retrieving settings.

```csharp
public interface ILocalSettingsService : IService
{
    /// <summary>
    /// Saves a setting value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The setting key.</param>
    /// <param name="value">The setting value.</param>
    void SaveSetting<T>(string key, T value);

    /// <summary>
    /// Gets a setting value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The setting key.</param>
    /// <returns>The setting value.</returns>
    T? ReadSetting<T>(string key);

    /// <summary>
    /// Gets a setting value, returning a default value if the setting is not found.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The setting key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The setting value or default if not found.</returns>
    T ReadSetting<T>(string key, T defaultValue);

    /// <summary>
    /// Deletes a setting.
    /// </summary>
    /// <param name="key">The setting key.</param>
    void DeleteSetting(string key);
}
```

### LocalSettingsService

The implementation of `ILocalSettingsService` that uses the Windows App SDK's `ApplicationData` APIs to store settings persistently on the device.

## Usage

### Service Registration

Register the local settings service in your application's startup:

```csharp
services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
```

### Saving Settings

```csharp
// Get the local settings service
var localSettings = ServiceLocator.Instance.GetRequiredService<ILocalSettingsService>();

// Save simple settings
localSettings.SaveSetting("Theme", "Dark");
localSettings.SaveSetting("FontSize", 14);
localSettings.SaveSetting("NotificationsEnabled", true);

// Save complex objects
localSettings.SaveSetting("UserPreferences", new UserPreferences
{
    Language = "en-US",
    TimeZone = "UTC",
    DisplayMode = DisplayMode.HighContrast
});
```

### Reading Settings

```csharp
// Read settings with default values if not found
string theme = localSettings.ReadSetting("Theme", "Light");
int fontSize = localSettings.ReadSetting("FontSize", 12);
bool notificationsEnabled = localSettings.ReadSetting("NotificationsEnabled", false);

// Read complex objects
var userPrefs = localSettings.ReadSetting<UserPreferences>("UserPreferences");
if (userPrefs != null)
{
    // Use the preferences
    ApplyLanguage(userPrefs.Language);
    SetTimeZone(userPrefs.TimeZone);
}
```

### Deleting Settings

```csharp
// Delete a setting
localSettings.DeleteSetting("TemporarySetting");

// Reset to defaults by deleting settings
localSettings.DeleteSetting("UserPreferences");
```

## Common Scenarios

### User Preferences

Store and retrieve user preferences that persist across application sessions:

```csharp
public class SettingsViewModel : ViewModelBase
{
    private readonly ILocalSettingsService _localSettings;
    private string _theme;
    private int _fontSize;
    private bool _notificationsEnabled;
    
    public string Theme
    {
        get => _theme;
        set
        {
            if (SetPropertyField(ref _theme, value))
            {
                _localSettings.SaveSetting("Theme", value);
                ApplyTheme(value);
            }
        }
    }
    
    public int FontSize
    {
        get => _fontSize;
        set
        {
            if (SetPropertyField(ref _fontSize, value))
            {
                _localSettings.SaveSetting("FontSize", value);
                ApplyFontSize(value);
            }
        }
    }
    
    public bool NotificationsEnabled
    {
        get => _notificationsEnabled;
        set
        {
            if (SetPropertyField(ref _notificationsEnabled, value))
            {
                _localSettings.SaveSetting("NotificationsEnabled", value);
                UpdateNotificationSettings(value);
            }
        }
    }
    
    public SettingsViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        _localSettings = GetRequiredService<ILocalSettingsService>();
        
        // Load settings with defaults
        _theme = _localSettings.ReadSetting("Theme", "Light");
        _fontSize = _localSettings.ReadSetting("FontSize", 12);
        _notificationsEnabled = _localSettings.ReadSetting("NotificationsEnabled", true);
    }
    
    public void ResetToDefaults()
    {
        Theme = "Light";
        FontSize = 12;
        NotificationsEnabled = true;
    }
}
```

### Application State

Save and restore application state to provide continuity across sessions:

```csharp
// Save application state before closing
public void SaveApplicationState()
{
    var stateInfo = new ApplicationState
    {
        LastOpenedDocumentId = currentDocumentId,
        LastViewedPage = currentPage,
        WindowPosition = new WindowPosition
        {
            X = currentWindow.X,
            Y = currentWindow.Y,
            Width = currentWindow.Width,
            Height = currentWindow.Height
        }
    };
    
    _localSettings.SaveSetting("ApplicationState", stateInfo);
}

// Restore application state when launching
public void RestoreApplicationState()
{
    var state = _localSettings.ReadSetting<ApplicationState>("ApplicationState");
    if (state != null)
    {
        // Restore document
        if (!string.IsNullOrEmpty(state.LastOpenedDocumentId))
            OpenDocument(state.LastOpenedDocumentId);
            
        // Restore window position
        if (state.WindowPosition != null)
            RestoreWindowPosition(state.WindowPosition);
            
        // Navigate to last viewed page
        if (state.LastViewedPage > 0)
            NavigateToPage(state.LastViewedPage);
    }
}
```

## Best Practices

### Key Naming

1. **Use descriptive keys**: Choose clear, descriptive names for settings keys
2. **Consider namespacing**: For complex applications, use prefixes to group related settings (e.g., "UI:Theme", "Notifications:Enabled")

### Data Types

1. **Simple types**: Strings, numbers, booleans, and simple serializable objects work best
2. **Avoid large objects**: The local settings service is not designed for large data storage (use FileStorageService instead)

### Security Considerations

1. **Don't store sensitive data**: Local settings are not encrypted by default
2. **Clear sensitive settings**: Provide a way for users to clear their data

### Performance

1. **Cache frequently accessed settings**: For settings accessed multiple times, consider caching in memory
2. **Batch updates**: Group related setting changes to minimize storage operations 