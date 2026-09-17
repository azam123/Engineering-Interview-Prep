# SOLID Principles — Interview Q&A

SOLID is a set of design guidelines that improve maintainability, testability, and changeability. Apply them pragmatically; excessive abstraction can increase complexity.

## S — Single Responsibility Principle

A class should have one primary responsibility and one reason to change.

**Poor design:** an `OrderService` validates orders, writes to SQL, generates PDFs, and sends emails.

**Better design:** separate application orchestration from validation, persistence, and notification.

```csharp
public interface IOrderRepository
{
    Task SaveAsync(Order order, CancellationToken ct);
}

public interface IOrderValidator
{
    bool IsValid(Order order);
}

public sealed class OrderApplicationService(
    IOrderRepository repository,
    IOrderValidator validator)
{
    public async Task CreateAsync(Order order, CancellationToken ct)
    {
        if (!validator.IsValid(order))
            throw new ArgumentException("Invalid order");

        await repository.SaveAsync(order, ct);
    }
}
```

## O — Open/Closed Principle

Software entities should be open for extension but closed for repeated modification.

Replace growing `if/else` pricing logic with a strategy abstraction when new pricing rules are expected frequently.

```csharp
public interface IDiscountPolicy
{
    decimal Apply(decimal amount);
}

public sealed class PremiumDiscount : IDiscountPolicy
{
    public decimal Apply(decimal amount) => amount * 0.90m;
}

public sealed class NoDiscount : IDiscountPolicy
{
    public decimal Apply(decimal amount) => amount;
}
```

## L — Liskov Substitution Principle

A subtype must honor the behavioral contract of its base abstraction. If a subtype cannot support an operation, the abstraction may be incorrect.

**Warning sign:** subclasses frequently throw `NotSupportedException`, weaken validation, or return surprising results.

Prefer capability-based interfaces when behavior differs:

```csharp
public interface IReadable
{
    string Read();
}

public interface IWritable
{
    void Write(string value);
}
```

## I — Interface Segregation Principle

Clients should not depend on methods they do not need.

```csharp
public interface IReportReader
{
    Task<Report> GetAsync(Guid id, CancellationToken ct);
}

public interface IReportWriter
{
    Task SaveAsync(Report report, CancellationToken ct);
}
```

Small interfaces improve substitution and make unit tests more focused. Do not split interfaces merely to reduce the number of methods; cohesion matters.

## D — Dependency Inversion Principle

High-level business rules should depend on abstractions rather than concrete infrastructure implementations.

```csharp
public interface INotifier
{
    Task SendAsync(string message, CancellationToken ct);
}

public sealed class OrderService(INotifier notifier)
{
    public Task NotifyAsync(CancellationToken ct) =>
        notifier.SendAsync("Order created", ct);
}
```

The composition root wires the implementation:

```csharp
builder.Services.AddScoped<INotifier, EmailNotifier>();
```

## Common interview questions

### Is SOLID always required?
No. SOLID is guidance. A small, stable feature may not need multiple interfaces or patterns. Consider change frequency, cohesion, testability, and operational complexity.

### Does dependency inversion mean every class needs an interface?
No. Use abstractions at volatile or externally controlled boundaries, such as payment providers, databases, queues, and time. A simple pure domain class may not need an interface.

### SRP vs separation of concerns?
Separation of concerns is the broader idea of isolating different concerns. SRP applies that idea to the reasons a particular module or class changes.

### How can SOLID be misused?
- Creating interfaces for every class without a real substitution need.
- Adding layers that only forward calls.
- Overusing inheritance instead of composition.
- Hiding simple logic behind unnecessary factories.
- Treating principles as rigid rules instead of trade-offs.

## Quick review table

| Principle | Interview keyword | Typical smell |
|---|---|---|
| SRP | One reason to change | God class |
| OCP | Extend without modifying core logic | Large conditional chains |
| LSP | Behavioral substitutability | Unsupported inherited methods |
| ISP | Focused interfaces | Fat interfaces |
| DIP | Depend on abstractions | Newing infrastructure inside business logic |

## References

- [Microsoft dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Microsoft architecture guidance](https://learn.microsoft.com/en-us/dotnet/architecture/)
