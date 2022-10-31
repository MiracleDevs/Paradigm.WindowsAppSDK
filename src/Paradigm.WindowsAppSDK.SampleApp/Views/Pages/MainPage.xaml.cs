using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class MainPage : INavigableView
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitializeNavigationAsync(INavigable navigable)
        {
            throw new NotImplementedException();
        }
    }
}
