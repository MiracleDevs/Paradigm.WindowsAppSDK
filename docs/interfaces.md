# Interfaces

The `Paradigm.WindowsAppSDK.Services.Interfaces` package contains the core interfaces used throughout the Paradigm.WindowsAppSDK framework.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Interfaces
```

## Core Interfaces

### IService

The `IService` interface is a marker interface that all services in the framework implement. This allows for easy identification and registration of services in the dependency injection container.

```csharp
namespace Paradigm.WindowsAppSDK.Services.Interfaces
{
    public interface IService
    {
    }
}
```

### INavigable

The `INavigable` interface provides navigation-aware methods for both the navigator and the navigable elements. Classes implementing this interface can participate in the navigation flow managed by the `INavigationService`.

```csharp
namespace Paradigm.WindowsAppSDK.Services.Interfaces
{
    public interface INavigable
    {
        /// <summary>
        /// Determines whether this instance can navigate to the specified navigable element.
        /// </summary>
        /// <param name="navigable">The navigable element.</param>
        Task<bool> CanNavigateTo(INavigable navigable);

        /// <summary>
        /// Determines whether this instance can navigate from the specified navigable element.
        /// </summary>
        /// <param name="navigable">The navigable element.</param>
        Task<bool> CanNavigateFrom(INavigable navigable);
    }
}
```

## Usage

These interfaces are typically used in the following ways:

1. Services implement the `IService` interface to be discovered and registered by the dependency injection container.

```csharp
public class MyCustomService : IService
{
    // Service implementation
}
```

2. ViewModels implement the `INavigable` interface to participate in the navigation flow.

```csharp
public class MyPageViewModel : ViewModelBase, INavigable
{
    public async Task<bool> CanNavigateTo(INavigable navigable)
    {
        // Logic to determine if navigation to this view model is allowed
        return true;
    }

    public async Task<bool> CanNavigateFrom(INavigable navigable)
    {
        // Logic to determine if navigation from this view model is allowed
        return true;
    }
}
```

By implementing these interfaces, you ensure your components can fully participate in the framework's functionality. 