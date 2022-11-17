using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class MessageBusPage : INavigableView
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        private MessageBusViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusPage"/> class.
        /// </summary>
        public MessageBusPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public async Task DisposeAsync()
        {
            this.ViewModel.Dispose();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the navigation.
        /// </summary>
        /// <param name="navigable">The navigable.</param>
        public async Task InitializeNavigationAsync(INavigable navigable)
        {
            this.ViewModel = (MessageBusViewModel)navigable;
            await Task.CompletedTask;
        }
    }
}