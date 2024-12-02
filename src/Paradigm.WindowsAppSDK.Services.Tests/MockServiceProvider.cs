namespace Paradigm.WindowsAppSDK.Services.Tests;
public class MockServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, object> _services = new();

    public void RegisterService<T>(T service)
    {
        _services[typeof(T)] = service!;
    }

    public object? GetService(Type serviceType)
    {
        _services.TryGetValue(serviceType, out var service);
        return service;
    }
}
