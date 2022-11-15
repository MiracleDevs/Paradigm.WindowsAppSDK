# Paradigm.WindowsAppSDK
A framework that provides base classes for WindowsAppSDK applications using WinUI interfaces.

## Nuget Packages
| Library    | Nuget | Install
|-|-|-|
| Application Information       | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.ApplicationInformation.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.ApplicationInformation/)            | `Install-Package Paradigm.WindowsAppSDK.Services.ApplicationInformation` |
| File Storage      | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.FileStorage.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.FileStorage/)      | `Install-Package Paradigm.WindowsAppSDK.Services.FileStorage` |
| Interfaces | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Interfaces.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Interfaces/) | `Install-Package Paradigm.WindowsAppSDK.Services.Interfaces` |
| Legacy Configuration | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.LegacyConfiguration.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.LegacyConfiguration/)  | `Install-Package Paradigm.WindowsAppSDK.Services.LegacyConfiguration` |
| Localization | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Localization.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Localization/)  | `Install-Package Paradigm.WindowsAppSDK.Services.Localization` |
| Local Settings | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.LocalSettings.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.LocalSettings/)  | `Install-Package Paradigm.WindowsAppSDK.Services.LocalSettings` |
| Logging | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Logging.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Logging/)  | `Install-Package Paradigm.WindowsAppSDK.Services.Logging` |
| Message Bus | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.MessageBus.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.MessageBus/)  | `Install-Package Paradigm.WindowsAppSDK.Services.MessageBus` |
| Navigation | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Navigation.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Navigation/)  | `Install-Package Paradigm.WindowsAppSDK.Services.Navigation` |
| Telemetry | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Services.Telemetry.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Services.Telemetry/)  | `Install-Package Paradigm.WindowsAppSDK.Services.Telemetry` |
| ViewModels | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.ViewModels.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.ViewModels/)  | `Install-Package Paradigm.WindowsAppSDK.ViewModels` |
| XAML | [![NuGet](https://img.shields.io/nuget/v/Paradigm.WindowsAppSDK.Xaml.svg)](https://www.nuget.org/packages/Paradigm.WindowsAppSDK.Xaml/)  | `Install-Package Paradigm.WindowsAppSDK.Xaml` |

## Nuget publish process
After modifying the solution you can change the version by executing
```shell
$ cd ./build
$ ./increment.version.sh "1.0.0" "1.0.1"
```
where the first argument ("1.0.0") is the current version and the second one ("1.0.1") is the new version number.


To publish to nuget you need to execute the following script
```shell
$ cd ./build
$ ./publish.nuget.sh "{nuget-secret-key}"
```

## Change log

Version `1.0.0`
- Uploaded first version of the Paradigm.WindowsAppSDK.