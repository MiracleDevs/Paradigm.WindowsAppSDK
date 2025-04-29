# Navigation Service

The `Paradigm.WindowsAppSDK.Services.Navigation` package provides a flexible navigation system for managing page navigation in Windows App SDK applications.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Navigation
```

## Key Components

### INavigationService

The `INavigationService` interface defines methods and properties for managing navigation between pages in the application.

Key features include:
- Back/forward navigation management
- Page registration
- Navigation history
- Transition effects
- Content clearing

### NavigationService

The implementation of `INavigationService` that handles navigation between pages, maintaining navigation history, and managing the navigation frame.

### INavigationFrame

Represents a frame control that can host pages and handle navigation operations.

### INavigableView

Interface that represents a view that can be navigated to. A navigable view is paired with an `INavigable` view model.

### NavigationTransition

An enumeration defining the available transition effects when navigating between pages:

```csharp
public enum NavigationTransition
{
    Default,
    Entrance,
    DrillIn,
    Suppress
}
```

## Usage

### Service Registration

Register the navigation service in your application's startup:

```csharp
services.AddSingleton<INavigationService, NavigationService>();
```

### View/ViewModel Registration

Register views and view models in your application's startup:

```csharp
private static void RegisterNavigation()
{
    var navigationService = serviceProvider.GetRequiredService<INavigationService>();
    navigationService.Register<HomePage, HomeViewModel>();
    navigationService.Register<SettingsPage, SettingsViewModel>();
    // Register other pages...
}
```

### Initialize the Navigation Frame

Initialize the navigation service with a frame in your main window:

```csharp
public MainWindow()
{
    this.InitializeComponent();
    
    var navigationService = App.Current.Services.GetRequiredService<INavigationService>();
    navigationService.Initialize(this.ContentFrame);
}
```

### Navigate to Pages

Navigate to pages using the navigation service:

```csharp
// Navigate to a page
await navigationService.NavigateToAsync<HomeViewModel>();

// Navigate with transition
await navigationService.NavigateToAsync<SettingsViewModel>(NavigationTransition.DrillIn);

// Go back or forward
if (navigationService.CanGoBack)
    await navigationService.GoBackAsync();
    
if (navigationService.CanGoForward)
    await navigationService.GoForwardAsync();
```

### Create Navigable View Models

Implement the `INavigable` interface in your view models to participate in the navigation flow:

```csharp
public class HomeViewModel : ViewModelBase, INavigable
{
    public async Task<bool> CanNavigateTo(INavigable navigable)
    {
        // Logic to determine if navigation to this view model is allowed
        return true;
    }

    public async Task<bool> CanNavigateFrom(INavigable navigable)
    {
        // Logic to determine if navigation from this view model is allowed
        // For example, check if there are unsaved changes
        return true;
    }
}
```

## Advanced Features

### Clear Navigation History

Clear the back stack to prevent users from navigating back:

```csharp
navigationService.ClearBackStack();
```

### Clear Current Content

Clear the current content in the navigation frame:

```csharp
await navigationService.ClearCurrentContentAsync();
``` 