using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class TestPage : INavigableView
    {
        private TestViewModel ViewModel { get; set; }

        public TestPage()
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
            this.ViewModel = (TestViewModel)navigable;
            await Task.CompletedTask;
        }
    }
}