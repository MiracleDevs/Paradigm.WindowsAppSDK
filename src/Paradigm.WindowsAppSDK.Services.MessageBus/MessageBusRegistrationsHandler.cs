using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.MessageBus.Models;

namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
    public sealed class MessageBusRegistrationsHandler
    {
        #region Singleton

        /// <summary>
        /// The internal instance
        /// </summary>
        private static readonly Lazy<MessageBusRegistrationsHandler> InternalInstance = new Lazy<MessageBusRegistrationsHandler>(() => new MessageBusRegistrationsHandler(), true);

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static MessageBusRegistrationsHandler Instance => InternalInstance.Value;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the message bus consumer registrations.
        /// </summary>
        /// <value>
        /// The message bus consumer registrations.
        /// </value>
        private IDictionary<Tuple<Type, Type>, RegistrationToken> MessageBusConsumerRegistrations { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="MessageBusRegistrationsHandler"/> class from being created.
        /// </summary>
        private MessageBusRegistrationsHandler()
        {
            MessageBusConsumerRegistrations = new Dictionary<Tuple<Type, Type>, RegistrationToken>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Registers the message handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="consumer">The consumer.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="messageListener">The message listener.</param>
        /// <returns></returns>
        public RegistrationToken RegisterMessageHandler<TMessage>(object consumer, IServiceProvider serviceProvider, Func<TMessage, Task> messageListener)
        {
            RegistrationToken token;

            var messageBusService = serviceProvider.GetRequiredService<IMessageBusService>();
            var key = CreateMessageRegistrationKey(typeof(TMessage), consumer.GetType());

            if (!MessageBusConsumerRegistrations.ContainsKey(key))
            {
                token = messageBusService.Register(consumer, messageListener);
                MessageBusConsumerRegistrations.Add(key, token);
            }
            else
            {
                token = MessageBusConsumerRegistrations[key];
            }

            return token;
        }

        /// <summary>
        /// Unregisters the message handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="consumer">The consumer.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public void UnregisterMessageHandler<TMessage>(object consumer, IServiceProvider serviceProvider)
        {
            var messageBusService = serviceProvider.GetRequiredService<IMessageBusService>();
            UnregisterMessage(consumer, messageBusService, typeof(TMessage));
        }

        /// <summary>
        /// Unregisters the message handlers.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public void UnregisterMessageHandlers(object consumer, IServiceProvider serviceProvider)
        {
            var consumerType = consumer.GetType();
            var messageBusService = serviceProvider.GetRequiredService<IMessageBusService>();

            var registrationTokens = this.MessageBusConsumerRegistrations
                .Where(messageRegistration => messageRegistration.Key.Item2 == consumerType)
                .Select(messageRegistration => messageRegistration.Value)
                .Distinct();

            foreach (var token in registrationTokens.Where(t => t.ConsumerType == consumerType))
            {
                UnregisterMessage(consumer, messageBusService, token.Type);
            }
        }

        /// <summary>
        /// Gets the registered message handlers.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <returns></returns>
        public IEnumerable<RegistrationToken> GetRegisteredMessageHandlers(object consumer)
        {
            return this.MessageBusConsumerRegistrations
                .Where(messageRegistration => messageRegistration.Key.Item2 == consumer.GetType())
                .Select(messageRegistration => messageRegistration.Value);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Unregisters the message.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <param name="messageBusService">The message bus service.</param>
        /// <param name="messageType">Type of the message.</param>
        private void UnregisterMessage(object consumer, IMessageBusService messageBusService, Type messageType)
        {
            var key = CreateMessageRegistrationKey(messageType, consumer.GetType());

            if (!MessageBusConsumerRegistrations.ContainsKey(key))
                return;

            messageBusService.Unregister(MessageBusConsumerRegistrations[key]);
            MessageBusConsumerRegistrations.Remove(key);
        }

        /// <summary>
        /// Creates the message registration key.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="consumerType">Type of the consumer.</param>
        /// <returns></returns>
        private Tuple<Type, Type> CreateMessageRegistrationKey(Type messageType, Type consumerType)
        {
            return Tuple.Create(messageType, consumerType);
        }

        #endregion
    }
}