using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MainWindowViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the message text.
        /// </summary>
        /// <value>
        /// The message text.
        /// </value>
        public string MessageText { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MainWindowViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            MessageText = "[messages content will be displayed here]";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates to page.
        /// </summary>
        public async Task NavigateToPageAsync(string typeName)
        {
            var type = typeof(MainWindowViewModel).Assembly.GetType($"Paradigm.WindowsAppSDK.SampleApp.ViewModels.{typeName}ViewModel");
            await Navigation.NavigateToAsync(type, Services.Navigation.NavigationTransition.Drill);
        }

        /// <summary>
        /// Registers the service bus message handlers.
        /// </summary>
        public override void RegisterServiceBusMessageHandlers()
        {
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<SampleMessage>(this, OnSampleMessageReceivedAsync);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [sample message received].
        /// </summary>
        /// <param name="message">The message.</param>
        private async Task OnSampleMessageReceivedAsync(SampleMessage message)
        {
            MessageText = message.Text;
            OnPropertyChanged(nameof(MessageText));
            await Task.CompletedTask;
        }

        #endregion
    }
}