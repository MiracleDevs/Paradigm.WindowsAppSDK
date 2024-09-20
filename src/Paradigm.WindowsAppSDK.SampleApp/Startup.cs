using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Dialogs;
using Paradigm.WindowsAppSDK.SampleApp.Views.Dialogs;
using Paradigm.WindowsAppSDK.SampleApp.Views.Pages;
using Paradigm.WindowsAppSDK.Services.Dialog;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using Paradigm.WindowsAppSDK.ViewModels.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;

namespace Paradigm.WindowsAppSDK.SampleApp
{
    internal static class Startup
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start()
        {
            ServiceLocator.Instance.Configure(RegisterDependencies);
            RegisterNavigation();
            RegisterDialogs();
            RegisterConfigurationFiles();
        }

        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        private static void RegisterDependencies(IServiceCollection serviceCollection)
        {
            var mainAssemblies = new[]
            {
                typeof(App).Assembly
            };

            var serviceAssemblies = mainAssemblies.SelectMany(x => x.GetFilteredReferencedAssemblies(typeof(IService))).Distinct();

            serviceCollection.RegisterServices(serviceAssemblies.ToArray());
            serviceCollection.RegisterViewModels(mainAssemblies.ToArray());
            serviceCollection.AddSingleton<ConfigurationProvider>();
        }

        /// <summary>
        /// Registers the navigation.
        /// </summary>
        private static void RegisterNavigation()
        {
            var navigationService = ServiceLocator.Instance.GetRequiredService<INavigationService>();
            navigationService.Register<HomePage, HomeViewModel>();
            navigationService.Register<ApplicationInformationPage, ApplicationInformationViewModel>();
            navigationService.Register<LocalSettingsPage, LocalSettingsViewModel>();
            navigationService.Register<MessageBusPage, MessageBusViewModel>();
            navigationService.Register<ConfigurationPage, ConfigurationViewModel>();
            navigationService.Register<TelemetryPage, TelemetryViewModel>();
            navigationService.Register<FileStoragePage, FileStorageViewModel>();
            navigationService.Register<LoggingPage, LoggingViewModel>();
            navigationService.Register<LocalizationPage, LocalizationViewModel>();
            navigationService.Register<DialogPage, DialogViewModel>();
        }

        /// <summary>
        /// Registers the dialogs.
        /// </summary>
        private static void RegisterDialogs()
        {
            var dialogService = ServiceLocator.Instance.GetRequiredService<IDialogService>();
            dialogService.Register<SampleDialog, SampleDialogViewModel>();
        }

        /// <summary>
        /// Registers the configuration files.
        /// </summary>
        private static void RegisterConfigurationFiles()
        {
            var configurationProvider = ServiceLocator.Instance.GetRequiredService<ConfigurationProvider>();
            configurationProvider.Initialize(Package.Current.InstalledLocation.Path, new List<Tuple<string, bool>>
            {
                new("appsettings.json", false)
            });
        }
    }
}