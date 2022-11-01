using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class LocalStateTestViewModel : TestViewModel
    {
        public LocalStateTestViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
         
        }

        protected override bool UseLocalState => true;
    }
}