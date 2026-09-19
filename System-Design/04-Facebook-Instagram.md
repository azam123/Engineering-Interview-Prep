# 📸 Facebook / Instagram System Design

> 📘 **Primary interview guide:** [Open the complete visual solution](04-Facebook-Instagram/solution.md)

```mermaid
flowchart LR
    U[👤 User] --> API[🟣 API Gateway]
    API --> POST[📝 Post Service]
    POST --> MEDIA[(🗄️ Media Storage)]
    POST --> EVT[📨 Event Bus]
    EVT --> FEED[🟢 Feed Fan-out]
    FEED --> CACHE[(⚡ Feed Cache)]
    API --> CACHE
```

## 🎯 Core Topics
- Feed generation and ranking
- Fan-out on write vs. read
- Celebrity and hot-key protection
- Media storage and CDN
- Caching, consistency, privacy, and observability

The detailed solution covers architecture, sequence flows, scaling strategies, and interview follow-ups.