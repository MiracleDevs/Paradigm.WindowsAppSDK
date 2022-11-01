using System.Reflection;

namespace Paradigm.WindowsAppSDK.ViewModels.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the filtered referenced assemblies.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeFilter">The type filter.</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetFilteredReferencedAssemblies(this Assembly assembly, Type typeFilter)
        {
            var items = new List<Assembly>
            {
                assembly
            };

            foreach (var referencedAssembly in assembly.GetReferencedAssemblies().Where(assemblyName => HasDefinedContcreteService(typeFilter, Assembly.Load(assemblyName))))
            {
                items.AddRange(Assembly.Load(referencedAssembly.FullName).GetFilteredReferencedAssemblies(typeFilter).Distinct());
            }

            return items.Distinct();
        }

        /// <summary>
        /// Determines whether [has defined contcrete service] [the specified service type].
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="asm">The asm.</param>
        /// <returns>
        ///   <c>true</c> if [has defined contcrete service] [the specified service type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool HasDefinedContcreteService(Type serviceType, Assembly asm)
        {
            return asm.DefinedTypes.Any(t => serviceType.IsAssignableFrom(t) && t.IsPublic && !t.IsAbstract);
        }
    }
}
