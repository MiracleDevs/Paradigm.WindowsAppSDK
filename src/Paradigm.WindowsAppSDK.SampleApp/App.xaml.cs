using Microsoft.UI.Xaml;
using Paradigm.WindowsAppSDK.Services.ApplicationInformation;
using Paradigm.WindowsAppSDK.Services.LocalSettings;
using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using Paradigm.WindowsAppSDK.Xaml.Extensions;
using Windows.Storage;

namespace Paradigm.WindowsAppSDK.SampleApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the main window.
        /// </summary>
        /// <value>
        /// The main window.
        /// </value>
        public static MainWindow MainWindow { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            Startup.Start();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            ServiceLocator.Instance.GetRequiredService<ILogService>().Initialize(ApplicationData.Current.TemporaryFolder.Path);
            ServiceLocator.Instance.GetRequiredService<IFileStorageService>().Initialize(new Services.FileStorage.FileStorageSettings
            {
                LocalFolderPath = ApplicationData.Current.LocalFolder.Path,
                InstallationFolderPath = $"{Windows.ApplicationModel.Package.Current.InstalledLocation.Path}\\Assets",
                LocalBaseUri = "ms-appdata:///local",
                InstallationBaseUri = "ms-appx:///Assets"
            });

            MessageBusRegistrationsHandler.Instance.AddServiceProvider(ServiceLocator.Instance.ServiceProvider);

            MainWindow = new MainWindow();

            ServiceLocator.Instance.GetRequiredService<INavigationService>().Initialize(MainWindow.GetNavigationFrame());
            ServiceLocator.Instance.GetRequiredService<ILocalSettingsService>().Initialize(ApplicationData.Current.LocalSettings.Values);
            ServiceLocator.Instance.GetRequiredService<IApplicationInformationService>().Initialize(MainWindow.GetWindowId());

            MainWindow.Activate();
        }
    }
}