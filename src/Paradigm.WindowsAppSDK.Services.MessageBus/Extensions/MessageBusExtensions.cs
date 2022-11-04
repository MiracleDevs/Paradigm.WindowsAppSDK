namespace Paradigm.WindowsAppSDK.Services.MessageBus.Extensions
{
    public static class MessageBusExtensions
    {
        /// <summary>
        /// Registers the message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBusServiceHandler">The message bus service handler.</param>
        /// <param name="messageListener">The message listener.</param>
        public static void RegisterMessage<T>(this IMessageBusServiceConsumer messageBusServiceHandler, Func<T, Task> messageListener)
        {
            messageBusServiceHandler.MessageBusConsumerRegistrations.Add(typeof(T), messageBusServiceHandler.MessageBusService.Register(messageListener));
        }

        /// <summary>
        /// Unregisters the message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBusServiceHandler">The message bus service handler.</param>
        public static void UnregisterMessage<T>(this IMessageBusServiceConsumer messageBusServiceHandler)
        {
            UnregisterMessage(messageBusServiceHandler, typeof(T));
        }

        /// <summary>
        /// Unregisters the messages.
        /// </summary>
        /// <param name="messageBusServiceHandler">The message bus service handler.</param>
        public static void UnregisterMessages (this IMessageBusServiceConsumer messageBusServiceHandler)
        {
            foreach(var registration in messageBusServiceHandler.MessageBusConsumerRegistrations)
            {
                UnregisterMessage(messageBusServiceHandler, registration.Key);
            }
        }

        /// <summary>
        /// Sends the message asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBusServiceHandler">The message bus service handler.</param>
        /// <param name="message">The message.</param>
        public static async Task SendMessageAsync<T>(this IMessageBusServiceConsumer messageBusServiceHandler, T message)
        {
            await messageBusServiceHandler.MessageBusService.SendAsync(message);
        }

        /// <summary>
        /// Unregisters the message.
        /// </summary>
        /// <param name="messageBusServiceHandler">The message bus service handler.</param>
        /// <param name="messageType">Type of the message.</param>
        private static void UnregisterMessage(IMessageBusServiceConsumer messageBusServiceHandler, Type messageType)
        {
            if (!messageBusServiceHandler.MessageBusConsumerRegistrations.ContainsKey(messageType))
            {
                return;
            }

            messageBusServiceHandler.MessageBusService.Unregister(messageBusServiceHandler.MessageBusConsumerRegistrations[messageType]);
            messageBusServiceHandler.MessageBusConsumerRegistrations.Remove(messageType);
        }
    }
}