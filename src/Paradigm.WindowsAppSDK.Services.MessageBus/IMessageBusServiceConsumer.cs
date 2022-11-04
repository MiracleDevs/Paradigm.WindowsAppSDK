using Paradigm.WindowsAppSDK.Services.MessageBus.Models;

namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
    public interface IMessageBusServiceConsumer
    {
        IMessageBusService MessageBusService { get; }

        IDictionary<Type, RegistrationToken> MessageBusConsumerRegistrations { get;}
    }
}
