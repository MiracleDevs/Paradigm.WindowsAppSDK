using Moq;
using Paradigm.WindowsAppSDK.Services.MessageBus;

namespace Paradigm.WindowsAppSDK.Services.Tests.MessageBus
{
    public class MessageBusTests
    {
        private MessageBusRegistrationsHandler Sut { get; set; }
        private Mock<IServiceProvider> ServiceProvider { get; set; }
        private IMessageBusService MessageBusService { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Sut = MessageBusRegistrationsHandler.Instance;
            this.ServiceProvider = new Mock<IServiceProvider>();
            this.MessageBusService = new MessageBusService();
            this.ServiceProvider.Setup(provider => provider.GetService(typeof(IMessageBusService))).Returns(this.MessageBusService);
        }

        [Test]
        public async Task ShouldRegisterMessage()
        {
            //arrange
            var msg = new FirstTestMessage();
            var firstConsumer = this;
            var anotherConsumer = new AnotherMessageConsumer(false);

            //act
            this.Sut.RegisterMessageHandler<FirstTestMessage>(firstConsumer, this.ServiceProvider.Object, OnMessageSentAsync);
            this.Sut.RegisterMessageHandler<FirstTestMessage>(anotherConsumer, this.ServiceProvider.Object, anotherConsumer.HandleMessage);
            await this.MessageBusService.SendAsync(msg);
            var registrations = this.Sut.GetRegisteredMessageHandlers(anotherConsumer).Concat(this.Sut.GetRegisteredMessageHandlers(firstConsumer));

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(registrations, Is.Not.Empty);
                Assert.That(msg.TimesHandled, Is.EqualTo(registrations.Count()));
                Assert.That(anotherConsumer.MessageHandled);
            });
        }

        [Test]
        public async Task ShouldUnRegisterMessagesAndIgnoreMessageProcessing()
        {
            //arrange
            var msg = new FirstTestMessage();
            var msg2 = new AnotherSampleTestMessage();
            var consumer = this;

            //act
            this.Sut.RegisterMessageHandler<FirstTestMessage>(consumer, this.ServiceProvider.Object, OnMessageSentAsync);
            this.Sut.RegisterMessageHandler<AnotherSampleTestMessage>(consumer, this.ServiceProvider.Object, OnMessage2SentAsync);
            this.Sut.UnregisterMessageHandlers(consumer, this.ServiceProvider.Object);

            await this.MessageBusService.SendAsync(msg);
            await this.MessageBusService.SendAsync(msg2);

            var items = this.Sut.GetRegisteredMessageHandlers(consumer);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(items, Is.Empty);
                Assert.That(msg2.Handled, Is.Null);
                Assert.That(msg.TimesHandled, Is.Null);
            });
        }

        [Test]
        public void ShouldNotThrowExceptionIfUnRegisterMessageIsCalledTwice()
        {
            //arrange
            var msg = new FirstTestMessage();
            var consumer = this;

            // act 
            this.Sut.RegisterMessageHandler<FirstTestMessage>(consumer, this.ServiceProvider.Object, OnMessageSentAsync);
            var items = this.Sut.GetRegisteredMessageHandlers(consumer);

            //assert
            Assert.DoesNotThrow(() =>
            {
                this.Sut.UnregisterMessageHandler<FirstTestMessage>(consumer, this.ServiceProvider.Object);

                this.Sut.UnregisterMessageHandler<FirstTestMessage>(consumer, this.ServiceProvider.Object);
            });

            Assert.That(items, Is.Empty);
        }

        [Test]
        public async Task ShouldNotHandleMessageIfItWasPreviouslyUnregistered()
        {
            //arrange
            var msg = new FirstTestMessage();
            var consumer = this;

            //act
            this.Sut.RegisterMessageHandler<FirstTestMessage>(consumer, this.ServiceProvider.Object, OnMessageSentAsync);
            this.Sut.UnregisterMessageHandler<FirstTestMessage>(consumer, this.ServiceProvider.Object);
            await this.MessageBusService.SendAsync(msg);

            var registrations = this.Sut.GetRegisteredMessageHandlers(consumer);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(registrations, Is.Empty);
                Assert.That(msg.TimesHandled, Is.Null);
            });
        }

        private async Task OnMessageSentAsync(FirstTestMessage message)
        {
            message.TimesHandled = message.TimesHandled.GetValueOrDefault(0) + 1;
            await Task.CompletedTask;
        }

        private async Task OnMessage2SentAsync(AnotherSampleTestMessage message)
        {
            message.Handled = true;
            await Task.CompletedTask;
        }

        internal class AnotherMessageConsumer
        {
            internal bool MessageHandled { get; private set; }

            internal AnotherMessageConsumer(bool messageHandled)
            {
                MessageHandled = messageHandled;
            }

            internal async Task HandleMessage(FirstTestMessage message)
            {
                message.TimesHandled = message.TimesHandled.GetValueOrDefault(0) + 1;
                MessageHandled = true;
                await Task.CompletedTask;
            }
        }

        internal class FirstTestMessage
        {
            internal FirstTestMessage()
            {
            }

            internal int? TimesHandled { get; set; }
        }

        internal class AnotherSampleTestMessage
        {
            internal AnotherSampleTestMessage()
            {
            }

            internal bool? Handled { get; set; }
        }
    }
}