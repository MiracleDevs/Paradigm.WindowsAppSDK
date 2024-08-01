namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceGoBackTests : NavigationServiceNavigateTests
    {
        public NavigationServiceGoBackTests()
        {
        }

        [Test]
        public async Task ShouldGoBackAsync()
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

            var goBackResult = await this.Sut.GoBackAsync();

            //assert
            Assert.That(goBackResult);
            Assert.That(this.Sut.CanGoBack, Is.False);
        }

        [Test]
        public async Task ShouldNotGoBackAsync()
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

            await this.Sut.GoBackAsync();

            var result = await this.Sut.GoBackAsync();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);
                Assert.That(this.Sut.CanGoBack, Is.False);
            });
        }
    }
}
