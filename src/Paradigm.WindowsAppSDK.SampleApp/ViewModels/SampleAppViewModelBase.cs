using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public abstract class SampleAppViewModelBase: ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the log service.
        /// </summary>
        /// <value>
        /// The log service.
        /// </value>

        protected ILogService LogService { get; }
        
        #endregion
        
        #region Constructor

        protected SampleAppViewModelBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.LogService = serviceProvider.GetRequiredService<ILogService>();
        }

        #endregion
    }
}