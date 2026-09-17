# C# and .NET Core Interview Q&A

## 1. Value types vs reference types

**Value types** contain their data directly. Assigning a value type copies the value. **Reference types** hold a reference to an object, so two variables may refer to the same object.

```csharp
int a = 10;
int b = a;
b++;
// a = 10, b = 11

var first = new Person { Name = "A" };
var second = first;
second.Name = "B";
// first.Name is now "B"

public sealed class Person
{
    public string Name { get; set; } = string.Empty;
}
```

**Interview point:** A value type can be boxed into an object, which allocates an object on the managed heap. Excessive boxing can affect performance.

## 2. Class vs record vs struct

- **Class:** reference type; suitable for entities and objects with identity.
- **Record:** designed for value-based equality and immutable-style data modeling.
- **Struct:** value type; best for small, logically value-based data that does not require inheritance.

```csharp
public record CustomerId(Guid Value);
public sealed class Customer(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}
```

## 3. `async` and `await`

Asynchronous programming allows a thread to perform other work while waiting for I/O such as HTTP, database, or file operations. `async` does not automatically create a new thread.

```csharp
public async Task<string> GetDataAsync(
    HttpClient client,
    CancellationToken cancellationToken)
{
    using var response = await client.GetAsync(
        "api/data", cancellationToken);

    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync(cancellationToken);
}
```

**Common mistakes:**

- Using `.Result` or `.Wait()` in request code.
- Forgetting to propagate cancellation tokens.
- Starting unbounded parallel operations.
- Using async methods for CPU-bound work without understanding the workload.

## 4. `Task` vs `ValueTask`

`Task` is the default choice for asynchronous APIs. `ValueTask` can reduce allocations when a result is frequently available synchronously, but it has usage restrictions and adds complexity. Use it only after measuring a real performance need.

## 5. `IEnumerable<T>` vs `IQueryable<T>`

- `IEnumerable<T>` generally performs operations in application memory.
- `IQueryable<T>` allows a provider, such as Entity Framework Core, to translate expressions into a query executed by the data source.

```csharp
IQueryable<User> query = db.Users
    .Where(u => u.IsActive)
    .OrderBy(u => u.Name);

var users = await query
    .Select(u => new { u.Id, u.Name })
    .ToListAsync(cancellationToken);
```

**Interview point:** Project only the required columns and avoid calling `ToList()` before filtering.

## 6. LINQ deferred execution

Many LINQ operators do not execute immediately. The query executes when enumerated, for example through `foreach`, `ToList()`, or `FirstOrDefault()`.

```csharp
var query = numbers.Where(n => n > 10);
numbers.Add(20);
var result = query.ToList(); // 20 is included
```

Use materialization deliberately when you need a stable snapshot or want to avoid repeated database execution.

## 7. Dependency injection lifetimes

- **Transient:** new instance each time it is requested.
- **Scoped:** one instance per dependency-injection scope; commonly one per HTTP request.
- **Singleton:** one instance for the application lifetime.

```csharp
builder.Services.AddTransient<IEmailFormatter, EmailFormatter>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
```

Never inject a scoped service directly into a singleton. Also ensure singleton services are thread-safe and do not retain request-specific data.

## 8. Middleware pipeline

Middleware components execute in registration order for the request and in reverse order for the response.

```text
Request
  ↓
Exception Handling
  ↓
Logging / Correlation ID
  ↓
Authentication
  ↓
Authorization
  ↓
Routing / Endpoint
  ↓
Response
```

```csharp
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

## 9. Exception handling and Problem Details

Do not expose stack traces, connection strings, or internal exception details to clients. Centralize exception handling and return consistent error responses.

```csharp
builder.Services.AddProblemDetails();
app.UseExceptionHandler();
```

Use exceptions for exceptional conditions, not routine validation flow. Log exceptions with a correlation identifier and relevant structured properties.

## 10. Minimal APIs vs controllers

**Minimal APIs** are concise and useful for small services, lightweight endpoints, and focused APIs. **Controllers** provide conventions and structure that may be helpful for large APIs with filters, model binding, and organization by feature.

The choice should be based on team conventions, complexity, testing needs, and maintainability—not performance assumptions alone.

## 11. Resilient HTTP clients

Use `IHttpClientFactory` and apply:

- Explicit timeouts.
- Cancellation tokens.
- Limited retries only for transient failures.
- Exponential backoff with jitter.
- Circuit breakers for repeatedly failing dependencies.
- Idempotency protection for retried writes.
- Structured logs and metrics.

```csharp
builder.Services.AddHttpClient<PaymentClient>(client =>
{
    client.BaseAddress = new Uri("https://payments.example.com/");
    client.Timeout = TimeSpan.FromSeconds(10);
});
```

A retry should not be used blindly for `POST` operations that may create duplicate transactions.

## 12. Authentication and authorization

Authentication answers **who the caller is**. Authorization answers **what the caller is allowed to do**.

For APIs, validate issuer, audience, signature, expiry, and required scopes or roles. Prefer standards-based OAuth 2.0 and OpenID Connect integrations rather than custom token formats.

## 13. API design checklist

- Use resource-oriented URLs and correct HTTP methods.
- Validate input at the boundary.
- Return consistent error contracts.
- Support pagination for large collections.
- Use idempotency keys for retryable business operations.
- Apply authentication, authorization, rate limits, and request size limits.
- Add correlation IDs, structured logging, metrics, and tracing.
- Version APIs when breaking changes are unavoidable.

## 14. Scenario question: How do you improve a slow API?

1. Measure latency using distributed tracing and endpoint metrics.
2. Identify whether time is spent in database, network, serialization, or application code.
3. Inspect database execution plans and indexes.
4. Reduce payload size and project only required fields.
5. Add caching where consistency requirements allow it.
6. Avoid synchronous blocking and unnecessary allocations.
7. Load-test the change and compare p50, p95, and p99 latency.

## 15. Scenario question: How do you handle graceful shutdown?

Use cancellation tokens, stop accepting new work, allow in-flight operations to finish within a bounded period, and make background handlers safe to restart. For queue consumers, acknowledge a message only after successful processing and design for duplicate delivery.

## 16. Testing strategy

- **Unit tests:** isolate business logic using controlled dependencies.
- **Integration tests:** validate real database, messaging, or HTTP boundaries.
- **Contract tests:** verify compatibility between service providers and consumers.
- **End-to-end tests:** validate critical user journeys.

Avoid mocking every internal implementation detail. Prefer testing observable behavior.

## References

- [C# documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [.NET documentation](https://learn.microsoft.com/en-us/dotnet/)
- [ASP.NET Core fundamentals](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/)
- [Dependency injection in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [ASP.NET Core error handling](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
- [HttpClientFactory](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)
