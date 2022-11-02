using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class LocalStateTestPage : INavigableView
    {
        private LocalStateTestViewModel ViewModel { get; set; }

        public LocalStateTestPage()
        {
            this.InitializeComponent();
        }

        public async Task DisposeAsync()
        {
            this.ViewModel.Dispose();
            await Task.CompletedTask;
        }

        public async Task InitializeNavigationAsync(INavigable navigable)
        {
            this.ViewModel = (LocalStateTestViewModel)navigable;
            await this.ViewModel.InitializeAsync();
        }
    }
}