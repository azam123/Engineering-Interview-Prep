# .NET Concurrency, Channels & Backpressure — Principal Engineer Q&A

## Scenario
An ASP.NET Core API receives 10,000 events/minute, but a downstream provider can process only 2,000/minute. How do you prevent thread starvation and uncontrolled memory growth?

### Answer
Use bounded asynchronous buffering, explicit backpressure, cancellation, and bounded concurrency. `System.Threading.Channels` is a good in-process primitive. In production, a durable broker such as Azure Service Bus is preferable when events must survive process restarts.

```csharp
var channel = Channel.CreateBounded<OrderEvent>(new BoundedChannelOptions(1000)
{
    FullMode = BoundedChannelFullMode.Wait,
    SingleWriter = false,
    SingleReader = false
});

await channel.Writer.WriteAsync(evt, cancellationToken);

await Parallel.ForEachAsync(
    channel.Reader.ReadAllAsync(cancellationToken),
    new ParallelOptions { MaxDegreeOfParallelism = 16, CancellationToken = cancellationToken },
    async (item, ct) => await ProcessAsync(item, ct));
```

## Architecture Flow
```mermaid
flowchart LR
    A[HTTP Producers] --> B[Bounded Channel]
    B --> C[Worker Pool]
    C --> D[Downstream API]
    D --> E[Metrics + Tracing]
    B -. backpressure .-> A
```

## Principal-Level Follow-ups
- When should the channel become a durable broker?
- How do you guarantee idempotency after a worker retry?
- What metrics prove that capacity is insufficient?
- How would you handle poison messages?

## Key Trade-off
An in-memory channel provides low latency but loses buffered work when the process dies. Durable messaging adds operational cost but provides persistence, retries, dead-lettering, and independent scaling.

## Official Documentation
- https://learn.microsoft.com/dotnet/core/extensions/channels
- https://learn.microsoft.com/aspnet/core/fundamentals/best-practices
- https://learn.microsoft.com/dotnet/api/system.threading.channels
