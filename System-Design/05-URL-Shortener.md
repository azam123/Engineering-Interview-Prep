# 🔗 URL Shortener System Design

> 📘 **Primary interview guide:** [Open the complete visual solution](05-URL-Shortener/solution.md)

```mermaid
flowchart LR
    U[👤 User] --> API[🟣 URL API]
    API --> DB[(🟢 URL Store)]
    API --> CACHE[(⚡ Redis Cache)]
    U --> R[➡️ Redirect API]
    R --> CACHE
    CACHE --> R
    R --> EVT[📨 Click Events]
    EVT --> A[📊 Analytics]
```

## 🎯 Core Topics
- Base62 or distributed ID generation
- Redirect latency and caching
- Collision prevention and custom aliases
- Analytics event streaming
- Abuse prevention, expiration, and availability

The detailed solution includes data model, APIs, capacity estimates, scaling, security, and observability.