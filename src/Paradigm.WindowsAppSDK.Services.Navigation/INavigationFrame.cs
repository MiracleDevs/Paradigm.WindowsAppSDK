using System;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public interface INavigationFrame
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        Action<object, NavigationFrameEventArgs> Navigated { get; set; }
        void GoForward();
        void Navigate(Type sourcePageType, object value);
        void GoBack();
        void ClearBackStack();
        Type LastBackStackSourcePageType();
        Type LastForwardStackSourcePageType();
    }

    public class NavigationFrameEventArgs: EventArgs

    {
        public NavigationFrameEventArgs(INavigableView content)
        {
            this.Content = content;
        }


        public INavigableView Content { get; private set; }
    }
}
