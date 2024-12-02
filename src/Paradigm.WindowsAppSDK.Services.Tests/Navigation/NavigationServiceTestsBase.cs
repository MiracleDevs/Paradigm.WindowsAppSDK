using Paradigm.WindowsAppSDK.Services.Navigation;

namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public abstract class NavigationServiceTestsBase
    {
        protected INavigationService Sut { get; private set; }
        protected MockServiceProvider? ServiceProvider { get; set; }

        protected NavigationServiceTestsBase()
        {
        }

        [SetUp]
        public virtual void Setup()
        {
            this.ServiceProvider = new MockServiceProvider();
            this.Sut = new NavigationService(this.ServiceProvider);
        }
    }
}