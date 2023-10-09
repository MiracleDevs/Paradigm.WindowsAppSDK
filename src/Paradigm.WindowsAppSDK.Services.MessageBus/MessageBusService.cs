using Paradigm.WindowsAppSDK.Services.MessageBus.Models;

namespace Paradigm.WindowsAppSDK.Services.MessageBus
{
    /// <summary>
    /// Provides an agnostic message bus implementation.
    /// </summary>
    /// <seealso cref="IMessageBusService" />
    public class MessageBusService : IMessageBusService
    {
        #region Properties

        /// <summary>
        /// Gets the message handler.
        /// </summary>
        /// <value>
        /// The message handler.
        /// </value>
        private Dictionary<Type, List<Handler>> MessageHandler { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusService"/> class.
        /// </summary>
        public MessageBusService()
        {
            MessageHandler = new Dictionary<Type, List<Handler>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers the specified message listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageListener">The message listener.</param>
        /// <returns></returns>
        public RegistrationToken Register<T>(object consumer, Func<T, Task> messageListener)
        {
            var type = typeof(T);

            if (!this.MessageHandler.ContainsKey(type))
                this.MessageHandler.Add(type, new List<Handler>());

            var handler = new Handler(type, consumer, messageListener);
            this.MessageHandler[type].Add(handler);
            return handler.Token;
        }

        /// <summary>
        /// Unregisters the message listener.
        /// </summary>
        /// <param name="token">The message listener token.</param>
        public void Unregister(RegistrationToken token)
        {
            if (!this.MessageHandler.ContainsKey(token.Type))
                return;

            var handler = this.MessageHandler[token.Type];
            handler.RemoveAll(x => x.Token.Identity == token.Identity);

            if (!handler.Any())
                this.MessageHandler.Remove(token.Type);
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task SendAsync<T>(T message)
        {
            var type = typeof(T);

            if (!this.MessageHandler.ContainsKey(type))
                return;

            var handlers = this.MessageHandler[type].Select(x => x.MessageListener).OfType<Func<T, Task>>().ToArray();

            for (var i = handlers.Length - 1; i >= 0; i--)
            {
                var handler = handlers[i];

                if (handler is null)
                    continue;

                await handler.Invoke(message);
            }
        }

        #endregion
    }
}