# 🛒 E-Commerce System Design (Amazon-like)

> 📘 **Primary interview guide:** [Open the complete visual solution](01-E-Commerce-Amazon/solution.md)

```mermaid
flowchart LR
    U[👤 User] --> API[🟣 API Gateway]
    API --> C[🟢 Catalog]
    API --> O[🔵 Orders]
    API --> P[🟠 Payments]
    API --> I[🔴 Inventory]
    O --> E[(📨 Event Bus)]
    E --> N[🔔 Notifications]
```

## 🎯 Core Topics
- Catalog and search
- Cart and checkout
- Inventory consistency
- Payment idempotency
- Event-driven order processing
- Caching, scaling, security, and observability

The detailed solution contains requirements, estimations, data model, APIs, diagrams, bottlenecks, and interview follow-ups.