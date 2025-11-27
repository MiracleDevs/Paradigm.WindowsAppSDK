# Paradigm.WindowsAppSDK

A framework that provides base classes for WindowsAppSDK applications using WinUI interfaces.

## Documentation

Comprehensive documentation for the Paradigm.WindowsAppSDK framework is available in the [docs folder](./docs/README.md). The documentation covers:

| Component                                                    | Description                                            |
| ------------------------------------------------------------ | ------------------------------------------------------ |
| [Interfaces](./docs/interfaces.md)                           | Core interfaces used throughout the framework          |
| [Application Information](./docs/application-information.md) | Service to access application metadata                 |
| [Configuration](./docs/configuration.md)                     | Services for application configuration management      |
| [Dialog](./docs/dialog.md)                                   | Dialog service for managing application dialogs        |
| [File Storage](./docs/file-storage.md)                       | Service for file system operations                     |
| [Localization](./docs/localization.md)                       | Localization services for multi-language support       |
| [Local Settings](./docs/local-settings.md)                   | Services for managing application settings             |
| [Logging](./docs/logging.md)                                 | Logging services for application diagnostics           |
| [Message Bus](./docs/message-bus.md)                         | Message bus for loosely coupled communication          |
| [Navigation](./docs/navigation.md)                           | Navigation service for managing application navigation |
| [Telemetry](./docs/telemetry.md)                             | Telemetry service for collecting application metrics   |
| [ViewModels](./docs/viewmodels.md)                           | Base classes for implementing the MVVM pattern         |
| [XAML](./docs/xaml.md)                                       | XAML helpers, converters, and extensions               |

## Nuget Packages

| Library                 | Nuget                                                                                                                                                                                         | Install                                                                  |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| Application Information | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.ApplicationInformation.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.ApplicationInformation/) | `Install-Package Paradigm.WindowsAppSDK.Services.ApplicationInformation` |
| Dialog                  | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Dialog.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Dialog/)                                 | `Install-Package Paradigm.WindowsAppSDK.Services.Dialog`                 |
| File Storage            | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.FileStorage.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.FileStorage/)                       | `Install-Package Paradigm.WindowsAppSDK.Services.FileStorage`            |
| Interfaces              | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Interfaces.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Interfaces/)                         | `Install-Package Paradigm.WindowsAppSDK.Services.Interfaces`             |
| Configuration           | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Configuration.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Configuration/)                   | `Install-Package Paradigm.WindowsAppSDK.Services.Configuration`          |
| Localization            | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Localization.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Localization/)                     | `Install-Package Paradigm.WindowsAppSDK.Services.Localization`           |
| Local Settings          | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.LocalSettings.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.LocalSettings/)                   | `Install-Package Paradigm.WindowsAppSDK.Services.LocalSettings`          |
| Logging                 | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Logging.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Logging/)                               | `Install-Package Paradigm.WindowsAppSDK.Services.Logging`                |
| Message Bus             | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.MessageBus.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.MessageBus/)                         | `Install-Package Paradigm.WindowsAppSDK.Services.MessageBus`             |
| Navigation              | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Navigation.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Navigation/)                         | `Install-Package Paradigm.WindowsAppSDK.Services.Navigation`             |
| Telemetry               | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Telemetry.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Telemetry/)                           | `Install-Package Paradigm.WindowsAppSDK.Services.Telemetry`              |
| ViewModels              | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.ViewModels.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.ViewModels/)                                           | `Install-Package Paradigm.WindowsAppSDK.ViewModels`                      |
| XAML                    | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Xaml.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Xaml/)                                                       | `Install-Package Paradigm.WindowsAppSDK.Xaml`                            |

## Nuget publish process

After modifying the solution you can change the version by executing

```shell
$ cd ./build
$ ./increment.version.sh "1.1.0" "1.1.1"
```

where the first argument ("1.1.0") is the current version and the second one ("1.1.1") is the new version number.

To publish to nuget you need to execute the following script

```shell
$ cd ./build
$ ./publish.nuget.sh "{nuget-secret-key}"
```

## Change log

Version `1.4.0`

- Upgrade to use NET10.

Version `1.3.4`

- Updated Microsoft.WindowsAppSDK and libraries.

Version `1.3.3`

- Updated packages icon. Updated Microsoft.WindowsAppSDK and libraries.

Version `1.3.2`

- Implemented Nuget CPM. Fixed packages license information.

Version `1.3.1`

- Updated Microsoft.WindowsAppSDK and libraries.

Version `1.3.0`

- Upgrade to WindowsAppSDK 1.7.

Version `1.2.2`

- Updated Microsoft.WindowsAppSDK and WinUIEx versions.

Version `1.2.1`

- Modified libraries to be AOT compatible.

Version `1.2.0`

- Upgrade to use NET9 and WindowsAppSDK 1.6.

Version `1.1.10`

- Modified LegacyConfigurationService to support content from multiple configuration files.

Version `1.1.9`

- Modified TelemetryService to restore the session id if changed while debouncing an event.

Version `1.1.7`

- Modified TelemetryService to have the session id as a public readonly property.

Version `1.1.6`

- Fixed Microsoft.Windows.SDK.BuildTools package issue in Paradigm.WindowsAppSDK.Services.ApplicationInformation.

Version `1.1.5`

- Fixed Microsoft.Windows.SDK.BuildTools package issue.

Version `1.1.4`

- Updated Microsoft.WindowsAppSDK version.

Version `1.1.3`

- Added DebounceHandler class to Paradigm.WindowsAppSDK.ViewModels. Fixed issue in LegacyConfigurationService.

Version `1.1.2`

- Removed IsDemoModeEnabled property call. Updated Microsoft.WindowsAppSDK version.

Version `1.1.1`

- Added new converters. Updated Microsoft.WindowsAppSDK version.

Version `1.1.0`

- Upgrade to use NET8 and WindowsAppSDK 1.5.

Version `1.0.25`

- Modified TelemetryService to send a session id when provided.

Version `1.0.24`

- Refactor TelemetryService to keep a dictionary with the requested connection strings.

Version `1.0.23`

- Modified TelemetryService to allow to prevent event debounce.

Version `1.0.22`

- Modified LogService to allow to archive previous log file.

Version `1.0.21`

- Modified LegacyConfigurationService to receive JsonSerializerOptions parameter in GetObject method.

Version `1.0.20`

- Modified LogService to register inner exceptions and stack trace (Paradigm.WindowsAppSDK.Services.Logging).

Version `1.0.19`

- Updated WindowsAppSDK and WinUIEx packages (Paradigm.WindowsAppSDK.Xaml).

Version `1.0.18`

- Added support for transitions in NavigationService.

Version `1.0.17`

- Fixed event properties processing race conditions if debounce is not activated.

Version `1.0.16`

- Added Hide method to IDialogView.

Version `1.0.15`

- Fixed BoolToParameterConverter decimal symbol culture bug.

Version `1.0.14`

- Modified FileStorageService settings to allow to prevent creating new directories when not exist.

Version `1.0.13`

- Modified NavigationService to allow to clear the current content. Updated dependencies.

Version `1.0.12`

- Fix in LegacyConfigurationService to allow comments in the JSON files.

Version `1.0.11`

- Fixed bug in MessageBusRegistrationsHandler. Modified TelemetryService to allow different connection strings. Modified LegacyConfigurationService to allow object values.

Version `1.0.10`

- Modified MessageBusRegistrationsHandler to include a ServiceProvider instance.

Version `1.0.9`

- Added GetLocalFileUri method in FileStorageService

Version `1.0.8`

- Added new converters and extension methods. Updated Microsoft.WindowsAppSDK version.

Version `1.0.7`

- Fixed bug in ObjectToVisibilityConverter (Paradigm.WindowsAppSDK.Xaml).

Version `1.0.6`

- Added new converters and controls to Paradigm.WindowsAppSDK.Xaml.

Version `1.0.5`

- Adjustments in FileStorageService to create the directory if not exists when save. Added WindowExtensions class to Paradigm.WindowsAppSDK.Xaml.

Version `1.0.4`

- LogService and ApplicationInformationService adjustments. Adjusted XAML converters. Fixed warnings.

Version `1.0.3`

- NavigationRootFrame control refactor.

Version `1.0.2`

- Added NavigationRootFrame control to Paradigm.WindowsAppSDK.Xaml.

Version `1.0.1`

- Added Paradigm.WindowsAppSDK libraries descriptions.

Version `1.0.0`

- Uploaded first version of the Paradigm.WindowsAppSDK.
