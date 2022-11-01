using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class MainPage : INavigableView
    {
        private MainViewModel ViewModel { get; set; }

        public MainPage()
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
            this.ViewModel = (MainViewModel)navigable;
            await Task.CompletedTask;
        }
    }
}
