namespace Paradigm.WindowsAppSDK.Services.MessageBus.Models
{
    /// <summary>
    /// Stores the message bus registration information that can be used
    /// to unregister the listener.
    /// </summary>
    /// <remarks>
    /// To unregister the a given listener, see <see cref="IMessageBusService.Unregister"/>.
    /// </remarks>
    public class RegistrationToken
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public Guid Identity { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationToken"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public RegistrationToken(Type type)
        {
            Type = type;
            Identity = Guid.NewGuid();
        }
    }
}
