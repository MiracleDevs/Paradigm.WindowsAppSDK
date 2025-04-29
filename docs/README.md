# Paradigm.WindowsAppSDK Documentation

## Overview

Paradigm.WindowsAppSDK is a comprehensive framework that provides base classes and services for Windows App SDK applications using WinUI interfaces. The framework implements common design patterns and best practices to simplify the development of Windows applications.

## Architecture

The framework is designed with a modular architecture, where each component is a separate NuGet package that can be used independently or together. The architecture follows these principles:

- **Service-based design**: Core functionality is provided through services that implement the `IService` interface
- **Dependency Injection**: Services are registered and resolved using a dependency injection container
- **MVVM Pattern**: Support for Model-View-ViewModel pattern with base classes for ViewModels
- **Navigation**: A flexible navigation service with support for navigation history and transitions
- **Configurable**: Extensive configuration options through various configuration services

## Core Components

| Component | Description |
|-----------|-------------|
| [Interfaces](interfaces.md) | Core interfaces used throughout the framework |
| [Application Information](application-information.md) | Service to access application metadata |
| [Configuration](configuration.md) | Services for application configuration management |
| [Dialog](dialog.md) | Dialog service for managing application dialogs |
| [File Storage](file-storage.md) | Service for file system operations |
| [Localization](localization.md) | Localization services for multi-language support |
| [Local Settings](local-settings.md) | Services for managing application settings |
| [Logging](logging.md) | Logging services for application diagnostics |
| [Message Bus](message-bus.md) | Message bus for loosely coupled communication |
| [Navigation](navigation.md) | Navigation service for managing application navigation |
| [Telemetry](telemetry.md) | Telemetry service for collecting application metrics |
| [ViewModels](viewmodels.md) | Base classes for implementing the MVVM pattern |
| [XAML](xaml.md) | XAML helpers, converters, and extensions |

## Getting Started

To start using Paradigm.WindowsAppSDK, you can install the NuGet packages for the services you need. For example:

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Navigation
Install-Package Paradigm.WindowsAppSDK.ViewModels
Install-Package Paradigm.WindowsAppSDK.Xaml
```

For more information on each component, please refer to the specific documentation pages linked above.

## Sample Application

The framework includes a sample application that demonstrates how to use the various components. See the [Sample Application](sample-application.md) documentation for more details. 