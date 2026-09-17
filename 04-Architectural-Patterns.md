# Architectural Patterns

## Layered architecture
Separates presentation, application, domain, and infrastructure. Easy to understand, but can produce tight coupling and an anemic domain model.

## Clean architecture
Business rules point inward; infrastructure depends on application/domain abstractions.

## Hexagonal architecture
Ports define use cases and adapters connect databases, APIs, queues, and external systems.

```text
Adapters → Ports → Application/Domain ← Ports ← Adapters
```

## CQRS
Separates command models from query models. It can improve scalability and clarity but adds operational complexity.

## Event-driven architecture
Components communicate through events. Design for duplicate delivery, ordering limitations, retries, dead-letter queues, and eventual consistency.

## Microservices interview points
Use service boundaries based on business capabilities, not technical layers alone. Discuss data ownership, observability, deployment independence, distributed transactions, and platform maturity.

## References
- [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [Cloud design patterns](https://learn.microsoft.com/en-us/azure/architecture/patterns/)
