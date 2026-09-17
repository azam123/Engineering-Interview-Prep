# SOLID Principles

## S — Single Responsibility
A class should have one reason to change. Separate validation, persistence, and notification responsibilities.

## O — Open/Closed
Software should be open for extension and closed for modification. Prefer strategies or policies over large conditional blocks.

## L — Liskov Substitution
Subtypes must be substitutable for their base types without breaking expected behavior. Avoid subclasses that throw `NotSupportedException` for inherited contracts.

## I — Interface Segregation
Clients should not depend on methods they do not use. Split large interfaces into focused contracts.

## D — Dependency Inversion
High-level policies depend on abstractions, not concrete infrastructure.

```csharp
public interface INotifier { Task SendAsync(string message); }

public sealed class OrderService(INotifier notifier)
{
    public Task NotifyAsync() => notifier.SendAsync("Order created");
}
```

## Interview discussion
SOLID is guidance, not a requirement to create excessive abstractions. Evaluate cohesion, change frequency, testability, and complexity before introducing interfaces.

## References
- [Microsoft dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
