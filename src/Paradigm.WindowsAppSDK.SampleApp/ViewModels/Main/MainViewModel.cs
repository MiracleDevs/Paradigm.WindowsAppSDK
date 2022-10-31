using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
         
        }

        public async Task ExecuteActionAsync(string content)
        {
            this.LogService.Information($"Executed action {content} from {GetType().FullName}");
            await Task.CompletedTask;
        }
    }
}