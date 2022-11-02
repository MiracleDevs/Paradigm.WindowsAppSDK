using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base
{
    public abstract class SampleAppPageViewModelBase : PageViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the log service.
        /// </summary>
        /// <value>
        /// The log service.
        /// </value>

        protected ILogService LogService { get; }

        /// <summary>
        /// Gets the navigation.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        protected INavigationService Navigation { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleAppPageViewModelBase"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected SampleAppPageViewModelBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            LogService = serviceProvider.GetRequiredService<ILogService>();
            Navigation = serviceProvider.GetRequiredService<INavigationService>();
        }

        #endregion
    }
}