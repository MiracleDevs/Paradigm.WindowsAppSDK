# Application Information Service

The `Paradigm.WindowsAppSDK.Services.ApplicationInformation` package provides access to application metadata and system information in Windows App SDK applications.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.ApplicationInformation
```

## Key Components

### IApplicationInformationService

The core interface that defines methods for accessing application metadata and system information.

```csharp
public interface IApplicationInformationService : IService
{
    /// <summary>
    /// Gets the application name.
    /// </summary>
    string ApplicationName { get; }

    /// <summary>
    /// Gets the application version.
    /// </summary>
    Version ApplicationVersion { get; }

    /// <summary>
    /// Gets the device name.
    /// </summary>
    string DeviceName { get; }

    /// <summary>
    /// Gets the operating system architecture.
    /// </summary>
    Architecture OperatingSystemArchitecture { get; }

    /// <summary>
    /// Gets the operating system version.
    /// </summary>
    string OperatingSystemVersion { get; }

    /// <summary>
    /// Gets the application publisher.
    /// </summary>
    string Publisher { get; }

    /// <summary>
    /// Gets the application package full name.
    /// </summary>
    string PackageFullName { get; }

    /// <summary>
    /// Gets the application package family name.
    /// </summary>
    string PackageFamilyName { get; }

    /// <summary>
    /// Gets a value indicating whether the application is running in development mode.
    /// </summary>
    bool IsDevelopmentMode { get; }
}
```

### ApplicationInformationService

The implementation of `IApplicationInformationService` that retrieves application metadata from the Windows App package and system information from the operating system.

## Usage

### Service Registration

Register the application information service in your application's startup:

```csharp
services.AddSingleton<IApplicationInformationService, ApplicationInformationService>();
```

### Accessing Application Information

```csharp
// Get the application information service
var appInfoService = ServiceLocator.Instance.GetRequiredService<IApplicationInformationService>();

// Access application metadata
string appName = appInfoService.ApplicationName;
Version version = appInfoService.ApplicationVersion;
string publisher = appInfoService.Publisher;

// Access system information
string deviceName = appInfoService.DeviceName;
string osVersion = appInfoService.OperatingSystemVersion;
Architecture architecture = appInfoService.OperatingSystemArchitecture;

// Check if running in development mode
bool isDevelopment = appInfoService.IsDevelopmentMode;
```

### Displaying Application Information

A common use case is to display application information in an "About" dialog or page:

```csharp
public class AboutViewModel : ViewModelBase
{
    private readonly IApplicationInformationService _appInfoService;
    
    public string ApplicationName => _appInfoService.ApplicationName;
    public string VersionInfo => $"Version {_appInfoService.ApplicationVersion}";
    public string PublisherInfo => $"Published by {_appInfoService.Publisher}";
    public string SystemInfo => $"Running on {_appInfoService.DeviceName}, {_appInfoService.OperatingSystemVersion} ({_appInfoService.OperatingSystemArchitecture})";
    
    public AboutViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        _appInfoService = GetRequiredService<IApplicationInformationService>();
    }
}
```

### Using Package Information

Access package-specific information for advanced scenarios:

```csharp
// Get package information
string packageFullName = appInfoService.PackageFullName;
string packageFamilyName = appInfoService.PackageFamilyName;

// Use package information with other Windows APIs
// For example, to interact with the Windows Store
```

## Advanced Usage

### Environmental Checks

You can use the `IsDevelopmentMode` property to conditionally enable features based on the environment:

```csharp
// Only enable debug features in development mode
if (appInfoService.IsDevelopmentMode)
{
    // Enable debug features
    EnableDebugLogging();
    ShowDeveloperTools();
}
```

### Application Telemetry

Combine with the Telemetry service to include application information in telemetry events:

```csharp
// Add application information to telemetry events
telemetryService.TrackEvent("ApplicationStarted", new Dictionary<string, string>
{
    ["AppVersion"] = appInfoService.ApplicationVersion.ToString(),
    ["OSVersion"] = appInfoService.OperatingSystemVersion,
    ["DeviceName"] = appInfoService.DeviceName
});
```

### System-Aware Features

Use system information to enable or customize features based on the operating system or device:

```csharp
// Adapt features based on system architecture
if (appInfoService.OperatingSystemArchitecture == Architecture.Arm64)
{
    // Use ARM-specific optimizations
}
else
{
    // Use x86/x64 optimizations
}
``` 