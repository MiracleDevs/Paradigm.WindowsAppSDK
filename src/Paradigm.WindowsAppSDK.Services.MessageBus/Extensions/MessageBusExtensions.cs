namespace Paradigm.WindowsAppSDK.Services.MessageBus.Extensions
{
    public static class MessageBusExtensions
    {
        ///// <summary>
        ///// Registers the specified message listener.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="messageListener">The message listener.</param>
        //protected void RegisterMessage<T>(Func<T, Task> messageListener)
        //{
        //    this.MessageBusRegistrations.Add(typeof(T), this.MessageBusService.Register(messageListener));
        //}

        ///// <summary>
        ///// Unregisters the message listener.
        ///// </summary>
        ///// <typeparam name="T">The message type.</typeparam>
        //protected void UnregisterMessage<T>()
        //{
        //    if (!this.MessageBusRegistrations.ContainsKey(typeof(T)))
        //    {
        //        LogService.Debug($"The message '{typeof(T).Name}' was not registered in the type '{this.GetType().Name}' and can not be unregistered.");
        //        return;
        //    }

        //    this.MessageBusService.Unregister(this.MessageBusRegistrations[typeof(T)]);
        //    this.MessageBusRegistrations.Remove(typeof(T));
        //}

        ///// <summary>
        ///// Sends the message.
        ///// </summary>
        ///// <typeparam name="T">The message type.</typeparam>
        ///// <param name="message">The message.</param>
        //protected async Task SendMessageAsync<T>(T message)
        //{
        //    if (_disposed)
        //        return;

        //    await this.MessageBusService.SendAsync(message);
        //}
    }
}