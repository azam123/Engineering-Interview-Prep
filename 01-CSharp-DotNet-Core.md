# C# and .NET Core Interview Q&A

## 1. Value type vs reference type
Value types store data directly; reference types store a reference to an object. Examples: `int`, `struct`, and `enum` are value types; classes, arrays, and delegates are reference types.

## 2. `async` and `await`
`async` enables asynchronous methods, while `await` asynchronously waits without blocking the calling thread.

```csharp
public async Task<string> GetDataAsync(CancellationToken ct)
{
    using var response = await _httpClient.GetAsync("api/data", ct);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync(ct);
}
```

## 3. DI lifetimes
- **Transient:** new instance every resolution.
- **Scoped:** one instance per request scope.
- **Singleton:** one instance for the application lifetime.

Avoid injecting scoped services into singletons unless a safe scope is explicitly created.

## 4. Middleware
Middleware forms a request pipeline. Each component can process the request, call the next component, and process the response.

```text
Request → Exception Handler → Authentication → Authorization → Endpoint → Response
```

## 5. Common interview topics
- Records vs classes
- `IEnumerable<T>` vs `IQueryable<T>`
- LINQ deferred execution
- `Task` vs `ValueTask`
- Cancellation tokens
- Exception handling and global problem details
- Minimal APIs vs controllers
- API versioning and validation
- Authentication with OAuth2/OIDC
- Unit, integration, and contract testing
- Caching, resilience, and observability

## Scenario
**How would you design a resilient API client?**
Use `HttpClientFactory`, timeouts, cancellation tokens, bounded retries for transient errors, exponential backoff, circuit breakers, structured logging, and idempotency where required.

## References
- [C# documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [.NET documentation](https://learn.microsoft.com/en-us/dotnet/)
- [ASP.NET Core fundamentals](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/)
