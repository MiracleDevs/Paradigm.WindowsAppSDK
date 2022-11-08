namespace Paradigm.WindowsAppSDK.Services.MessageBus.Models
{
    /// <summary>
    /// Represents a message listener handler.
    /// </summary>
    internal class Handler
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public RegistrationToken Token { get; }

        /// <summary>
        /// Gets the message listener.
        /// </summary>
        /// <value>
        /// The message listener.
        /// </value>
        public object MessageListener { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="messageListener">The message listener.</param>
        public Handler(Type type, Type consumerType, object messageListener)
        {
            MessageListener = messageListener;
            Token = new RegistrationToken(type, consumerType);
        }
    }
}