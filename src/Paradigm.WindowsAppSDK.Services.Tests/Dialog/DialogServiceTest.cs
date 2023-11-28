using Moq;
using Paradigm.WindowsAppSDK.Services.Dialog;

namespace Paradigm.WindowsAppSDK.Services.Tests.Dialog
{
    public class DialogServiceTest
    {
        private IDialogService Sut { get; set; }

        private Mock<IServiceProvider> ServiceProvider { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            ServiceProvider = new Mock<IServiceProvider>();
            ServiceProvider.Setup(provider => provider.GetService(typeof(TestDialogViewModel))).Returns(new TestDialogViewModel());

            Sut = new DialogService(ServiceProvider.Object);
            Sut.Register<TestDialogView, TestDialogViewModel>();
        }

        [Test]
        public async Task ShouldInitializeViewModelAndOpenAsync()
        {
            //act
            var result = await Sut.OpenAsync<TestDialogViewModel>(default(object?));

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ShouldReceiveViewModelAndOpenAsync()
        {
            //arrange
            var dialogViewModel = new TestDialogViewModel();

            //act
            var result = await Sut.OpenAsync(dialogViewModel);

            //assert
            Assert.That(result, Is.True);
        }
    }
}
