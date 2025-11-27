# Sample Application

The Paradigm.WindowsAppSDK.SampleApp is a comprehensive demonstration of how to use the various components of the Paradigm.WindowsAppSDK framework in a real-world application.

## Overview

The sample application showcases the different services and features provided by the framework:

- Navigation between pages
- Dialog handling
- Application configuration
- Localization
- Local settings management
- Logging
- File storage operations
- Message bus for communication between components
- Telemetry tracking

## Architecture

The sample application follows the MVVM (Model-View-ViewModel) pattern and is structured as follows:

- **App.xaml.cs**: Application entry point and initialization
- **Startup.cs**: Registration of services, navigation, and configuration
- **Models**: Data models used in the application
- **ViewModels**: View models that implement application logic
- **Views**: XAML views that represent the UI

## Key Components

### Startup

The `Startup` class in the sample application demonstrates how to configure and register the various services:

```csharp
internal static class Startup
{
    public static void Start()
    {
        ServiceLocator.Instance.Configure(RegisterDependencies);
        RegisterNavigation();
        RegisterDialogs();
        RegisterConfigurationFiles();
    }

    private static void RegisterDependencies(IServiceCollection serviceCollection)
    {
        var mainAssemblies = new[] { typeof(App).Assembly };
        var serviceAssemblies = mainAssemblies.SelectMany(x => x.GetFilteredReferencedAssemblies(typeof(IService))).Distinct();

        serviceCollection.RegisterServices(serviceAssemblies.ToArray());
        serviceCollection.RegisterViewModels(mainAssemblies.ToArray());
        serviceCollection.AddSingleton<ConfigurationProvider>();
    }

    private static void RegisterNavigation()
    {
        var navigationService = ServiceLocator.Instance.GetRequiredService<INavigationService>();
        navigationService.Register<HomePage, HomeViewModel>();
        navigationService.Register<ApplicationInformationPage, ApplicationInformationViewModel>();
        // Other registrations...
    }

    private static void RegisterDialogs()
    {
        var dialogService = ServiceLocator.Instance.GetRequiredService<IDialogService>();
        dialogService.Register<SampleDialog, SampleDialogViewModel>();
    }

    private static void RegisterConfigurationFiles()
    {
        var configurationProvider = ServiceLocator.Instance.GetRequiredService<ConfigurationProvider>();
        configurationProvider.Initialize(Package.Current.InstalledLocation.Path, new List<Tuple<string, bool>>
        {
            new("appsettings.json", false)
        });
    }
}
```

### Application Initialization

The `App` class demonstrates how to initialize the application and set up the main window:

```csharp
public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Startup.Start();

        var window = new MainWindow();
        window.Activate();

        var navigationService = ServiceLocator.Instance.GetRequiredService<INavigationService>();
        navigationService.NavigateToAsync<HomeViewModel>();
    }
}
```

## Feature Demonstrations

The sample application includes multiple pages that demonstrate different features:

### Home Page

A dashboard-style page that provides navigation to the different feature demonstrations.

### Navigation

The application demonstrates the navigation service by allowing users to navigate between different pages, with support for back/forward navigation.

### Dialog Service

The Dialog page demonstrates how to create and show dialogs using the Dialog service.

### Configuration

The Configuration page demonstrates how to read configuration settings from an `appsettings.json` file.

### Local Settings

The Local Settings page demonstrates how to save and load application settings that persist between sessions.

### Message Bus

The Message Bus page demonstrates how to use the Message Bus for loose coupling between components.

### Application Information

The Application Information page demonstrates how to access information about the application, such as version and publisher.

### File Storage

The File Storage page demonstrates how to perform file operations using the File Storage service.

### Logging

The Logging page demonstrates how to log messages at different levels using the Logging service.

### Localization

The Localization page demonstrates how to use the Localization service for multi-language support.

### Telemetry

The Telemetry page demonstrates how to track events and metrics using the Telemetry service.

## Running the Sample Application

To run the sample application:

1. Open the `Paradigm.WindowsAppSDK.SampleApp.slnx` solution in Visual Studio
2. Set `Paradigm.WindowsAppSDK.SampleApp` as the startup project
3. Build and run the application

## Learning from the Sample

The sample application is designed to be a learning resource. You can:

1. Examine the code to understand how to use the different services
2. See how the MVVM pattern is implemented using the framework
3. Learn best practices for structuring a Windows App SDK application
4. Use it as a starting point for your own applications