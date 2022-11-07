namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
    public interface IMessageBusConsumer
    {

        /// <summary>
        /// Gets the message bus service.
        /// </summary>
        /// <value>
        /// The message bus service.
        /// </value>
        IMessageBusService MessageBusService { get; }

        /// <summary>
        /// Registers the service bus message handlers.
        /// </summary>
        void RegisterServiceBusMessageHandlers();

        /// <summary>
        /// Uns the register service bus message handlers.
        /// </summary>
        void UnRegisterServiceBusMessageHandlers();
    }
}
