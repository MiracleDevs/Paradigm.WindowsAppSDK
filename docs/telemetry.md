# Telemetry Service

The `Paradigm.WindowsAppSDK.Services.Telemetry` package provides a comprehensive telemetry system for Windows App SDK applications, enabling applications to track events, metrics, and exceptions for analysis and monitoring.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Telemetry
```

## Key Components

### ITelemetryService

The core interface that defines methods for tracking telemetry data.

```csharp
public interface ITelemetryService : IService
{
    /// <summary>
    /// Gets the session identifier.
    /// </summary>
    string SessionId { get; }

    /// <summary>
    /// Initializes the telemetry engine with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="sessionId">The session identifier.</param>
    void Initialize(string connectionString, string? sessionId = null);

    /// <summary>
    /// Initializes the telemetry engine with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="key">The telemetry instance key.</param>
    void Initialize(string connectionString, string? sessionId, string key);

    /// <summary>
    /// Tracks the event.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    void TrackEvent(string eventName);

    /// <summary>
    /// Tracks the event.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    /// <param name="properties">The properties.</param>
    void TrackEvent(string eventName, IDictionary<string, string> properties);

    /// <summary>
    /// Tracks the event.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="preventDebounce">if set to <c>true</c> [prevent debounce].</param>
    /// <param name="key">The telemetry instance key.</param>
    void TrackEvent(string eventName, IDictionary<string, string> properties, bool preventDebounce, string? key = null);

    /// <summary>
    /// Tracks the exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    void TrackException(Exception exception);

    /// <summary>
    /// Tracks the exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="properties">The properties.</param>
    void TrackException(Exception exception, IDictionary<string, string> properties);
}
```

### TelemetryService

The implementation of `ITelemetryService` that handles tracking events, metrics, and exceptions. It includes features for:

- Event debouncing to prevent excessive reporting
- Session tracking
- Multiple telemetry instances with different connection strings
- Custom properties for events and exceptions

### TelemetrySettings

Configuration settings for the telemetry service.

```csharp
public class TelemetrySettings
{
    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the telemetry tracking is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow debug tracing.
    /// </summary>
    public bool DebugTracing { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the maximum amount of seconds to debounce same-type event tracking.
    /// </summary>
    public double EventDebounce { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets a value indicating the maximum amount of events to collect when offline.
    /// </summary>
    public uint MaxOfflineEvents { get; set; } = 1000000;
}
```

## Usage

### Service Registration

Register the telemetry service in your application's startup:

```csharp
// Register telemetry settings
services.AddSingleton(new TelemetrySettings
{
    ConnectionString = "InstrumentationKey=your-app-insights-key",
    IsEnabled = true,
    DebugTracing = false,
    EventDebounce = 1.0,
    MaxOfflineEvents = 10000
});

// Register the telemetry service
services.AddSingleton<ITelemetryService, TelemetryService>();
```

### Initializing Telemetry

Initialize the telemetry service with your connection string:

```csharp
// Get the telemetry service
var telemetryService = ServiceLocator.Instance.GetRequiredService<ITelemetryService>();

// Initialize with a connection string
telemetryService.Initialize("InstrumentationKey=your-app-insights-key");

// Or initialize with a custom session ID
telemetryService.Initialize("InstrumentationKey=your-app-insights-key", "user123-session456");
```

### Tracking Events

```csharp
// Track a simple event
telemetryService.TrackEvent("ApplicationStarted");

// Track an event with properties
telemetryService.TrackEvent("DocumentOpened", new Dictionary<string, string>
{
    ["DocumentId"] = documentId,
    ["DocumentType"] = documentType,
    ["DocumentSize"] = documentSize.ToString()
});

// Track an event without debouncing (will be sent immediately, even if a similar event was recently sent)
telemetryService.TrackEvent("CriticalOperation", properties, preventDebounce: true);
```

### Tracking Exceptions

```csharp
try
{
    // Some operation that might throw
    ProcessDocument(documentId);
}
catch (Exception ex)
{
    // Track the exception
    telemetryService.TrackException(ex);
    
    // Or track with additional properties
    telemetryService.TrackException(ex, new Dictionary<string, string>
    {
        ["DocumentId"] = documentId,
        ["UserAction"] = "ProcessDocument"
    });
}
```

### Using Multiple Telemetry Instances

You can use multiple telemetry instances with different connection strings:

```csharp
// Initialize the default telemetry instance
telemetryService.Initialize("InstrumentationKey=main-app-insights-key");

// Initialize a secondary telemetry instance
telemetryService.Initialize("InstrumentationKey=secondary-app-insights-key", null, "Secondary");

// Track events to different instances
telemetryService.TrackEvent("MainEvent", properties, false); // Goes to default instance
telemetryService.TrackEvent("SecondaryEvent", properties, false, "Secondary"); // Goes to secondary instance
```

## Advanced Usage

### Event Debouncing

The telemetry service includes built-in debouncing to prevent excessive event tracking:

```csharp
// Configure debounce time in settings
services.AddSingleton(new TelemetrySettings
{
    EventDebounce = 5.0 // 5 seconds
});

// Events with the same name and properties within 5 seconds will be debounced (only the first one is sent)
telemetryService.TrackEvent("ButtonClicked", new Dictionary<string, string> { ["ButtonId"] = "SaveButton" });
// If this is called again within 5 seconds, it will be ignored
telemetryService.TrackEvent("ButtonClicked", new Dictionary<string, string> { ["ButtonId"] = "SaveButton" });

// Force an event to be sent regardless of debounce
telemetryService.TrackEvent("ImportantEvent", properties, preventDebounce: true);
```

### Session Tracking

The telemetry service automatically generates a session ID if one is not provided:

```csharp
// Access the current session ID
string sessionId = telemetryService.SessionId;

// Use the session ID in other parts of the application
localSettings.SaveSetting("LastSessionId", sessionId);
```

### Integrating with Application Insights

The telemetry service is designed to work with Azure Application Insights:

```csharp
// Get the Application Insights instrumentation key from configuration
var configService = ServiceLocator.Instance.GetRequiredService<IConfigurationService>();
string instrumentationKey = configService.GetValue<string>("Telemetry:InstrumentationKey");

// Initialize telemetry with the key
if (!string.IsNullOrEmpty(instrumentationKey))
{
    telemetryService.Initialize($"InstrumentationKey={instrumentationKey}");
}
```

## Best Practices

### Event Naming

1. **Use consistent names**: Adopt a naming convention for events (e.g., "PageViewed", "ButtonClicked")
2. **Use hierarchical names**: Consider using dot-notation for hierarchical events (e.g., "Document.Opened", "Document.Saved")
3. **Be specific but not too specific**: "ButtonClicked" with a "ButtonId" property is better than "SaveButtonClicked"

### Properties

1. **Include context**: Add relevant properties to events for better analysis (user ID, feature area, etc.)
2. **Don't include sensitive data**: Avoid including personally identifiable information or sensitive data
3. **Use consistent property names**: Standardize property names across events for better analysis

### Performance Considerations

1. **Use debounce for high-frequency events**: For events that might be triggered rapidly, rely on the built-in debouncing
2. **Consider offline support**: The service can cache events when offline, but be mindful of the `MaxOfflineEvents` setting
3. **Balance detail and volume**: Collect enough detail to be useful, but avoid excessive telemetry that might impact performance

### Testing

1. **Enable debug tracing in development**: Set `DebugTracing = true` in development to see telemetry events in the debug output
2. **Use a separate connection string for testing**: Don't mix test and production telemetry data 