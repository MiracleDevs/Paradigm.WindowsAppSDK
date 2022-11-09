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
    public sealed partial class NavigationRootFrame : UserControl, INavigationFrame
    {

        public NavigationRootFrame()
        {
            this.InitializeComponent();

            this.RootFrame.Navigated += OnFrameNavigated;
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            this.Navigated(sender, new NavigationFrameEventArgs(e.Content as INavigableView));
        }

        public bool CanGoBack => this.RootFrame.CanGoBack;

        public bool CanGoForward => this.RootFrame.CanGoForward;

        public Action<object, NavigationFrameEventArgs> Navigated { get; set; }

        public void ClearBackStack()
        {
            this.RootFrame.BackStack.Clear();
        }

        public void GoBack()
        {
            this.RootFrame.GoBack();
        }

        public void GoForward()
        {
            this.RootFrame.GoForward();
        }

        public Type LastForwardStackSourcePageType()
        {
            return this.RootFrame.ForwardStack.Last().SourcePageType;
        }

        public Type LastBackStackSourcePageType()
        {
            return this.RootFrame.BackStack.Last().SourcePageType;
        }

        public void Navigate(Type sourcePageType, object value)
        {
            this.RootFrame.Navigate(sourcePageType, value, new SuppressNavigationTransitionInfo());
        }
    }
}
