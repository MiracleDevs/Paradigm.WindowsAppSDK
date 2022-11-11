using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using System.Diagnostics;

namespace Paradigm.WindowsAppSDK.Services.Tests.MessageBus
{
    public class MessageBusTests
    {
        private MessageBusRegistrationsHandler Sut { get;set;}
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


        [TestCase]
        public async Task ShouldRegisterMessage()
        {
            //arrange
            
            var msg = new TestMessage();

            var firstConsumer = this;

            var anotherConsumer = new AnotherMessageConsumer(false);
            
            //act
            this.Sut.RegisterMessageHandler<TestMessage>(firstConsumer, this.ServiceProvider.Object, OnMessageSentAsync);
            
            this.Sut.RegisterMessageHandler<TestMessage>(anotherConsumer, this.ServiceProvider.Object, anotherConsumer.HandleMessage);

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

        [TestCase]
        public async Task ShouldUnRegisterMessagesAndIgnoreMessageProcessing()
        {
            //arrange
            var msg = new TestMessage();
            var msg2 = new TestMessage2();
            var consumer = this;

            //act
            this.Sut.RegisterMessageHandler<TestMessage>(consumer, this.ServiceProvider.Object, OnMessageSentAsync);

            this.Sut.RegisterMessageHandler<TestMessage2>(consumer, this.ServiceProvider.Object, OnMessage2SentAsync);

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

        [TestCase]
        public void ShouldNotThrowExceptionIfUnRegisterMessageIsCalledTwice()
        {
            //arrange
            var msg = new TestMessage();
            var consumer = this;

            // act 
            this.Sut.RegisterMessageHandler<TestMessage>(consumer, this.ServiceProvider.Object, OnMessageSentAsync);
            var items = this.Sut.GetRegisteredMessageHandlers(consumer);

            //assert
            Assert.DoesNotThrow(() =>
            {
                this.Sut.UnregisterMessageHandler<TestMessage>(consumer, this.ServiceProvider.Object);

                this.Sut.UnregisterMessageHandler<TestMessage>(consumer, this.ServiceProvider.Object);
            });
            
            Assert.That(items, Is.Empty);
        }

        [TestCase]
        public async Task ShouldNotHandleMessageIfItWasPreviouslyUnregistered()
        {
            //arrange
            var msg = new TestMessage();
            var consumer = this;

            //act
            this.Sut.RegisterMessageHandler<TestMessage>(consumer, this.ServiceProvider.Object, OnMessageSentAsync);
            
            this.Sut.UnregisterMessageHandler<TestMessage>(consumer, this.ServiceProvider.Object);

            await this.MessageBusService.SendAsync(msg);

            var registrations = this.Sut.GetRegisteredMessageHandlers(consumer);
            
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(registrations, Is.Empty);
                Assert.That(msg.TimesHandled, Is.Null);
            });
        }
       
        private async Task OnMessageSentAsync(TestMessage message)
        {
            message.TimesHandled= message.TimesHandled.GetValueOrDefault(0) + 1;

            await Task.CompletedTask;
        }

        private async Task OnMessage2SentAsync(TestMessage2 message)
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

            internal async Task HandleMessage(TestMessage message)
            {
                message.TimesHandled = message.TimesHandled.GetValueOrDefault(0) + 1;
                MessageHandled = true;
                await Task.CompletedTask;
            }
        }

        internal class TestMessage
        {
            internal TestMessage()
            {

            }

            internal int? TimesHandled { get; set; }
        }

        internal class TestMessage2
        {
            internal TestMessage2()
            {

            }

            internal bool? Handled { get; set; }
        }
    }
}
