# Message Bus Service

The `Paradigm.WindowsAppSDK.Services.MessageBus` package provides a lightweight, in-process messaging system that enables loose coupling between components in Windows App SDK applications.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.MessageBus
```

## Key Components

### IMessageBusService

The core interface that defines methods for publishing and subscribing to messages.

```csharp
public interface IMessageBusService : IService
{
    /// <summary>
    /// Publishes the specified message.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="message">The message.</param>
    void Publish<TMessage>(TMessage message);

    /// <summary>
    /// Subscribes to a message type with the specified handler.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="handler">The handler.</param>
    /// <returns>A token that can be used to unsubscribe.</returns>
    IDisposable Subscribe<TMessage>(Action<TMessage> handler);
}
```

### MessageBusService

The implementation of `IMessageBusService` that handles message publication and subscription.

### MessageBusRegistrationsHandler

A class that manages message handler registrations and handles message delivery to subscribers.

### Models

#### RegistrationToken

A token that represents a subscription to a message type. When disposed, the subscription is canceled.

#### Handler

A container for message handlers that ensures type safety when delivering messages.

## Usage

### Service Registration

Register the message bus service in your application's startup:

```csharp
services.AddSingleton<IMessageBusService, MessageBusService>();
```

### Basic Messaging

#### Publishing Messages

Publish a message to all subscribers:

```csharp
// Define a message class
public class UserLoggedInMessage
{
    public string UserId { get; set; }
    public DateTime LoginTime { get; set; }
}

// Get the message bus service
var messageBus = ServiceLocator.Instance.GetRequiredService<IMessageBusService>();

// Publish a message
messageBus.Publish(new UserLoggedInMessage 
{ 
    UserId = "user123", 
    LoginTime = DateTime.Now 
});
```

#### Subscribing to Messages

Subscribe to receive messages of a specific type:

```csharp
// Subscribe to UserLoggedInMessage
IDisposable subscription = messageBus.Subscribe<UserLoggedInMessage>(message => 
{
    // Handle the message
    Console.WriteLine($"User {message.UserId} logged in at {message.LoginTime}");
});

// Later, when no longer needed, unsubscribe
subscription.Dispose();
```

### Advanced Usage

#### Subscription Management in ViewModels

In view models, it's common to subscribe to messages when the view model is created and unsubscribe when it's disposed:

```csharp
public class DashboardViewModel : ViewModelBase
{
    private readonly IDisposable _userLoggedInSubscription;
    
    public DashboardViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        var messageBus = GetRequiredService<IMessageBusService>();
        
        // Subscribe to messages
        _userLoggedInSubscription = messageBus.Subscribe<UserLoggedInMessage>(OnUserLoggedIn);
    }
    
    private void OnUserLoggedIn(UserLoggedInMessage message)
    {
        // Handle the message
    }
    
    public override void Dispose()
    {
        // Unsubscribe when the view model is disposed
        _userLoggedInSubscription?.Dispose();
        
        base.Dispose();
    }
}
```

#### Multiple Subscribers

Multiple components can subscribe to the same message type, enabling one-to-many communication:

```csharp
// Component 1
messageBus.Subscribe<DataUpdatedMessage>(message => {
    // Refresh a list
});

// Component 2
messageBus.Subscribe<DataUpdatedMessage>(message => {
    // Update a chart
});

// Component 3
messageBus.Subscribe<DataUpdatedMessage>(message => {
    // Log the update
});

// Publish once, notify all three subscribers
messageBus.Publish(new DataUpdatedMessage());
```

#### Using MessageBusRegistrationsHandler Directly

For more advanced scenarios, you can use the `MessageBusRegistrationsHandler` directly:

```csharp
var handler = new MessageBusRegistrationsHandler(serviceProvider);

// Register handlers
handler.Register<MyMessage>(OnMyMessage);

// Receive a message
handler.OnReceive(new MyMessage());

// Unregister handlers
handler.Unregister<MyMessage>(OnMyMessage);
```

## Best Practices

### Message Design

1. **Make messages immutable**: Once published, a message should not be modified by subscribers.
2. **Keep messages simple**: Messages should primarily be data containers without complex logic.
3. **Use meaningful names**: Name messages based on events rather than commands (e.g., `UserLoggedInMessage` instead of `LoginMessage`).

### Subscription Management

1. **Always dispose subscriptions**: To prevent memory leaks, always dispose of subscriptions when they're no longer needed.
2. **Use the using pattern**: For short-lived subscriptions, use the `using` pattern to ensure disposal.
3. **Centralize subscriptions**: In view models, manage all subscriptions in one place for easier tracking.

### Performance Considerations

1. **Avoid heavy processing in handlers**: Message handlers should be lightweight to avoid blocking the message bus.
2. **Consider message filtering**: For high-frequency messages, filter at the source rather than in each subscriber.
3. **Be cautious with recursive publishing**: Avoid situations where a message handler publishes the same message type, which could lead to infinite recursion. 