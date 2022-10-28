using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System.Reflection;

namespace Paradigm.WindowsAppSDK.ViewModels.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Public Methods

        /// <summary>
        /// Registers the view models.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assemblies">The assemblies.</param>
        public static void RegisterViewModels(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var types = GetPublishedTypes(x => typeof(ViewModelBase).IsAssignableFrom(x) && !x.IsAbstract && x.IsPublic, assemblies);

            foreach (var type in types)
            {
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assemblies">The assemblies.</param>
        public static void RegisterServices(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var types = GetPublishedTypes(x => typeof(IService).IsAssignableFrom(x) && !x.IsAbstract && x.IsPublic, assemblies);

            foreach (var type in types)
            {
                serviceCollection.AddSingleton(type);

                var interfaces = type.GetInterfaces().Where(x => x.Name == $"I{type.Name}").ToList();

                foreach (var singleInterface in interfaces)
                {
                    serviceCollection.AddSingleton(singleInterface, type);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the types published by the assembly list .
        /// </summary>
        /// <param name="filter">Function to filter the types.</param>
        /// <param name="assemblies">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        /// <returns></returns>
        private static IEnumerable<TypeInfo> GetPublishedTypes(Func<TypeInfo, bool> filter, params Assembly[] assemblies)
        {
            return assemblies.SelectMany(x => x.DefinedTypes).Where(filter).ToList();
        }

        #endregion
    }
}