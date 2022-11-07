namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
    public interface IMessageBusConsumer
    {
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
