using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class LoggingPage : INavigableView
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        private LoggingViewModel? ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingPage"/> class.
        /// </summary>
        public LoggingPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public async Task DisposeAsync()
        {
            ViewModel?.Dispose();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the navigation.
        /// </summary>
        /// <param name="navigable">The navigable.</param>
        public async Task InitializeNavigationAsync(INavigable navigable)
        {
            ViewModel = (LoggingViewModel)navigable;
            await Task.CompletedTask;
        }
    }
}
