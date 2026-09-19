# 🏗️ System Design Interview Preparation

A simple, interview-focused collection of system design problems using practical cloud architecture patterns.

## Problems

| # | Problem | Main Concepts |
|---|---|---|
| 1 | [E-Commerce System like Amazon](01-E-Commerce-Amazon/solution.md) | Catalog, cart, orders, payments, inventory |
| 2 | [YouTube](02-YouTube/solution.md) | Video upload, transcoding, CDN, streaming |
| 3 | [WhatsApp](03-WhatsApp/solution.md) | WebSockets, delivery, presence, offline messages |
| 4 | [Facebook / Instagram](04-Facebook-Instagram/solution.md) | Feed, media, fan-out, ranking |
| 5 | [URL Shortener](05-URL-Shortener/solution.md) | Base62, caching, redirects, analytics |
| 6 | [Google Docs](06-Google-Docs/solution.md) | Collaboration, document storage, conflict handling |

## 🎯 Recruiter-Shared Preparation Guide

- [Google Recruiter System Design Interview Guide](07-Google-Recruiter-System-Design-Guide.md) — recruiter-shared preparation themes, interview approach, quantitative design, APIs, trade-offs, reliability, and scalability.

## Standard Coverage in Every Design

Each solution should explain:

- Requirement gathering and scope
- Functional and non-functional requirements
- **DAU/MAU assumptions and traffic profile**
- **Average QPS, peak QPS, read QPS and write QPS**
- Capacity, data, network and bandwidth estimation
- **Daily/monthly/yearly storage growth**
- Database and storage choice with rationale
- Data model and API contracts
- High-level architecture and key request flows
- Caching, partitioning, replication and scaling
- Security, authentication and authorization
- Monitoring, logging, tracing and failure handling
- Interview follow-up questions and trade-offs

## Diagram Standard

Diagrams use Mermaid so they render directly on GitHub. They use:

- Clear left-to-right or top-to-bottom flows
- Separate groups for clients, APIs, async processing, databases and external systems
- Color-coded nodes with `classDef`
- Explicit arrows for synchronous and asynchronous communication
- Azure-oriented examples where appropriate, while noting cloud-neutral alternatives

## Interview Flow

```text
Gather requirements → Estimate DAU and QPS → Estimate storage/network
→ Select database and storage → Draw architecture
→ Define data model and APIs → Discuss bottlenecks and trade-offs
→ Explain security, scaling, reliability and observability
```

> All numbers are illustrative assumptions. In an interview, confirm the expected scale with the interviewer and explain your reasoning.
