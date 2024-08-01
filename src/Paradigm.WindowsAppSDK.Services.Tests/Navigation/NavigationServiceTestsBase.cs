using Moq;
using Paradigm.WindowsAppSDK.Services.Navigation;

namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public abstract class NavigationServiceTestsBase
    {
        protected NavigationServiceTestsBase()
        {

        }

        protected INavigationService Sut { get; private set; }
        protected Mock<IServiceProvider> ServiceProvider { get; private set; }
        protected virtual Mock<INavigationFrame> NavigationFrame { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            this.NavigationFrame = new Mock<INavigationFrame>();

            this.ServiceProvider = new Mock<IServiceProvider>();

            this.Sut = new NavigationService(this.ServiceProvider.Object);
        }
    }
}