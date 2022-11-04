using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Logging;
using Paradigm.WindowsAppSDK.Services.MessageBus.Models;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Collections.Generic;
using Paradigm.WindowsAppSDK.Services.MessageBus.Extensions;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base
{
    public abstract class SampleAppPageViewModelBase : PageViewModelBase, IMessageBusServiceConsumer
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
        public IMessageBusService MessageBusService { get; private set; }

        /// <summary>
        /// Gets the message bus consumer registrations.
        /// </summary>
        /// <value>
        /// The message bus consumer registrations.
        /// </value>
        public IDictionary<Type, RegistrationToken> MessageBusConsumerRegistrations { get; private set; }


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

            MessageBusService = serviceProvider.GetService<IMessageBusService>();
            MessageBusConsumerRegistrations = new Dictionary<Type, RegistrationToken>();

            RegisterServiceBusMessageHandlers();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            LogService.Debug($"Unregistering {this.MessageBusConsumerRegistrations.Count} message registrations from {this.GetType().FullName}");
            this.UnregisterMessages();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Registers the service bus message handlers.
        /// </summary>
        protected virtual void RegisterServiceBusMessageHandlers()
        {

        }

        #endregion
    }
}