using Paradigm.WindowsAppSDK.Services.Dialog;

namespace Paradigm.WindowsAppSDK.Services.Tests.Dialog
{
    public class DialogServiceTest
    {
        private IDialogService? Sut { get; set; }

        private MockServiceProvider? ServiceProvider { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            ServiceProvider = new MockServiceProvider();
            ServiceProvider.RegisterService(new TestDialogViewModel());

            Sut = new DialogService(ServiceProvider);
            Sut.Register<TestDialogView, TestDialogViewModel>();
        }

        [Test]
        public async Task ShouldInitializeViewModelAndOpenAsync()
        {
            if (Sut is null) return;

            //act
            var result = await Sut.OpenAsync<TestDialogViewModel>(default(object?));

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ShouldReceiveViewModelAndOpenAsync()
        {
            if (Sut is null) return;

            //arrange
            var dialogViewModel = new TestDialogViewModel();

            //act
            var result = await Sut.OpenAsync(dialogViewModel);

            //assert
            Assert.That(result, Is.True);
        }
    }
}
