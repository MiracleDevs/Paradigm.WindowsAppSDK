namespace Paradigm.WindowsAppSDK.Services.MessageBus.Extensions
{
    public static class MessageBusExtensions
    {
        /// <summary>
        /// Registers the message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBusServiceConsumer">The message bus service consumer.</param>
        /// <param name="messageListener">The message listener.</param>
        public static void RegisterMessage<T>(this IMessageBusServiceConsumer messageBusServiceConsumer, Func<T, Task> messageListener)
        {
            messageBusServiceConsumer.MessageBusConsumerRegistrations.Add(typeof(T), messageBusServiceConsumer.MessageBusService.Register(messageListener));
        }

        /// <summary>
        /// Unregisters the message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBusServiceConsumer">The message bus service consumer.</param>
        public static void UnregisterMessage<T>(this IMessageBusServiceConsumer messageBusServiceConsumer)
        {
            UnregisterMessage(messageBusServiceConsumer, typeof(T));
        }

        /// <summary>
        /// Unregisters the messages.
        /// </summary>
        /// <param name="messageBusServiceConsumer">The message bus service consumer.</param>
        public static void UnregisterMessages (this IMessageBusServiceConsumer messageBusServiceConsumer)
        {
            foreach(var registration in messageBusServiceConsumer.MessageBusConsumerRegistrations)
            {
                UnregisterMessage(messageBusServiceConsumer, registration.Key);
            }
        }

        /// <summary>
        /// Sends the message asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBusServiceSender">The message bus service sender.</param>
        /// <param name="message">The message.</param>
        public static async Task SendMessageAsync<T>(this IMessageBusServiceSender messageBusServiceSender, T message)
        {
            await messageBusServiceSender.MessageBusService.SendAsync(message);
        }

        /// <summary>
        /// Unregisters the message.
        /// </summary>
        /// <param name="messageBusServiceConsumer">The message bus service consumer.</param>
        /// <param name="messageType">Type of the message.</param>
        private static void UnregisterMessage(IMessageBusServiceConsumer messageBusServiceConsumer, Type messageType)
        {
            if (!messageBusServiceConsumer.MessageBusConsumerRegistrations.ContainsKey(messageType))
            {
                return;
            }

            messageBusServiceConsumer.MessageBusService.Unregister(messageBusServiceConsumer.MessageBusConsumerRegistrations[messageType]);
            messageBusServiceConsumer.MessageBusConsumerRegistrations.Remove(messageType);
        }
    }
}