namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceNavigateToTests : NavigationServiceNavigateTests
    {
        public NavigationServiceNavigateToTests()
        {
        }

        [Test]
        public async Task ShouldNavigateAsync()
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

            var mainNavigationResult = await this.Sut.NavigateToAsync<NavigationMainViewModel>();
            var testNavigationResult = await this.Sut.NavigateToAsync<NavigationTestViewModel>();
            var goBackResult = await Sut.GoBackAsync();
            var secondNavigationResult = await this.Sut.NavigateToAsync<NavigationTestViewModel2>();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(mainNavigationResult && goBackResult && testNavigationResult && secondNavigationResult);
                Assert.That(this.Sut.CanGoBack, Is.True);
                Assert.That(this.Sut.CanGoForward, Is.False);
            });
        }
    }
}

