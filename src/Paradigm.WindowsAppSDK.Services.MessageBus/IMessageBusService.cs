using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.MessageBus.Models;

namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
    /// <summary>
    /// Provides an interface for a message bus service.
    /// </summary>
    public interface IMessageBusService : IService
    {
        /// <summary>
        /// Registers a message listener.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="messageListener">The message listener.</param>
        /// <returns></returns>
        RegistrationToken Register<T>(Func<T, Task> messageListener);

        /// <summary>
        /// Unregisters the message listener.
        /// </summary>
        /// <param name="token">The message listener token.</param>
        void Unregister(RegistrationToken token);

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="message">The message.</param>
        Task SendAsync<T>(T message);
    }
}
