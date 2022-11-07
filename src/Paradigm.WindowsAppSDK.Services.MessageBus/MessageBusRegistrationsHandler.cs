using Paradigm.WindowsAppSDK.Services.MessageBus.Models;

namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
  
    public sealed class MessageBusRegistrationsHandler
    {
        #region Singleton

        private static readonly Lazy<MessageBusRegistrationsHandler> InternalInstance = new Lazy<MessageBusRegistrationsHandler>(() => new MessageBusRegistrationsHandler(), true);

        public static MessageBusRegistrationsHandler Instance => InternalInstance.Value;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the message bus consumer registrations.
        /// </summary>
        /// <value>
        /// The message bus consumer registrations.
        /// </value>
        private IDictionary<Type, IDictionary<Type, RegistrationToken>> MessageBusConsumerRegistrations { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="MessageBusRegistrationsHandler"/> class from being created.
        /// </summary>
        private MessageBusRegistrationsHandler()
        {
            MessageBusConsumerRegistrations = new Dictionary<Type, IDictionary<Type, RegistrationToken>>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Registers the message handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="consumer">The consumer.</param>
        /// <param name="messageListener">The message listener.</param>
        /// <returns></returns>
        public RegistrationToken RegisterMessageHandler<TMessage>(IMessageBusConsumer consumer, Func<TMessage, Task> messageListener)
        {
            RegistrationToken token;

            var consumerType = consumer.GetType();

            if (MessageBusConsumerRegistrations.TryGetValue(typeof(TMessage), out var registrations))
            {
                if (!registrations.ContainsKey(consumerType))
                {

                    registrations.Add(consumerType, consumer.MessageBusService.Register(messageListener));
                }

                token = registrations[consumerType];
            }
            else
            {
                token = consumer.MessageBusService.Register(messageListener);

                MessageBusConsumerRegistrations.Add(typeof(TMessage), new Dictionary<Type, RegistrationToken>
                {
                    { consumerType, token }
                });
            }

            return token;
        }

        /// <summary>
        /// Unregisters the message handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="consumer">The consumer.</param>
        public void UnregisterMessageHandler<TMessage>(IMessageBusConsumer consumer)
        {
            UnregisterMessage(consumer, typeof(TMessage));
        }

        /// <summary>
        /// Unregisters the message handlers.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        public void UnregisterMessageHandlers(IMessageBusConsumer consumer)
        {
            var consumerType = consumer.GetType();

            var registrationMessageTypes = this.MessageBusConsumerRegistrations
            .Where(messageRegistration => messageRegistration.Value.Any(consumerRegistration => consumerRegistration.Key == consumerType));

            foreach (var messageType in registrationMessageTypes)
            {
                foreach (var consumerRegistration in messageType.Value)
                {
                    if (consumerRegistration.Key == consumerType)
                    {
                        UnregisterMessage(consumer, messageType.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the registered message handlers.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <returns></returns>
        public IEnumerable<RegistrationToken> GetRegisteredMessageHandlers(IMessageBusConsumer consumer)
        {
            var consumerType = consumer.GetType();

            return this.MessageBusConsumerRegistrations
                .Where(messageRegistration => messageRegistration.Value.Any(consumerRegistration => consumerRegistration.Key == consumerType))
                .SelectMany(consumerRegistration => consumerRegistration.Value.Where(a => a.Key == consumerType))
                .Select(messageRegistration => messageRegistration.Value);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Unregisters the message.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <param name="messageType">Type of the message.</param>
        private void UnregisterMessage(IMessageBusConsumer consumer, Type messageType)
        {
            var consumerType = consumer.GetType();

            if (!MessageBusConsumerRegistrations.ContainsKey(messageType))
            {
                return;
            }

            if (MessageBusConsumerRegistrations[messageType].Any(r => r.Key == consumerType))
            {
                if (MessageBusConsumerRegistrations[messageType].TryGetValue(consumerType, out var token))
                {
                    consumer.MessageBusService.Unregister(token);
                }

                MessageBusConsumerRegistrations[messageType].Remove(consumerType);
            }

            if (!MessageBusConsumerRegistrations[messageType].Any())
            {
                MessageBusConsumerRegistrations.Remove(messageType);
            }
        }

        #endregion

    }
}
