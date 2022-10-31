using Microsoft.UI.Xaml;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Main;
using Paradigm.WindowsAppSDK.ViewModels;

namespace Paradigm.WindowsAppSDK.SampleApp
{
    public sealed partial class MainWindow : Window
    {

        #region Properties
        
        internal MainViewModel ViewModel { get; }

        #endregion

        #region Constructor

        public MainWindow()
        {
            this.InitializeComponent();
            this.ViewModel = ServiceLocator.Instance.GetRequiredService <MainViewModel>();
        }

        #endregion

        #region Event Handlers  

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";

            await ViewModel.ExecuteActionAsync(myButton.Content.ToString());
        }

        #endregion

    }
}