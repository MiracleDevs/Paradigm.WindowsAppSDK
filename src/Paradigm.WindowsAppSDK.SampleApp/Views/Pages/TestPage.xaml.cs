using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class TestPage : INavigableView
    {
        public TestPage()
        {
            this.InitializeComponent();
        }

        public Task DisposeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task InitializeNavigationAsync(INavigable navigable)
        {
            throw new System.NotImplementedException();
        }
    }
}
