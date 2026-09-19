# 🚀 Engineering Interview Prep

[![C#](https://img.shields.io/badge/C%23-.NET-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/)
[![Azure](https://img.shields.io/badge/Azure-PaaS-0078D4?logo=microsoftazure&logoColor=white)](https://learn.microsoft.com/en-us/azure/)
[![GenAI](https://img.shields.io/badge/GenAI-RAG-6A1B9A)](#topics)
[![Interview Prep](https://img.shields.io/badge/Focus-Interview%20Preparation-success)](#how-to-use-this-repository)
[![Daily Updates](https://img.shields.io/badge/Updates-Daily-orange)](#daily-learning-plan)
[![DSA](https://img.shields.io/badge/DSA-LeetCode%20Top%20Interview%20150-FFA116?logo=leetcode&logoColor=black)](https://leetcode.com/studyplan/top-interview-150/)

A practical, continuously improving interview-preparation knowledge base for **backend engineers, Principal Engineers, Solution Architects, cloud engineers, and GenAI engineers**.

> Learn the concept → explain the design → write the code → visualize the flow → discuss trade-offs → handle production scenarios.

## 🎨 Beginner-Friendly Visual Learning

The notes use a consistent colorful and easy-to-follow format:

| Visual marker | Meaning |
|---|---|
| 🟦 | Question, input, or starting point |
| 🟢 | Simple explanation, success, or output |
| 💻 | Practical, readable code |
| 🔄 | Mermaid flow diagram |
| 🟠 | Interview tips, trade-offs, and production concerns |
| 🔗 | Official documentation and further reading |

📘 **Read the full format guide:** [Interview Notes Style Guide](INTERVIEW-NOTES-STYLE-GUIDE.md)

## 🏷️ Repository Tags

`csharp` `dotnet` `aspnetcore` `python` `sql` `azure` `paas` `data-factory` `system-design` `microservices` `distributed-systems` `genai` `rag` `llm` `dsa` `leetcode` `principal-engineer` `solution-architect` `interview-preparation`

## 📚 Topics

| # | Topic | Focus |
|---|---|---|
| 1 | [C# and .NET Core](01-CSharp-DotNet-Core.md) | Language, runtime, ASP.NET Core, performance, resilience |
| 2 | [SOLID Principles](02-SOLID-Principles.md) | Maintainable and testable object-oriented design |
| 3 | [Top 5 Design Patterns](03-Top-5-Design-Patterns.md) | Reusable object-oriented design solutions |
| 4 | [Architectural Patterns](04-Architectural-Patterns.md) | Microservices, clean architecture, CQRS, event-driven systems |
| 5 | [GenAI and RAG](05-GenAI-RAG-Interview-QA.md) | Retrieval, evaluation, security, agents, production design |
| 6 | [Python](06-Python-Interview-QA.md) | Core Python, async programming, APIs, testing |
| 7 | [SQL](07-SQL-Interview-QA.md) | Querying, indexing, transactions, optimization |
| 8 | [Azure PaaS](08-Azure-PAAS-Interview-QA.md) | Azure services, identity, networking, observability |
| 9 | [Azure Data Factory](09-Azure-Data-Factory.md) | ETL/ELT, incremental loads, triggers, CI/CD |
| 10 | [Principal & Architect Scenarios](10-Principal-Architect-Scenario-QA.md) | Production incidents, architecture trade-offs, GenAI/RAG, Azure, SQL, Python |
| 11 | [Advanced C#/.NET Scenarios](11-Advanced-CSharp-DotNet-Scenarios.md) | ValueTask, thread-pool starvation, async design |
| 12 | [SOLID Design Scenarios](12-SOLID-Design-Scenario-QA.md) | SRP, LSP, abstraction boundaries, DI |
| 13 | [Architecture Principal Scenarios](13-Architecture-Principal-Engineer-Scenarios.md) | Monolith evolution, APIs vs events, trade-offs |
| 14 | [GenAI/RAG Production Scenarios](14-GenAI-RAG-Production-Scenarios.md) | Retrieval debugging, authorization, evaluation, security |
| 15 | [Python Backend Scenarios](15-Python-Backend-Interview-Scenarios.md) | Async APIs, CPU workloads, retries and resilience |
| 16 | [SQL Performance Scenarios](16-SQL-Performance-Interview-Scenarios.md) | Execution plans, indexing, idempotent writes |
| 17 | [Azure PaaS Principal Scenarios](17-Azure-PAAS-Principal-Scenarios.md) | 503 diagnosis, managed identity, reliability |
| 18 | [Azure Data Factory Advanced Scenarios](18-Azure-Data-Factory-Advanced-Scenarios.md) | Watermarks, late data, recovery, data quality |
| 19 | [DSA — LeetCode Top Interview 150](DSA-LeetCode-150/README.md) | Patterns, complexity, interview questions, optimized C# solutions |

## 🧠 DSA — LeetCode Top Interview 150

Use the **official LeetCode Top Interview 150** as the canonical problem list: [LeetCode Top Interview 150](https://leetcode.com/studyplan/top-interview-150/).

The repository documents each problem using a consistent interview-first format:

- Problem link, difficulty, topic, pattern, data structure and algorithm
- Explanation with examples and interviewer clarifying questions
- Brute-force approach, C# solution and complexity
- Brute-force drawbacks and bottleneck analysis
- Optimized approach, C# solution and complexity
- Dry run and edge cases
- Further optimization discussion where applicable

## 🔄 Learning Flow

```mermaid
flowchart TD
    A[🟦 Understand Concept] --> B[🟢 Study Simple Example]
    B --> C[💻 Write or Review Code]
    C --> D[🔄 Follow Visual Diagram]
    D --> E[🟠 Explain Trade-offs]
    E --> F[🎯 Practice Scenario Question]
    F --> G[🧠 Mock Interview]
    G --> A
    style A fill:#dbeafe,stroke:#2563eb,color:#111827
    style B fill:#dcfce7,stroke:#16a34a,color:#111827
    style C fill:#f3e8ff,stroke:#9333ea,color:#111827
    style D fill:#fef3c7,stroke:#d97706,color:#111827
    style E fill:#fef3c7,stroke:#d97706,color:#111827
    style F fill:#fee2e2,stroke:#dc2626,color:#111827
    style G fill:#dcfce7,stroke:#16a34a,color:#111827
```

## 🎯 Question Format

Each topic is progressively enhanced with:

- 🟢 Fundamentals explained in simple English
- 🏠 Real-world analogies where useful
- 🔵 Practical code examples with meaningful comments
- 🟣 Simple colorful Mermaid diagrams
- 🟠 Production scenarios and troubleshooting
- 🔴 Security, scalability, reliability, and observability
- 🔗 Official documentation and further reading
- 📋 Quick revision points for interview preparation

## 🗓️ Daily Learning Plan

The content is intended to grow through daily additions. DSA solutions should be added incrementally without duplicating existing problems, while keeping the problem index synchronized with the solution files.

## 🧭 Interview Answer Framework

Use this structure for system-design and scenario questions:

```text
Clarify Requirements → State Assumptions → Propose Design → Explain Data Flow
→ Discuss Failure Modes → Security → Scalability → Observability → Trade-offs
```

## Reference Sources

- [Microsoft Learn](https://learn.microsoft.com/)
- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [Azure Well-Architected Framework](https://learn.microsoft.com/en-us/azure/well-architected/)
- [Python Documentation](https://docs.python.org/3/)
- [SQL Server Documentation](https://learn.microsoft.com/en-us/sql/)
- [LeetCode Top Interview 150](https://leetcode.com/studyplan/top-interview-150/)
