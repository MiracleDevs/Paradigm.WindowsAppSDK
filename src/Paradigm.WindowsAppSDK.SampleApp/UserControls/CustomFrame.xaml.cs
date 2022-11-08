using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Paradigm.WindowsAppSDK.SampleApp.UserControls
{
    public sealed partial class CustomFrame : UserControl, INavigationFrame
    {

        public CustomFrame()
        {
            this.InitializeComponent();

            this.RootFrame.Navigated += OnFrameNavigated;
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            this.Navigated(sender, new NavigationFrameEventArgs(e.Content as INavigableView));
        }

        bool INavigationFrame.CanGoBack => this.RootFrame.CanGoBack;

        bool INavigationFrame.CanGoForward => this.RootFrame.CanGoForward;

        public Action<object, NavigationFrameEventArgs> Navigated { get; set; }

        void INavigationFrame.ClearBackStack()
        {
            this.RootFrame.BackStack.Clear();
        }

        void INavigationFrame.GoBack()
        {
            this.RootFrame.GoBack();
        }

        void INavigationFrame.GoForward()
        {
            this.RootFrame.GoForward();
        }

        Type INavigationFrame.LastForwardStackSourcePageType()
        {
            return this.RootFrame.ForwardStack.Last().SourcePageType;
        }

        Type INavigationFrame.LastBackStackSourcePageType()
        {
            return this.RootFrame.BackStack.Last().SourcePageType;
        }

        void INavigationFrame.Navigate(Type sourcePageType, object value)
        {
            this.RootFrame.Navigate(sourcePageType, value, new SuppressNavigationTransitionInfo());
        }
    }
}
