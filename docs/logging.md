# Logging Service

The `Paradigm.WindowsAppSDK.Services.Logging` package provides a centralized logging system for Windows App SDK applications, enabling consistent logging across different components of an application.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Logging
```

## Key Components

### ILogService

The core interface that defines methods for logging messages at different severity levels.

```csharp
public interface ILogService : IService
{
    /// <summary>
    /// Logs the debug message.
    /// </summary>
    /// <param name="message">The message.</param>
    void LogDebug(string message);

    /// <summary>
    /// Logs the information message.
    /// </summary>
    /// <param name="message">The message.</param>
    void LogInformation(string message);

    /// <summary>
    /// Logs the warning message.
    /// </summary>
    /// <param name="message">The message.</param>
    void LogWarning(string message);

    /// <summary>
    /// Logs the error message.
    /// </summary>
    /// <param name="message">The message.</param>
    void LogError(string message);

    /// <summary>
    /// Logs the error message and exception.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="exception">The exception.</param>
    void LogError(string message, Exception exception);

    /// <summary>
    /// Logs the critical message.
    /// </summary>
    /// <param name="message">The message.</param>
    void LogCritical(string message);

    /// <summary>
    /// Logs the critical message and exception.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="exception">The exception.</param>
    void LogCritical(string message, Exception exception);
}
```

### LogService

The implementation of `ILogService` that handles logging messages to a file.

### LogSettings

Configuration settings for the logging service.

```csharp
public class LogSettings
{
    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the logs.
    /// </summary>
    public LogTypes LogTypes { get; set; } = LogTypes.Debug | LogTypes.Information | LogTypes.Warning | LogTypes.Error | LogTypes.Critical;

    /// <summary>
    /// Gets or sets the size of the maximum file.
    /// </summary>
    public long MaximumFileSize { get; set; } = 5 * 1024 * 1024; // 5MB

    /// <summary>
    /// Gets or sets a value indicating whether to allow archive process when log file is restarted.
    /// </summary>
    public bool ArchivePreviousLog { get; set; } = false;
}
```

### LogTypes

An enumeration that defines the different types (severity levels) of log messages.

```csharp
[Flags]
public enum LogTypes
{
    Debug = 1,
    Information = 2,
    Warning = 4,
    Error = 8,
    Critical = 16
}
```

## Usage

### Service Registration

Register the logging service in your application's startup:

```csharp
// Register the log settings
services.AddSingleton(new LogSettings
{
    FilePath = Path.Combine(AppContext.BaseDirectory, "Logs", "app.log"),
    LogTypes = LogTypes.Debug | LogTypes.Information | LogTypes.Warning | LogTypes.Error | LogTypes.Critical,
    MaximumFileSize = 10 * 1024 * 1024, // 10MB
    ArchivePreviousLog = true
});

// Register the log service
services.AddSingleton<ILogService, LogService>();
```

### Basic Logging

```csharp
// Get the log service
var logService = ServiceLocator.Instance.GetRequiredService<ILogService>();

// Log messages at different levels
logService.LogDebug("This is a debug message");
logService.LogInformation("Application started");
logService.LogWarning("Configuration file not found, using defaults");

// Log errors with exception details
try
{
    // Some operation that might throw
    throw new InvalidOperationException("Example exception");
}
catch (Exception ex)
{
    logService.LogError("An error occurred during operation", ex);
}

// Log critical errors
logService.LogCritical("System is in an unstable state", new Exception("Critical failure"));
```

### Configuring Log Levels

You can configure which log levels are written to the log file:

```csharp
// Only log warnings, errors, and critical messages
services.AddSingleton(new LogSettings
{
    FilePath = Path.Combine(AppContext.BaseDirectory, "Logs", "app.log"),
    LogTypes = LogTypes.Warning | LogTypes.Error | LogTypes.Critical
});
```

### Log File Management

The logging service automatically manages the log file size:

1. When the log file exceeds the `MaximumFileSize`, it is truncated to half its size
2. The oldest log entries are removed first
3. If `ArchivePreviousLog` is set to `true`, the existing log file is archived with a timestamp before creating a new one

```csharp
// Configure log file management
services.AddSingleton(new LogSettings
{
    FilePath = Path.Combine(AppContext.BaseDirectory, "Logs", "app.log"),
    MaximumFileSize = 20 * 1024 * 1024, // 20MB
    ArchivePreviousLog = true
});
```

## Best Practices

### Log Level Guidelines

- **Debug**: Detailed information useful for debugging, not enabled in production
- **Information**: General information about application flow
- **Warning**: Potential issues that don't prevent the application from working
- **Error**: Errors that prevent a specific operation from completing
- **Critical**: Critical errors that might lead to application failure

### Structuring Log Messages

1. **Be specific**: Include enough detail to understand what happened
2. **Include context**: Mention the component, user, or operation involved
3. **Be consistent**: Use a consistent format for similar log messages

### Performance Considerations

1. **Check log level before formatting**: If constructing a log message is expensive, check if the log level is enabled first
2. **Avoid excessive logging**: Especially in performance-critical code paths
3. **Consider asynchronous logging**: For high-throughput applications

### Security Considerations

1. **Don't log sensitive information**: Avoid logging passwords, tokens, or personal data
2. **Secure log files**: Ensure log files have appropriate access permissions
3. **Consider log rotation**: Implement a policy for how long logs are kept 