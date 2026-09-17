# Top 5 Design Patterns

## 1. Factory
Encapsulates object creation and avoids coupling callers to concrete implementations.

## 2. Strategy
Encapsulates interchangeable algorithms behind an interface.

```csharp
public interface IDiscount { decimal Apply(decimal total); }
public sealed class VipDiscount : IDiscount
{
    public decimal Apply(decimal total) => total * .9m;
}
```

## 3. Decorator
Adds behavior without modifying the wrapped object; useful for logging, caching, and authorization.

## 4. Adapter
Converts one interface into another expected by the client; useful when integrating legacy systems.

## 5. Observer
Publishes state changes to subscribers. In distributed systems, prefer durable messaging when delivery and replay matter.

## Interview trade-offs
Discuss object lifetime, thread safety, testability, coupling, performance, and whether dependency injection already provides a simpler solution. Avoid manually implemented Singleton where the DI container can manage lifetime.

## References
- [Microsoft dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Refactoring Guru design patterns](https://refactoring.guru/design-patterns)
