using System;

namespace Paradigm.WindowsAppSDK.SampleApp.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MessageBase
    {

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>
        /// The delay.
        /// </value>
        public int Delay { get; protected set; }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Guid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        public MessageBase()
        {
            Guid = Guid.NewGuid();
            Delay = 50;
        }
    }
}
