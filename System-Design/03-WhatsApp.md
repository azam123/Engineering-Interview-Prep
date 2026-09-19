# 💬 WhatsApp System Design

> 📘 **Primary interview guide:** [Open the complete visual solution](03-WhatsApp/solution.md)

```mermaid
flowchart LR
    U[📱 Users] --> GW[🟣 Gateway]
    GW --> WS[🔌 WebSocket Service]
    WS --> MSG[🟢 Message Service]
    MSG --> Q[(📨 Durable Queue)]
    Q --> STORE[(🗄️ Message Store)]
    Q --> PUSH[🔔 Push Notifications]
    WS --> PRES[🟡 Presence Cache]
```

## 🎯 Core Topics
- WebSockets and connection routing
- Message ordering and delivery receipts
- Offline message storage
- Presence, retries, and idempotency
- Encryption, partitioning, scaling, and monitoring

See the detailed solution for complete flows, estimates, APIs, and trade-offs.