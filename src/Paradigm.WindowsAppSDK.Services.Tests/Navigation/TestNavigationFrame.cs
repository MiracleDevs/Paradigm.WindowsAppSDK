using Paradigm.WindowsAppSDK.Services.Navigation;

namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class TestNavigationFrame : INavigationFrame
    {
        private IDictionary<Type, Type> NavigationTypes { get; set; }

        private Type ActiveFrameKeyType { get; set; }

        public TestNavigationFrame(IDictionary<Type, Type> navigationTypes)
        {
            NavigationTypes = navigationTypes;

            this.ActiveFrameKeyType = navigationTypes.Keys.First();

            this.Navigated += new Action<object, NavigationFrameEventArgs>(OnNavigated);
        }

        public void Dispose()
        {
        }

        private void OnNavigated(object sender, NavigationFrameEventArgs navigationArgs)
        {
            this.ActiveFrameKeyType = this.NavigationTypes.FirstOrDefault(navigationType => navigationType.Value == navigationArgs.Content.GetType()).Key;
        }

        public bool CanGoBack
        {
            get
            {
                return NavigationTypes.Any() && Array.IndexOf(this.NavigationTypes.Keys.ToArray(), this.ActiveFrameKeyType) > 0;
            }
        }

        public bool CanGoForward
        {
            get
            {
                var count = this.NavigationTypes.Keys.Count;

                return NavigationTypes.Any() && Array.IndexOf(this.NavigationTypes.Keys.ToArray(), this.ActiveFrameKeyType) < count - 1;
            }
        }

        public Action<object, NavigationFrameEventArgs> Navigated { get; set; }

        public void ClearBackStack()
        {
            this.Navigate(NavigationTypes.Values.First(), null);
        }

        public void GoBack()
        {
            var index = Array.IndexOf(this.NavigationTypes.Keys.ToArray(), this.ActiveFrameKeyType);

            this.Navigate(this.NavigationTypes.Values.ToArray()[index - 1], null);
        }

        public void GoForward()
        {
            var index = Array.IndexOf(this.NavigationTypes.Keys.ToArray(), this.ActiveFrameKeyType);

            this.Navigate(this.NavigationTypes.Values.ToArray()[index + 1], null);
        }

        public Type LastBackStackSourcePageType()
        {
            return NavigationTypes.Values.First();
        }

        public Type LastForwardStackSourcePageType()
        {
            return NavigationTypes.Values.Last();
        }

        public void Navigate(Type sourcePageType, object? value)
        {
            this.Navigated(this, new NavigationFrameEventArgs(Activator.CreateInstance(sourcePageType) as INavigableView));
        }
    }
}