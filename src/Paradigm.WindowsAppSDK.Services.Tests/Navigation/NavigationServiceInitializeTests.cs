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
            this.NavigationFrame.Setup(frame => frame.CanGoForward).Returns(false);
            this.NavigationFrame.Setup(frame => frame.CanGoBack).Returns(true);

            //act
            this.Sut.Initialize(this.NavigationFrame.Object);

            //assert
            Assert.That(this.Sut.CanGoForward, Is.EqualTo(this.NavigationFrame.Object.CanGoForward));
            Assert.That(this.Sut.CanGoBack, Is.EqualTo(this.NavigationFrame.Object.CanGoBack));
        }
    }
}
