using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;

namespace Paradigm.WindowsAppSDK.Services.Tests.Navigation
{
    public class NavigationServiceNavigateTests : NavigationServiceTestsBase
    {
        public NavigationServiceNavigateTests()
        {
        }

        public override void Setup()
        {
            base.Setup();

            this.Sut.Register<NavigationMainTestPage, NavigationMainViewModel>();
            this.Sut.Register<NavigationTestPage, NavigationTestViewModel>();
            this.Sut.Register<NavigationTestPage2, NavigationTestViewModel2>();

            this.ServiceProvider.RegisterService(new NavigationMainViewModel());
            this.ServiceProvider.RegisterService(new NavigationTestViewModel());
            this.ServiceProvider.RegisterService(new NavigationTestViewModel2());
        }

        [Test]
        public async Task ShouldNavigateToMainViewModelAsync()
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

            var navigationResult = await this.Sut.NavigateToAsync(typeof(NavigationMainViewModel));

            //assert
            Assert.That(navigationResult);

            Assert.That(this.Sut.CanGoBack, Is.False);
            Assert.That(this.Sut.CanGoForward, Is.True);
        }

        [Test]
        public async Task ShouldNavigateToTestViewModelAsync()
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

            var navigationResult = await this.Sut.NavigateToAsync(typeof(NavigationTestViewModel));

            //assert
            Assert.That(navigationResult);
            Assert.That(this.Sut.CanGoBack, Is.True);
            Assert.That(this.Sut.CanGoForward, Is.False);
        }

        [Test]
        public async Task ShouldNavigateToTestViewModel2Async()
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
            await this.Sut.NavigateToAsync(typeof(NavigationMainViewModel));

            var navigationResult = await this.Sut.NavigateToAsync(typeof(NavigationTestViewModel2));

            //assert
            Assert.That(navigationResult);
            Assert.That(this.Sut.CanGoBack, Is.True);
            Assert.That(this.Sut.CanGoForward, Is.False);
        }

        [Test]
        public void ShouldNotCanGoBackIfNoNavigationWasPerformed()
        {
            //arrange 
            var navigationFrame = new TestNavigationFrame(new Dictionary<Type, Type>
            {
                { typeof(NavigationMainViewModel), typeof(NavigationMainTestPage) }
            });

            //act
            this.Sut.Initialize(navigationFrame);
            var canGoBack = this.Sut.CanGoBack;

            //assert
            Assert.That(canGoBack, Is.False);
        }

        [Test]
        public void ShouldCanGoForwardIfNoNavigationWasPerformed()
        {
            //arrange 
            var navigationFrame = new TestNavigationFrame(new Dictionary<Type, Type>
            {
                { typeof(NavigationMainViewModel), typeof(NavigationMainTestPage) },
                { typeof(NavigationTestViewModel), typeof(NavigationTestPage) }
            });

            //act
            this.Sut.Initialize(navigationFrame);

            var canGoForward = this.Sut.CanGoForward;

            //assert
            Assert.That(canGoForward, Is.True);
        }

        protected class NavigationTestViewModel2 : INavigable
        {
            public Task<bool> CanNavigateFrom(INavigable navigable)
            {
                return Task.FromResult(navigable is NavigationMainViewModel);
            }

            public Task<bool> CanNavigateTo(INavigable navigable)
            {
                return Task.FromResult(navigable is NavigationMainViewModel);
            }
        }

        protected class NavigationTestViewModel : INavigable
        {
            public Task<bool> CanNavigateFrom(INavigable navigable)
            {
                return Task.FromResult(navigable is NavigationMainViewModel);
            }

            public Task<bool> CanNavigateTo(INavigable navigable)
            {
                return Task.FromResult(navigable is NavigationMainViewModel);
            }
        }

        protected class NavigationMainViewModel : INavigable
        {
            public Task<bool> CanNavigateFrom(INavigable navigable)
            {
                return Task.FromResult(true);
            }

            public Task<bool> CanNavigateTo(INavigable navigable)
            {
                return Task.FromResult(true);
            }
        }

        protected class NavigationMainTestPage : INavigableView
        {
            public Task DisposeAsync()
            {
                return Task.CompletedTask;
            }

            public Task InitializeNavigationAsync(INavigable navigable)
            {
                return Task.CompletedTask;
            }
        }

        protected class NavigationTestPage : INavigableView
        {
            public Task DisposeAsync()
            {
                return Task.CompletedTask;
            }

            public Task InitializeNavigationAsync(INavigable navigable)
            {
                return Task.CompletedTask;
            }
        }

        protected class NavigationTestPage2 : INavigableView
        {
            public Task DisposeAsync()
            {
                return Task.CompletedTask;
            }

            public Task InitializeNavigationAsync(INavigable navigable)
            {
                return Task.CompletedTask;
            }
        }
    }
}
