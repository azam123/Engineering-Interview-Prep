# Python & FastAPI Production Interview Q&A

## 1. What makes an async FastAPI endpoint actually non-blocking?

The endpoint and its dependencies must avoid blocking the event loop. Async I/O libraries should be used for network/database operations. CPU-heavy work should be moved to a worker/process or specialized service. Calling a synchronous SDK directly from an async path can destroy concurrency.

```python
from fastapi import FastAPI
import httpx

app = FastAPI()

@app.get("/customers/{customer_id}")
async def get_customer(customer_id: str):
    async with httpx.AsyncClient(timeout=5.0) as client:
        response = await client.get(f"https://example.com/customers/{customer_id}")
        response.raise_for_status()
        return response.json()
```

## 2. Scenario: p95 latency increases although CPU is only 25%. What do you investigate?

Look for blocking I/O, connection-pool exhaustion, downstream latency, event-loop starvation, lock contention, DNS/TLS overhead and excessive object allocation. Measure before optimizing.

## 3. How should a production FastAPI service be structured?

Separate API schemas, application/use-case logic, domain logic and infrastructure adapters. Keep framework-specific code at the edges.

```text
app/
  api/          # HTTP routes and DTOs
  application/ # use cases
  domain/      # business rules
  infrastructure/ # DB, messaging, external APIs
  tests/
```

## 4. How do you implement graceful shutdown?

Stop accepting new work, allow in-flight operations to complete within a bounded period, close pools/clients and then exit. Kubernetes termination behavior should be aligned with the application's shutdown timeout.

## Principal-level point
Python concurrency is a system-design issue, not just syntax. Choose async I/O, threads, processes or distributed workers based on the workload's blocking and CPU characteristics.

## Official documentation
- https://fastapi.tiangolo.com/async/
- https://docs.python.org/3/library/asyncio.html
- https://docs.python.org/3/library/concurrent.futures.html
