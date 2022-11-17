using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using Paradigm.WindowsAppSDK.Services.MessageBus.Models;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Paradigm.WindowsAppSDK.SampleApp
{
    public sealed partial class MainWindow : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public MainWindowViewModel ViewModel { get; }

        /// <summary>
        /// Gets the message bus.
        /// </summary>
        /// <value>
        /// The message bus.
        /// </value>
        private IMessageBusService MessageBus { get; }

        /// <summary>
        /// Gets the toggle display messages viewer token.
        /// </summary>
        /// <value>
        /// The toggle display messages viewer token.
        /// </value>
        private RegistrationToken ToggleDisplayMessagesViewerToken { get; }

        #endregion

        #region Constructor  

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
                return;

            MessageBus = ServiceLocator.Instance.GetRequiredService<IMessageBusService>();
            ViewModel = ServiceLocator.Instance.GetRequiredService<MainWindowViewModel>();
            Title = AppInfo.Current.DisplayInfo.DisplayName;
            ToggleDisplayMessagesViewerToken = MessageBus.Register<ToggleDisplayMessagesViewerMessage>(this, OnToggleDisplayMessagesViewerAsync);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            MessageBus.Unregister(ToggleDisplayMessagesViewerToken);
            ViewModel.Dispose();
            NavigationFrame.Dispose();
        }

        /// <summary>
        /// Gets the navigation frame.
        /// </summary>
        /// <returns></returns>
        public INavigationFrame GetNavigationFrame() => NavigationFrame;

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [toggle display messages viewer].
        /// </summary>
        /// <param name="message">The message.</param>
        private async Task OnToggleDisplayMessagesViewerAsync(ToggleDisplayMessagesViewerMessage message)
        {
            MessageViewer.Visibility = MessageViewer.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            await Task.CompletedTask;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Called when [navigation view selection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs"/> instance containing the event data.</param>
        private async void OnNavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            await ViewModel.NavigateToPageAsync((args.SelectedItem as NavigationViewItem).Tag?.ToString());
        }

        /// <summary>
        /// Handles the Closed event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="WindowEventArgs"/> instance containing the event data.</param>
        private void OnClosed(object sender, WindowEventArgs args)
        {
            Dispose();
        }

        #endregion
    }
}