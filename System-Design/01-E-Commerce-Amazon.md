# 🛒 E-Commerce System Design (Amazon-like)

![System Design](https://img.shields.io/badge/System%20Design-End--to--End-6f42c1?style=for-the-badge)
![Beginner Friendly](https://img.shields.io/badge/Guide-Beginner%20Friendly-2ea44f?style=for-the-badge)
![Architecture](https://img.shields.io/badge/Architecture-Distributed%20Systems-0969da?style=for-the-badge)
![Focus](https://img.shields.io/badge/Focus-Scalability%20%7C%20Reliability-orange?style=for-the-badge)

> 🎯 **Interview goal:** Design a reliable shopping platform where product discovery is fast, checkout is safe, inventory is accurate, and services can scale independently.

## 🧭 Complete visual guide

➡️ **[Open the complete end-to-end system design](01-E-Commerce-Amazon/solution.md)**

The full guide uses colorful Mermaid diagrams that render on GitHub and beginner-friendly explanations of the major design decisions.

## 🗺️ What's covered?

- 📋 Functional requirements (FR) and non-functional requirements (NFR)
- 📊 Capacity estimation: RPS, storage, network and bandwidth
- 🏗️ High-level architecture and checkout sequence
- 🗂️ Data model, database selection and API contracts
- ⚡ Cache-aside, cache invalidation and CDN
- 🪞 Replication, leader/follower, lag and failover
- 🧩 Sharding strategies and shard-key trade-offs
- 🔄 Consistent hashing and virtual nodes
- 🚀 Performance optimization and horizontal/vertical scaling
- 🛡️ High availability, disaster recovery, RTO and RPO
- ⚖️ Strong/eventual consistency and the CAP theorem
- 🔐 Authentication, authorization, payment safety and privacy
- 🔭 Monitoring, logs, metrics, traces, alerts and failure scenarios
- 🎤 Interview follow-up questions and final checklist

## 🎨 Architecture at a glance

~~~mermaid
flowchart LR
    U["🔵 Customer"] --> GW["🚪 API Gateway"]
    GW --> CAT["🟢 Catalog"]
    GW --> CART["🟢 Cart"]
    GW --> ORD["🟢 Order"]
    ORD --> INV["🔴 Inventory"]
    ORD --> PAY["🔴 Payment"]
    ORD --> BUS[["🟣 Event Bus"]]
    BUS --> NOTIFY["🟢 Notifications"]
    CAT --> CACHE[("🟠 Redis Cache")]
    CAT --> DB[("🟠 Product DB")]
    ORD --> ODB[("🔴 Order DB")]
    classDef client fill:#dbeafe,stroke:#2563eb,color:#1e3a8a,stroke-width:2px;
    classDef service fill:#dcfce7,stroke:#16a34a,color:#14532d,stroke-width:2px;
    classDef data fill:#ffedd5,stroke:#ea580c,color:#7c2d12,stroke-width:2px;
    classDef critical fill:#fee2e2,stroke:#dc2626,color:#7f1d1d,stroke-width:2px;
    classDef async fill:#f3e8ff,stroke:#9333ea,color:#581c87,stroke-width:2px;
    class U,GW client;
    class CAT,CART,ORD,NOTIFY service;
    class CACHE,DB data;
    class INV,PAY,ODB critical;
    class BUS async;
~~~

## 🏷️ Tags

#SystemDesign #SystemDesignInterview #DistributedSystems #Scalability #HighAvailability #Caching #Sharding #ConsistentHashing #CAPTheorem #DatabaseDesign #PerformanceEngineering #CloudArchitecture #BackendEngineering #SoftwareArchitecture #InterviewPreparation
