# Top 5 Design Patterns — Interview Q&A

Design patterns are reusable approaches to recurring design problems. They are not copy-paste frameworks; select a pattern only when it reduces coupling or clarifies intent.

## 1. Factory Pattern

The Factory pattern centralizes object creation and hides concrete implementation details.

```csharp
public interface INotificationSender
{
    Task SendAsync(string message, CancellationToken ct);
}

public sealed class EmailSender : INotificationSender
{
    public Task SendAsync(string message, CancellationToken ct) =>
        Task.CompletedTask;
}

public sealed class NotificationFactory
{
    public INotificationSender Create(string channel) =>
        channel.ToLowerInvariant() switch
        {
            "email" => new EmailSender(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel))
        };
}
```

**Use when:** creation logic is complex or multiple implementations are selected dynamically.

**Trade-off:** A factory can become a large conditional hub. Dependency injection registrations may be simpler for fixed dependencies.

## 2. Strategy Pattern

Strategy encapsulates interchangeable algorithms behind a common interface.

```csharp
public interface IDiscountStrategy
{
    decimal Apply(decimal total);
}

public sealed class VipDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.90m;
}

public sealed class RegularDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.98m;
}

public sealed class CheckoutService(IDiscountStrategy strategy)
{
    public decimal Calculate(decimal total) => strategy.Apply(total);
}
```

**Interview question:** Strategy vs inheritance?

Strategy favors composition and allows behavior to change at runtime. Inheritance creates a tighter type hierarchy and can violate substitutability when subclasses differ significantly.

## 3. Decorator Pattern

Decorator wraps an object to add behavior without changing the original implementation.

```csharp
public interface IProductService
{
    Task<Product> GetAsync(Guid id, CancellationToken ct);
}

public sealed class LoggingProductService(IProductService inner,
    ILogger<LoggingProductService> logger) : IProductService
{
    public async Task<Product> GetAsync(Guid id, CancellationToken ct)
    {
        logger.LogInformation("Getting product {ProductId}", id);
        return await inner.GetAsync(id, ct);
    }
}
```

**Use cases:** logging, caching, metrics, authorization checks, retries, and validation.

**Trade-off:** Many nested decorators can make debugging and execution order harder to understand.

## 4. Adapter Pattern

Adapter converts one interface into another expected by the client. It is useful when integrating legacy systems or third-party libraries.

```csharp
public interface IPaymentGateway
{
    Task ChargeAsync(decimal amount, CancellationToken ct);
}

public sealed class LegacyPaymentAdapter(LegacyPaymentClient client)
    : IPaymentGateway
{
    public Task ChargeAsync(decimal amount, CancellationToken ct) =>
        client.ExecuteChargeAsync((int)(amount * 100), ct);
}
```

**Interview question:** Adapter vs Facade?

- **Adapter:** changes an incompatible interface.
- **Facade:** provides a simpler interface over a complex subsystem.

## 5. Observer Pattern

Observer allows subscribers to receive notifications when a subject publishes an event.

```csharp
public sealed class OrderCreatedPublisher
{
    private readonly List<Func<Order, Task>> handlers = [];

    public void Subscribe(Func<Order, Task> handler) => handlers.Add(handler);

    public async Task PublishAsync(Order order)
    {
        foreach (var handler in handlers)
            await handler(order);
    }
}
```

For distributed systems, in-memory observers are not durable. Use a broker such as Azure Service Bus when delivery, retries, dead-lettering, or replay are required.

## Pattern selection guide

| Requirement | Suitable pattern |
|---|---|
| Select implementation based on input | Factory |
| Swap business algorithms | Strategy |
| Add cross-cutting behavior | Decorator |
| Integrate incompatible APIs | Adapter |
| Notify multiple subscribers | Observer or durable messaging |

## Common interview questions

### Should Singleton always be used for shared services?
No. Prefer dependency-injection-managed singletons when shared lifetime is required. Ensure thread safety, avoid mutable global state, and never store request-specific information in a singleton.

### Can patterns reduce performance?
Yes. Extra allocations, indirection, reflection, and excessive abstraction may add overhead. Measure before optimizing and favor clarity unless performance requirements justify complexity.

### What is the difference between a pattern and a principle?
A principle is a general design guideline, such as dependency inversion. A pattern is a repeatable structure for solving a specific design problem.

## References

- [Microsoft dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Refactoring Guru design patterns](https://refactoring.guru/design-patterns)
- [.NET architecture guidance](https://learn.microsoft.com/en-us/dotnet/architecture/)
