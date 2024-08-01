namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceGoForwardTests : NavigationServiceNavigateTests
    {
        public NavigationServiceGoForwardTests()
        {
        }

        [Test]
        public async Task ShouldGoForwardAsync()
        {
            //arrange 
            var navigationFrame = new TestNavigationFrame(new Dictionary<Type, Type>
            {
                { typeof(NavigationMainViewModel), typeof(NavigationMainTestPage) },
                { typeof(NavigationTestViewModel), typeof(NavigationTestPage) },
                { typeof(NavigationTestViewModel2), typeof(NavigationTestPage2) }
            });

            //act
            this.Sut.Initialize(navigationFrame);

            await this.Sut.NavigateToAsync(typeof(NavigationMainViewModel));

            var goForwardResult = await this.Sut.GoForwardAsync();

            //assert
            Assert.That(goForwardResult);
            Assert.That(this.Sut.CanGoBack, Is.True);
        }

        [Test]
        public async Task ShouldNotGoForwardAsync()
        {
            //arrange 
            var navigationFrame = new TestNavigationFrame(new Dictionary<Type, Type>
            {
                { typeof(NavigationMainViewModel), typeof(NavigationMainTestPage) },
                { typeof(NavigationTestViewModel), typeof(NavigationTestPage) }
            });

            //act
            this.Sut.Initialize(navigationFrame);

            await this.Sut.NavigateToAsync(typeof(NavigationMainViewModel));

            await this.Sut.GoForwardAsync();

            var goForwardResult = await this.Sut.GoForwardAsync();
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(goForwardResult, Is.False);
                Assert.That(this.Sut.CanGoForward, Is.False);
            });
        }
    }
}
