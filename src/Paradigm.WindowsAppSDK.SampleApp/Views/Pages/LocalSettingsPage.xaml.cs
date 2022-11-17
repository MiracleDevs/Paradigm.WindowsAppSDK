using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class LocalSettingsPage : INavigableView
    {
        private LocalSettingsViewModel ViewModel { get; set; }

        public LocalSettingsPage()
        {
            this.InitializeComponent();
        }

        public async Task DisposeAsync()
        {
            ViewModel.Dispose();
            await Task.CompletedTask;
        }

        public async Task InitializeNavigationAsync(INavigable navigable)
        {
            ViewModel = (LocalSettingsViewModel)navigable;
            ViewModel.LoadLocalSettings();
            await Task.CompletedTask;
        }
    }
}