using Paradigm.WindowsAppSDK.SampleApp.Messages;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Paradigm.WindowsAppSDK.SampleApp.ViewModels.TestViewModel" />
    public class LocalStateTestViewModel : TestViewModel
    {
        #region Properties 
        /// <summary>
        /// Gets a value indicating whether [use local state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use local state]; otherwise, <c>false</c>.
        /// </value>
        protected override bool UseLocalState => true;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalStateTestViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public LocalStateTestViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        #endregion

        #region Private methods

        /// <summary>
        /// Sends the message asynchronous.
        /// </summary>
        protected override async Task SendMessageAsync()
        {
            var message = new LocalStateContentFolderReadFinishedMessage();

            LogService.Debug($"Sending message {message.GetType()}");
            await this.MessageBusService.SendAsync(message);
        }

        #endregion
    }
}