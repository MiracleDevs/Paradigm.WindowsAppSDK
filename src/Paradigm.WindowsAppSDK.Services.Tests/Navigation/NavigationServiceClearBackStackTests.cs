namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceClearBackStackTests : NavigationServiceNavigateTests
    {
        public NavigationServiceClearBackStackTests()
        {
        }

        [Test]
        public async Task ShouldClearBackStackAsync()
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
            await this.Sut.NavigateToAsync(typeof(NavigationTestViewModel));

            this.Sut.ClearBackStack();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(this.Sut.CanGoBack, Is.False);
                Assert.That(this.Sut.CanGoForward, Is.True);
            });

        }
    }
}
