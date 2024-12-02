namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceInitializeTests : NavigationServiceTestsBase
    {
        public NavigationServiceInitializeTests()
        {
        }

        [Test]
        public void ShouldInitializeNavigationService()
        {
            //arrange
            using var navigationFrame = new TestNavigationFrame(new Dictionary<Type, Type>());

            //act
            Sut.Initialize(navigationFrame);

            //assert
            Assert.That(Sut.CanGoForward, Is.EqualTo(navigationFrame.CanGoForward));
            Assert.That(Sut.CanGoBack, Is.EqualTo(navigationFrame.CanGoBack));
        }
    }
}