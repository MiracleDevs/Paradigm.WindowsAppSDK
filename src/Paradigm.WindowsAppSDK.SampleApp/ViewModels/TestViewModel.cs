using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        private INavigationService Navigation { get; }

        public TestViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Navigation = serviceProvider.GetRequiredService<INavigationService>();
        }

        public async Task ExecuteActionAsync()
        {
            if (await Navigation.GoBackAsync())
                LogService.Information("Executed back navigation");
        }
    }
}