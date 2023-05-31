namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceClearCurrentContentTests : NavigationServiceNavigateTests
    {
        public NavigationServiceClearCurrentContentTests()
        {
        }

        [Test]
        public async Task ShouldClearCurrentContentAsync()
        {
            //arrange 
            var navigationFrame = new TestNavigationFrame(new Dictionary<Type, Type>
            {
                { typeof(NavigationMainViewModel), typeof(NavigationMainTestPage) }
            });

            //act
            this.Sut.Initialize(navigationFrame);

            await this.Sut.NavigateToAsync(typeof(NavigationMainViewModel));
            await this.Sut.ClearCurrentContentAsync();

            Assert.IsNull(this.Sut.CurrentNavigable);
            Assert.IsNull(this.Sut.CurrentNavigableView);
        }
    }
}
