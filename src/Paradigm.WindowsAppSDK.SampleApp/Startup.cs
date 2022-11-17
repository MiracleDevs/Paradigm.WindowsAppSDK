using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.SampleApp.Views.Pages;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using Paradigm.WindowsAppSDK.ViewModels.Extensions;
using System.Linq;

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
            var mainAssemblies = new[]
            {
                typeof(App).Assembly
            };
            
            var serviceAssemblies = mainAssemblies.SelectMany(x => x.GetFilteredReferencedAssemblies(typeof(IService))).Distinct();

            serviceCollection.RegisterServices(serviceAssemblies.ToArray());
            serviceCollection.RegisterViewModels(mainAssemblies.ToArray());
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

            navigationService.Register<TestPage, TestViewModel>();
            navigationService.Register<LocalStateTestPage, LocalStateTestViewModel>();
        }
    }
}
