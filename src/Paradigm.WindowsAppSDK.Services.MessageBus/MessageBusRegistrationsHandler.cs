﻿using Microsoft.Extensions.DependencyInjection;
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
        private IDictionary<Tuple<Type, int>, RegistrationToken> MessageBusConsumerRegistrations { get; set; }

        /// <summary>
        /// Gets or sets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        private IServiceProvider? ServiceProvider { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="MessageBusRegistrationsHandler"/> class from being created.
        /// </summary>
        private MessageBusRegistrationsHandler()
        {
            MessageBusConsumerRegistrations = new Dictionary<Tuple<Type, int>, RegistrationToken>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public void AddServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Registers the message handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="consumer">The consumer.</param>
        /// <param name="messageListener">The message listener.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ServiceProvider</exception>
        public RegistrationToken RegisterMessageHandler<TMessage>(object consumer, Func<TMessage, Task> messageListener)
        {
            if (ServiceProvider is null)
                throw new ArgumentNullException(nameof(ServiceProvider));

            return RegisterMessageHandler(consumer, ServiceProvider, messageListener);
        }

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
            var key = CreateMessageRegistrationKey(typeof(TMessage), consumer);

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
        /// <exception cref="ArgumentNullException">ServiceProvider</exception>
        public void UnregisterMessageHandler<TMessage>(object consumer)
        {
            if (ServiceProvider is null)
                throw new ArgumentNullException(nameof(ServiceProvider));

            UnregisterMessageHandler<TMessage>(consumer, ServiceProvider);
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
        /// <exception cref="ArgumentNullException">ServiceProvider</exception>
        public void UnregisterMessageHandlers(object consumer)
        {
            if (ServiceProvider is null)
                throw new ArgumentNullException(nameof(ServiceProvider));

            UnregisterMessageHandlers(consumer, ServiceProvider);
        }

        /// <summary>
        /// Unregisters the message handlers.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public void UnregisterMessageHandlers(object consumer, IServiceProvider serviceProvider)
        {
            var consumerHash = consumer.GetHashCode();
            var messageBusService = serviceProvider.GetRequiredService<IMessageBusService>();

            var registrationTokens = MessageBusConsumerRegistrations
                .Where(x => x.Key.Item2 == consumerHash)
                .Select(x => x.Value)
                .Distinct();

            foreach (var token in registrationTokens.Where(t => t.ConsumerHash == consumerHash))
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
            var consumerHash = consumer.GetHashCode();

            return this.MessageBusConsumerRegistrations
                .Where(x => x.Key.Item2 == consumerHash)
                .Select(x => x.Value);
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
            var key = CreateMessageRegistrationKey(messageType, consumer);

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
        private Tuple<Type, int> CreateMessageRegistrationKey(Type messageType, object consumer)
        {
            return Tuple.Create(messageType, consumer.GetHashCode());
        }

        #endregion
    }
}