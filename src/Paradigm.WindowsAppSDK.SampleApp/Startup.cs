﻿using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using Paradigm.WindowsAppSDK.ViewModels.Extensions;

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
        }

        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        private static void RegisterDependencies(IServiceCollection serviceCollection)
        {
            var assemblies = new[]
            {
                typeof(App).Assembly,
                typeof(INavigationService).Assembly
            };

            serviceCollection.RegisterServices(assemblies);
            serviceCollection.RegisterViewModels(assemblies);
        }

        /// <summary>
        /// Registers the navigation.
        /// </summary>
        private static void RegisterNavigation()
        {
            var navigationService = ServiceLocator.Instance.GetRequiredService<INavigationService>();
            //navigationService.Register<MainWindow, ViewModels.Main.MainViewModel>();
        }
    }
}