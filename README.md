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

For the full change history, see [CHANGELOG.md](./CHANGELOG.md).
