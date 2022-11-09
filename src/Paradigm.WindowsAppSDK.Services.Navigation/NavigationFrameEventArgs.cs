using System;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public class NavigationFrameEventArgs : EventArgs

    {
        public NavigationFrameEventArgs(INavigableView content)
        {
            this.Content = content;
        }

        public INavigableView Content { get; private set; }
    }
}
