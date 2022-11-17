using Microsoft.UI.Xaml;
using Paradigm.WindowsAppSDK.Services.ApplicationInformation;
using Paradigm.WindowsAppSDK.Services.LocalSettings;
using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using Windows.Storage;

namespace Paradigm.WindowsAppSDK.SampleApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private MainWindow m_window;

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
                InstallationFolderPath = $"{Windows.ApplicationModel.Package.Current.InstalledLocation.Path}\\Assets"
            });

            m_window = new MainWindow();

            ServiceLocator.Instance.GetRequiredService<INavigationService>().Initialize(m_window.GetNavigationFrame());
            ServiceLocator.Instance.GetRequiredService<ILocalSettingsService>().Initialize(ApplicationData.Current.LocalSettings.Values);
            ServiceLocator.Instance.GetRequiredService<IApplicationInformationService>().Initialize(WinRT.Interop.WindowNative.GetWindowHandle(m_window));

            m_window.Activate();
        }
    }
}