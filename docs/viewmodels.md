# ViewModels

The `Paradigm.WindowsAppSDK.ViewModels` package provides base classes and utilities for implementing the MVVM (Model-View-ViewModel) pattern in Windows App SDK applications.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.ViewModels
```

## Key Components

### Base Classes

#### ViewModelBase

The `ViewModelBase` class is the foundation for all view models in the application. It implements `INotifyPropertyChanged` for property change notifications and provides utility methods for service resolution.

Key features:
- Property change notification
- Service resolution from dependency injection
- Resource management (implements `IDisposable`)
- Helper methods for setting properties

```csharp
public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    // Core properties and methods for MVVM implementation
    
    protected bool SetPropertyField<T>(ref T field, T value, [CallerMemberName] string fieldName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(fieldName);
        return true;
    }
    
    // Service resolution methods
    protected T? GetService<T>()
    {
        return ServiceProvider.GetService<T>();
    }

    protected T GetRequiredService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
```

#### PageViewModelBase

Extends `ViewModelBase` and is designed to be used with page-level views. It provides additional functionality specific to pages.

```csharp
public abstract class PageViewModelBase : ViewModelBase
{
    // Additional page-specific functionality
}
```

#### ListPageViewModelBase

Extends `PageViewModelBase` and is designed for pages that display lists of items.

```csharp
public abstract class ListPageViewModelBase : PageViewModelBase
{
    // List-specific functionality
}
```

### ServiceLocator

The `ServiceLocator` class provides a centralized point for accessing the application's services. It's implemented as a singleton and wraps the .NET dependency injection container.

```csharp
public class ServiceLocator
{
    private static readonly Lazy<ServiceLocator> LazyInstance = new(() => new ServiceLocator());
    
    public static ServiceLocator Instance => LazyInstance.Value;
    
    // Service resolution methods
    public T? GetService<T>()
    {
        return ServiceProvider?.GetService<T>();
    }

    public T GetRequiredService<T>() where T : notnull
    {
        if (ServiceProvider == null)
            throw new InvalidOperationException("Service provider is not configured");
            
        return ServiceProvider.GetRequiredService<T>();
    }
}
```

### ConfigurationProvider

The `ConfigurationProvider` class provides a way to access configuration values from various sources, such as JSON configuration files.

### Utils

Contains utility classes for common operations, including:

- `DebounceHandler`: Helps prevent rapid repeated executions of a method

### Extensions

Contains extension methods that enhance the functionality of various types, including:

- Service collection extensions for registering view models
- Assembly extensions for filtering and discovering types

## Usage

### Creating a View Model

Create a view model by inheriting from `ViewModelBase`:

```csharp
public class MainViewModel : ViewModelBase
{
    private string _title;
    
    public string Title
    {
        get => _title;
        set => SetPropertyField(ref _title, value);
    }
    
    public MainViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        Title = "My Application";
    }
}
```

### Using the ServiceLocator

Access services using the `ServiceLocator`:

```csharp
// Get a service
var navigationService = ServiceLocator.Instance.GetRequiredService<INavigationService>();

// Navigate to a page
await navigationService.NavigateToAsync<HomeViewModel>();
```

### Using the ConfigurationProvider

Access configuration values:

```csharp
// Initialize configuration provider
var configProvider = ServiceLocator.Instance.GetRequiredService<ConfigurationProvider>();
configProvider.Initialize(Package.Current.InstalledLocation.Path, 
    new List<Tuple<string, bool>>
    {
        new("appsettings.json", false)
    });

// Get configuration value
var apiUrl = configProvider.GetValue<string>("Api:BaseUrl");
```

### Registering View Models

Register view models with the dependency injection container:

```csharp
// Register all view models in specified assemblies
services.RegisterViewModels(typeof(App).Assembly);
``` 