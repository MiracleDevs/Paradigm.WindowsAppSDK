using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Linq;

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

        /// <summary>
        /// Gets the message bus service.
        /// </summary>
        /// <value>
        /// The message bus service.
        /// </value>
        protected IMessageBusService MessageBusService { get; private set; }

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
            MessageBusService = serviceProvider.GetRequiredService<IMessageBusService>();

            RegisterServiceBusMessageHandlers();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            UnRegisterServiceBusMessageHandlers();
            base.Dispose();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Registers the service bus message handlers.
        /// </summary>
        public virtual void RegisterServiceBusMessageHandlers()
        {
        }

        /// <summary>
        /// Uns the register service bus message handlers.
        /// </summary>
        public virtual void UnRegisterServiceBusMessageHandlers()
        {
            LogService.Debug($"Unregistering {MessageBusRegistrationsHandler.Instance.GetRegisteredMessageHandlers(this).Count()} message registrations from {this.GetType().FullName}");
            MessageBusRegistrationsHandler.Instance.UnregisterMessageHandlers(this);
            LogService.Debug($"Found {MessageBusRegistrationsHandler.Instance.GetRegisteredMessageHandlers(this).Count()} message registrations from {this.GetType().FullName}");
        }

        #endregion
    }
}