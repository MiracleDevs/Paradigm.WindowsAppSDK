using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        #region Properties

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        private IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the navigation views.
        /// </summary>
        /// <value>
        /// The navigation views.
        /// </value>
        private Dictionary<Type, Type> NavigationViews { get; }

        /// <summary>
        /// Gets the navigables.
        /// </summary>
        /// <value>
        /// The navigables.
        /// </value>
        private Dictionary<Type, Type> Navigables { get; }

        /// <summary>
        /// Gets the frame.
        /// </summary>
        /// <value>
        /// The frame.
        /// </value>
        private INavigationFrame? Frame { get; set; }

        /// <summary>
        /// Gets or sets the candidate view.
        /// </summary>
        /// <value>
        /// The candidate view.
        /// </value>
        private INavigableView? CandidateView { get; set; }

        /// <summary>
        /// Gets the current navigable.
        /// </summary>
        /// <value>
        /// The current navigable.
        /// </value>
        public INavigable? CurrentNavigable { get; private set; }

        /// <summary>
        /// Gets the current navigable view.
        /// </summary>
        /// <value>
        /// The current navigable view.
        /// </value>
        public INavigableView? CurrentNavigableView { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoBack => Frame?.CanGoBack ?? false;

        /// <summary>
        /// Gets a value indicating whether this instance can go forward.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoForward => Frame?.CanGoForward ?? false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public NavigationService(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.NavigationViews = new Dictionary<Type, Type>();
            this.Navigables = new Dictionary<Type, Type>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the specified frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        public void Initialize(INavigationFrame frame)
        {
            this.Frame = frame;
            this.Frame.Navigated += this.OnNavigated;
        }

        /// <summary>
        /// Registers a navigable element and its paired view.
        /// </summary>
        /// <typeparam name="TNavigableView">The type of the page.</typeparam>
        /// <typeparam name="TNavigable">The type of the navigable.</typeparam>
        public void Register<TNavigableView, TNavigable>()
            where TNavigableView : INavigableView
            where TNavigable : INavigable
        {
            this.NavigationViews.Add(typeof(TNavigable), typeof(TNavigableView));
            this.Navigables.Add(typeof(TNavigableView), typeof(TNavigable));
        }

        /// <summary>
        /// Goes back to the previous navigable.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">The navigable view '{navigableView.Name}' is not registered.</exception>
        public async Task<bool> GoBackAsync()
        {
            if (!this.CanGoBack || this.Frame == null || this.CurrentNavigable == null || this.CurrentNavigableView == null)
                return false;

            var navigableView = this.Frame.LastBackStackSourcePageType();

            if (!this.Navigables.ContainsKey(navigableView))
                throw new Exception($"The navigable view '{navigableView.Name}' is not registered.");

            return await this.NavigateToNavigableViewAsync(this.Navigables[navigableView], _ => this.Frame.GoBack());
        }

        /// <summary>
        /// Goes forward to the next navigable.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">The navigable view '{navigableView.Name}' is not registered.</exception>
        public async Task<bool> GoForwardAsync()
        {
            if (!this.CanGoForward || this.Frame == null || this.CurrentNavigable == null || this.CurrentNavigableView == null)
                return false;

            var navigableView = this.Frame.LastForwardStackSourcePageType();

            if (!this.Navigables.ContainsKey(navigableView))
                throw new Exception($"The navigable view '{navigableView.Name}' is not registered.");

            return await this.NavigateToNavigableViewAsync(this.Navigables[navigableView], _ => this.Frame.GoForward());
        }

        /// <summary>
        /// Navigates to a navigable element.
        /// </summary>
        /// <typeparam name="TNavigable">The type of the navigable.</typeparam>
        /// <returns></returns>
        public async Task<bool> NavigateToAsync<TNavigable>() where TNavigable : INavigable
        {
            return await this.NavigateToAsync(typeof(TNavigable));
        }

        /// <summary>
        /// Navigates to a navigable element.
        /// </summary>
        /// <param name="navigableType">Type of the navigable.</param>
        /// <returns></returns>
        public async Task<bool> NavigateToAsync(Type navigableType)
        {
            if (this.Frame == null)
                return false;

            return await this.NavigateToNavigableViewAsync(navigableType, x => this.Frame.Navigate(x, null));
        }

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        public void ClearBackStack()
        {
            this.Frame?.ClearBackStack();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to navigable view.
        /// </summary>
        /// <param name="navigableType">Type of the navigable.</param>
        /// <param name="navigation">The navigation action.</param>
        /// <exception cref="Exception">The navigable element '{navigableType.Name}' is not registered in the navigator service.</exception>
        private async Task<bool> NavigateToNavigableViewAsync(Type navigableType, Action<Type> navigation)
        {
            // 1. check if the navigable element is registered.
            if (!this.NavigationViews.ContainsKey(navigableType))
                throw new Exception($"The navigable element '{navigableType.Name}' is not registered in the navigator service.");

            // 2. instantiate the navigable element from the service provider.
            var navigable = (INavigable)this.ServiceProvider.GetRequiredService(navigableType);

            // 3. check if the previous and current navigable accept the navigation, and if they can, we dispose the current view.
            if (this.CurrentNavigable != null)
            {
                if (!await this.CurrentNavigable.CanNavigateTo(navigable) || !await navigable.CanNavigateFrom(this.CurrentNavigable))
                    return false;

                if (this.CurrentNavigableView != null)
                    await this.CurrentNavigableView.DisposeAsync();
            }

            // 4. if we get to this point, the navigable elements allowed the transition, so we can try to navigate using the uwp navigator manager.
            navigation(this.NavigationViews[navigableType]);

            // 5. at this point we should have the view. if the view is null then the navigation was cancelled.
            if (this.CandidateView == null)
                return false;

            // 6. initialize the navigable view setting up the navigable.
            await this.CandidateView.InitializeNavigationAsync(navigable);

            // 7. we save the current navigable element and view.
            this.CurrentNavigable = navigable;
            this.CurrentNavigableView = this.CandidateView;
            this.CandidateView = null;

            GC.WaitForPendingFinalizers();
            GC.Collect();

            GC.WaitForPendingFinalizers();
            GC.Collect();

            return true;
        }

        /// <summary>
        /// Called when the frame has navigated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void OnNavigated(object sender, NavigationFrameEventArgs e)
        {
            this.CandidateView = e.Content;
        }

        #endregion
    }
}