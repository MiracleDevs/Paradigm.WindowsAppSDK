using Microsoft.UI.Xaml;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels;
using System;

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
        public MainWindowViewModel ViewModel { get; private set; }

        #endregion

        #region Constructor  

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.ViewModel = ServiceLocator.Instance.GetRequiredService<MainWindowViewModel>();

            this.Closed += MainWindow_Closed;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            NavigationFrame.Dispose();
        }

        /// <summary>
        /// Gets the navigation frame.
        /// </summary>
        /// <returns></returns>
        public INavigationFrame GetNavigationFrame() => NavigationFrame;

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Closed event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="WindowEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            this.ViewModel.Dispose();
        }

        #endregion
    }
}