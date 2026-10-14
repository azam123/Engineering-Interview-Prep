# 🛒 Amazon-Like E-Commerce System Design — End-to-End Guide

![System Design](https://img.shields.io/badge/System%20Design-End--to--End-6f42c1?style=for-the-badge)
![Level](https://img.shields.io/badge/Level-Beginner%20Friendly-2ea44f?style=for-the-badge)
![Architecture](https://img.shields.io/badge/Architecture-Distributed%20Systems-0969da?style=for-the-badge)
![Topics](https://img.shields.io/badge/Topics-Scalability%20%7C%20Reliability%20%7C%20Security-orange?style=for-the-badge)

> 🎯 **Goal:** Design an online shopping platform where browsing is fast, checkout is reliable, inventory is not oversold, payments are safe, and the system can grow from thousands to millions of users.
>
> 🧠 **Beginner analogy:** Think of a digital shopping mall. The catalog is the shop directory, the cart is your basket, inventory is the stockroom, payment is the cashier, and the event queue is the back-office message board.

## 🗺️ Learning roadmap
1. Requirements: what must the product do?
2. Capacity: how much traffic and data should it handle?
3. Architecture: which services and databases are needed?
4. Data model and API contracts: how do components exchange information?
5. Reliability: cache, replication, sharding, consistency, and CAP.
6. Operations: performance, scaling, security, and monitoring.

## 🎨 Diagram legend
- 🔵 **Blue:** user, edge, and API entry points
- 🟢 **Green:** application services
- 🟠 **Orange:** databases and caches
- 🟣 **Purple:** queues and asynchronous work
- 🔴 **Red:** payment, inventory, and correctness-sensitive operations

---

## 1. 🧭 Requirements gathering

Clarify the scope before drawing boxes. Ask whether the platform supports multiple countries, third-party sellers, multiple warehouses, promotions, returns, reviews, recommendations, and delivery tracking. This example covers the core shopping journey.

### Functional requirements (FR) — what the system does

| ID | Requirement | Example |
|---|---|---|
| FR-1 | Browse and search products | Search for wireless headphones |
| FR-2 | View product details | See description, price, images and availability |
| FR-3 | Manage a cart | Add, update or remove an item |
| FR-4 | Checkout | Validate price, address and stock |
| FR-5 | Reserve inventory | Temporarily hold stock during checkout |
| FR-6 | Pay and place an order | Authorize payment and create an order |
| FR-7 | Track or cancel an order | See status and request cancellation |
| FR-8 | Manage catalog and stock | Authorized sellers update listings and inventory |
| FR-9 | Send notifications | Send order confirmation and shipping updates |

### Non-functional requirements (NFR) — how well it works

Targets below are **illustrative design goals**, not claims about Amazon's actual production system.

| NFR | Example target | Design implication |
|---|---|---|
| Availability | 99.95% for core shopping APIs | Redundant instances and zones |
| Latency | Product reads p95 under 300 ms | Cache, indexes and nearby edge delivery |
| Durability | Confirmed orders must not be lost | Durable database, backups and recovery tests |
| Correctness | No overselling beyond business rules | Atomic stock reservation |
| Security | Protect identities, addresses and payments | Authentication, authorization, encryption |
| Scalability | Add capacity without redesigning everything | Stateless services, partitioning and queues |
| Observability | Quickly detect and diagnose failures | Metrics, logs, traces and alerts |

**Availability math:** 99.95% availability allows about 21.6 minutes of downtime in a 30-day month. An SLO is a target; measure it rather than assuming it.

---

## 2. 📊 Capacity estimation — make assumptions explicit

Estimates are not predictions. They help choose an initial architecture and identify which numbers need validating with real traffic.

### Example workload assumptions
- 10 million daily active users (DAU)
- 20 product-detail/list reads per user per day
- 1 million orders per day
- Average product API response: 20 KB
- Peak-to-average traffic multiplier: 5×
- Average order record plus items and metadata: 2 KB (simplified)
- Average image payload: 300 KB, typically served through a CDN

### Requests per second (RPS)

**Product reads**
- Daily reads = 10,000,000 × 20 = **200,000,000 reads/day**
- Average RPS = 200,000,000 ÷ 86,400 ≈ **2,315 RPS**
- Estimated peak RPS = 2,315 × 5 ≈ **11,575 RPS**, round to **12,000 RPS**

**Orders**
- Average orders/s = 1,000,000 ÷ 86,400 ≈ **11.6 orders/s**
- At a 5× peak, approximately **58 orders/s**. Flash sales may be much higher, so model bursts separately.

> ⚠️ **Important:** RPS is not the same as users. One user action can trigger several API calls, and retries can increase load.

### Data storage estimate

| Data | Simple calculation | Approximate raw size |
|---|---|---:|
| Product catalog | 100M products × 2 KB | 200 GB |
| Orders | 1M/day × 2 KB × 365 days | 730 GB/year |
| Product images | 100M images × 300 KB | 30 TB |
| Logs and events | Depends on event volume and retention | Measure separately |

These decimal approximations exclude indexes, replicas, backups, version history, metadata, and storage overhead. Order items can add materially to order size. Images belong in object storage, not as database blobs.

For a first-pass database budget, multiply raw structured data by a planning factor (for example, 2–4×) for indexes, replicas and overhead, then validate against the chosen database and retention policy.

### Network and bandwidth estimate

For 12,000 peak product API responses/s at 20 KB each:
- Payload throughput = 12,000 × 20 KB = **240,000 KB/s**
- Approximately **240 MB/s**, or **1.92 Gbit/s**, before protocol overhead.
- If CDN/cache hits remove 80% of those responses from the origin, origin response traffic could fall to roughly **48 MB/s**, assuming the same response size and request mix.

Images can dominate bandwidth. At 1,000 image requests/s × 300 KB, image delivery is about **300 MB/s**. A CDN can serve much of this near users and reduce origin load. Compression, cache-hit rate, protocol overhead and geography affect actual bandwidth.

### Estimation method
1. State assumptions.
2. Calculate daily volume.
3. Divide by 86,400 for average per-second rate.
4. Apply a peak factor and model bursts separately.
5. Estimate payload size × requests/s for bandwidth.
6. Add indexes, replication, backups and retention for storage.
7. Revisit assumptions with production telemetry and load tests.

---

## 3. 🏗️ High-level architecture

Services are split by business responsibility. They can be deployed and scaled independently, but each extra service adds network calls and operational complexity. Start simpler if traffic and team size do not justify many services.

~~~mermaid
flowchart TB
    U["🔵 Customer Web / Mobile"] --> EDGE["🌍 CDN + WAF"]
    EDGE --> GW["🔵 Load Balancer / API Gateway"]
    GW --> CAT["🟢 Catalog Service"]
    GW --> SEARCH["🟢 Search Service"]
    GW --> CART["🟢 Cart Service"]
    GW --> ORDER["🟢 Order Service"]
    GW --> ACCOUNT["🟢 Identity / Profile"]
    CAT --> REDIS[("🟠 Redis Cache")]
    CAT --> PDB[("🟠 Product Database")]
    SEARCH --> IDX[("🟠 Search Index")]
    CART --> CDB[("🟠 Cart Store")]
    ORDER --> ODB[("🔴 Order Database")]
    ORDER --> INV["🔴 Inventory Service"]
    ORDER --> PAY["🔴 Payment Service"]
    INV --> IDB[("🔴 Inventory Database")]
    PAY --> PSP["💳 Payment Provider"]
    ORDER --> OUTBOX[("🟠 Transactional Outbox")]
    OUTBOX --> BUS[["🟣 Event Bus / Queue"]]
    BUS --> FUL["🟢 Fulfillment"]
    BUS --> NOTIFY["🟢 Notifications"]
    BUS --> ANALYTICS["🟢 Analytics"]
    CAT --> OBJ[("🟠 Object Storage: Images")]
    classDef edge fill:#dbeafe,stroke:#2563eb,color:#1e3a8a,stroke-width:2px;
    classDef svc fill:#dcfce7,stroke:#16a34a,color:#14532d,stroke-width:2px;
    classDef data fill:#ffedd5,stroke:#ea580c,color:#7c2d12,stroke-width:2px;
    classDef critical fill:#fee2e2,stroke:#dc2626,color:#7f1d1d,stroke-width:2px;
    classDef async fill:#f3e8ff,stroke:#9333ea,color:#581c87,stroke-width:2px;
    class U,EDGE,GW edge;
    class CAT,SEARCH,CART,ORDER,ACCOUNT,INV,PAY,FUL,NOTIFY,ANALYTICS svc;
    class REDIS,PDB,IDX,CDB,ODB,IDB,OUTBOX,OBJ data;
    class INV,PAY,ODB,IDB critical;
    class BUS async;
~~~

| Component | Responsibility |
|---|---|
| CDN | Cache public images and other cacheable content close to customers |
| WAF / API gateway | TLS entry point, request limits, routing and policy enforcement |
| Catalog | Product details, descriptions and listing metadata |
| Search | Full-text search, ranking and filters |
| Cart | Customer's current basket; some eventual consistency is acceptable |
| Order | Order lifecycle and checkout coordination |
| Inventory | Authoritative stock counts and reservations |
| Payment | Payment-provider integration, idempotency and reconciliation |
| Event bus | Deliver durable events to independent consumers |
| Object storage | Product images and other large files |

**Synchronous work:** checkout validation and stock reservation, where the customer needs an immediate result.

**Asynchronous work:** email, analytics, search-index updates and recommendations. Use durable messages, retries with backoff, dead-letter handling, idempotent consumers and queue-age monitoring.

---

## 4. 🛍️ Checkout sequence and failure handling

~~~mermaid
sequenceDiagram
    autonumber
    actor C as Customer
    participant G as API Gateway
    participant O as Order Service
    participant I as Inventory Service
    participant P as Payment Service
    participant D as Order DB
    participant Q as Event Bus
    C->>G: Submit order + idempotency key
    G->>O: Authenticated checkout request
    O->>O: Validate cart, price and address
    O->>I: Reserve stock
    I-->>O: Reservation confirmed or rejected
    O->>P: Authorize payment with idempotency
    P-->>O: Authorized, declined or pending
    O->>D: Save order + outbox event atomically
    D-->>O: Commit successful
    O-->>C: Return order ID and status
    Q-->>Q: Publish order event for consumers
    Note over O,P: If outcome is uncertain, reconcile before retrying side effects
~~~

A robust workflow is usually a **saga**: a sequence of local transactions with compensating actions rather than one giant distributed database transaction.

1. Validate the latest price and cart contents on the server.
2. Reserve stock atomically with an expiry time.
3. Authorize payment using a stable idempotency key.
4. Persist order state and an outbox event in one database transaction.
5. Publish the event from the outbox; consumers must tolerate duplicate delivery.
6. Capture payment / confirm the order according to the business workflow.
7. If payment is declined, release the reservation. If the result is unknown, query the provider rather than blindly charging again.

**Why the transactional outbox matters:** saving an order and publishing an event as unrelated operations can lose the event if the process crashes between them. An outbox relay publishes committed records later.

---

## 5. 🗂️ Data model

Use a relational database for core transactional data when relationships, constraints and multi-row transactions matter. A search index is optimized for search, Redis for fast temporary lookups, and object storage for images.

~~~mermaid
erDiagram
    USER ||--o{ CART_ITEM : owns
    USER ||--o{ ORDER : places
    CART ||--o{ CART_ITEM : contains
    ORDER ||--|{ ORDER_ITEM : contains
    PRODUCT ||--o{ ORDER_ITEM : referenced_by
    PRODUCT ||--o{ INVENTORY : stocked_as
    ORDER ||--o{ PAYMENT : attempts
    ORDER ||--o{ STOCK_RESERVATION : reserves
    USER {
      uuid user_id PK
      string email UK
      string status
    }
    PRODUCT {
      uuid product_id PK
      uuid seller_id
      string title
      decimal current_price
      string status
    }
    INVENTORY {
      uuid product_id PK
      string warehouse_id PK
      int available_quantity
      int reserved_quantity
      int version
    }
    ORDER {
      uuid order_id PK
      uuid user_id
      string status
      decimal total_amount
      string currency
      datetime created_at
    }
    ORDER_ITEM {
      uuid order_id PK
      int line_number PK
      uuid product_id
      int quantity
      decimal unit_price_snapshot
    }
    PAYMENT {
      uuid payment_id PK
      uuid order_id
      string provider_reference
      string status
      string idempotency_key UK
    }
~~~

### Important modeling decisions
- Store a price snapshot on each order item because catalog prices may change later.
- Use decimal/fixed-point values or integer minor units (paise/cents) for money, never floating-point arithmetic.
- Track inventory by product and warehouse. Update available and reserved quantities atomically.
- Use explicit order/payment state machines rather than many unrelated booleans.
- Store payment-provider tokens/references, not raw card numbers or CVVs.
- Add indexes for real query patterns, such as user ID plus creation time for order history.
- Document which service owns each record; avoid multiple services writing directly to the same tables.

---

## 6. 🔌 API contracts

A contract specifies method, path, input, output and error behavior. These examples are illustrative JSON contracts, not a complete OpenAPI specification.

### Browse products

~~~http
GET /v1/products?query=headphones&page=1&pageSize=20
Authorization: Bearer <access-token>
~~~

~~~json
{
  "items": [
    {
      "productId": "prd_123",
      "title": "Wireless Headphones",
      "price": { "amount": "79.99", "currency": "USD" },
      "availability": "IN_STOCK",
      "imageUrl": "https://cdn.example.com/products/prd_123.webp"
    }
  ],
  "page": 1,
  "pageSize": 20,
  "hasMore": true
}
~~~

### Add an item to a cart

~~~http
POST /v1/carts/items
Authorization: Bearer <access-token>
Content-Type: application/json
~~~

~~~json
{
  "productId": "prd_123",
  "quantity": 2
}
~~~

Validate quantity and product eligibility on the server; never trust a client-supplied price.

### Create an order

~~~http
POST /v1/orders
Authorization: Bearer <access-token>
Idempotency-Key: 6f5f1f3c-unique-checkout-attempt
Content-Type: application/json
~~~

~~~json
{
  "cartId": "cart_456",
  "shippingAddressId": "addr_789",
  "paymentMethodToken": "provider_token_abc"
}
~~~

Example response:

~~~json
{
  "orderId": "ord_123",
  "status": "PENDING",
  "total": { "amount": "159.98", "currency": "USD" },
  "createdAt": "2026-10-14T10:00:00Z"
}
~~~

Return **201 Created** when a new order resource is created. If checkout is still processing, return a documented pending state or **202 Accepted**. Repeating the same idempotency key with the same request should return the same logical result; reject reuse with a different payload.

### Read an order

~~~http
GET /v1/orders/ord_123
Authorization: Bearer <access-token>
~~~

Only the owner or an authorized operator may read the order. Use cursor pagination for large order histories.

| Situation | Example response |
|---|---|
| Invalid request | 400 Bad Request |
| Missing/invalid authentication | 401 Unauthorized |
| Authenticated but not permitted | 403 Forbidden |
| Resource not found | 404 Not Found |
| Stock unavailable / conflicting state | 409 Conflict |
| Rate limit exceeded | 429 Too Many Requests |
| Temporary dependency failure | 503 Service Unavailable |

Use versioned contracts, validation, timeouts, pagination, consistent error bodies, correlation IDs and backward-compatible changes. Do not expose internal exception messages.

---

## 7. ⚡ Caching

Caching keeps frequently requested data closer to the application. Think of it like keeping popular products on a nearby shelf rather than walking to the warehouse for every request.

~~~mermaid
flowchart TD
    R["🟦 Request product"] --> C{"⚡ Cache hit?"}
    C -->|Yes| V["🟢 Return cached value"]
    C -->|No| DB[("🟠 Read product database")]
    DB --> SET["⚡ Store value with TTL"]
    SET --> V
    W["✏️ Product update"] --> DBW[("🟠 Commit database change")]
    DBW --> INV["🧹 Invalidate/update cache"]
    classDef request fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef cache fill:#f3e8ff,stroke:#9333ea,color:#581c87;
    classDef data fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    class R,V request;
    class C,SET,INV cache;
    class DB,DBW data;
~~~

Good cache candidates: product details, category lists, public images via CDN, and search suggestions. Use a TTL and a clear invalidation strategy. Plan for cache misses, outages, stampedes and hot keys; techniques include request coalescing, randomized TTLs and stale-while-revalidate.

**Never treat cached values as the source of truth** for stock reservation, payment status or authorization. A cache can display an approximate stock hint; inventory must make the final atomic decision.

---

## 8. 🪞 Replication

Replication keeps copies of data on more than one node. It helps availability and read throughput, but it does not automatically solve every failure or consistency problem.

~~~mermaid
flowchart LR
    APP["🟦 Application"] --> P[("🟠 Primary / Leader")]
    P -->|Replicate changes| R1[("🟢 Replica A")]
    P -->|Replicate changes| R2[("🟢 Replica B")]
    APP -->|Read-only query| R1
    APP -->|Read-only query| R2
    P -->|Failover when safe| R1
~~~

- **Leader/follower:** writes usually go to a leader; followers replay changes and may lag.
- **Synchronous replication:** waits for replica acknowledgement before confirming a write. It can improve durability but adds latency and may reduce write availability during network problems.
- **Asynchronous replication:** acknowledges earlier and copies changes later. It is faster, but failover can lose recently acknowledged writes if they were not replicated.
- **Failover:** promote an eligible replica using a controlled process that prevents two leaders from accepting writes (split brain).

Route read-after-write queries to the leader or use a consistency token/session policy when users must immediately see their own update. Monitor replication lag and test failover, backups and restoration.

---

## 9. 🧩 Sharding / partitioning

Sharding splits one logical dataset across multiple database partitions. It helps when a single database cannot handle the required storage, throughput or working set.

~~~mermaid
flowchart TD
    APP["🟦 Order Service"] --> ROUTER{"🧭 Shard router"}
    ROUTER -->|Customer range/hash A| S1[("🟠 Orders Shard A")]
    ROUTER -->|Customer range/hash B| S2[("🟠 Orders Shard B")]
    ROUTER -->|Customer range/hash C| S3[("🟠 Orders Shard C")]
    S1 --> REP1[("Replica A")]
    S2 --> REP2[("Replica B")]
    S3 --> REP3[("Replica C")]
~~~

Common strategies:
- **Range-based:** nearby keys go together. Good for range scans, but sequential IDs can create hotspots.
- **Hash-based:** spreads writes more evenly, but range queries across keys become harder.
- **Directory-based:** maps each tenant/customer to a shard. Flexible, but the directory must be highly available.
- **Geographic:** places data near a region for latency or residency needs, with clear ownership and failover rules.

Choose a shard key based on common queries, write distribution and data ownership. Avoid a key that sends all flash-sale writes for one popular product to one partition. Cross-shard joins and transactions are more complex; design APIs to minimize them.

---

## 10. 🔄 Consistent hashing

Consistent hashing distributes keys across a changing set of cache or service nodes while minimizing how many keys move when nodes are added or removed.

Imagine a circular track numbered from 0 to 2³²−1. Hash each node and key onto the track. A key belongs to the first node encountered clockwise. Virtual nodes place each physical server at multiple positions to improve balance.

~~~mermaid
flowchart TD
    K["🔑 Key: product-123"] --> H["#️⃣ Hash key"]
    H --> R["⭕ Find next clockwise node on ring"]
    R --> N["🟢 Node B owns key"]
    ADD["➕ Add Node D"] --> RING["🔁 Reassign keys in affected ring segments"]
    R --> RING
    classDef key fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef hash fill:#f3e8ff,stroke:#9333ea,color:#581c87;
    classDef node fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class K key;
    class H,R,RING hash;
    class N,ADD node;
~~~

With simple modulo hashing, changing the number of nodes can remap many keys. Consistent hashing usually limits movement to a smaller portion of the keyspace. It is common in distributed caches and some partition-routing designs.

It is not a complete sharding strategy by itself. You still need replication, rebalancing, failure detection, hot-key handling and safe data migration.

---

## 11. 🚀 Performance optimization

Optimize from measurements, not guesses.

1. **Measure latency percentiles:** p50, p95 and p99 reveal slow-tail behavior hidden by averages.
2. **Reduce network round trips:** avoid chatty service calls and fetch only required fields.
3. **Optimize database queries:** inspect query plans, add useful indexes and avoid N+1 queries.
4. **Cache read-heavy data:** use CDN and application caches with clear freshness rules.
5. **Paginate results:** prefer cursor pagination for large or changing datasets.
6. **Move slow side effects off the request path:** queue notifications and analytics.
7. **Apply timeouts and bounded retries:** use exponential backoff with jitter; never retry non-idempotent actions blindly.
8. **Protect against overload:** rate limits, bulkheads, bounded queues and load shedding.
9. **Compress appropriately:** reduce large responses while measuring CPU cost.
10. **Load-test realistic journeys:** include cache misses, flash-sale traffic, failures and ramp-up.

A useful diagnostic order is **client/edge → gateway → service → database/dependency → network**. Find where time is spent before adding machines.

---

## 12. 📈 Scaling strategy

- **Vertical scaling:** give a machine more CPU/RAM/IOPS. Easy initially, but has an upper limit.
- **Horizontal scaling:** add instances. Works well for stateless APIs behind a load balancer.
- **Read scaling:** cache, CDN and database read replicas.
- **Write scaling:** partitioning/sharding, batching where safe, and reducing unnecessary writes.
- **Workload smoothing:** queues and rate limits absorb bursts, provided queue capacity and processing delay are bounded.
- **Autoscaling:** scale on useful signals such as request concurrency, CPU, queue age and latency; scale down gradually to avoid oscillation.

~~~mermaid
flowchart TD
    T["📈 Traffic or latency increases"] --> D{"Where is the bottleneck?"}
    D -->|API CPU / concurrency| A["↔️ Add stateless API replicas"]
    D -->|Repeated reads| C["⚡ CDN / cache / read replicas"]
    D -->|Database writes| S["🧩 Partition, shard or optimize writes"]
    D -->|Slow side effects| Q["🟣 Queue + worker pool"]
    D -->|One hot key| H["🔥 Hot-key protection / key redesign"]
    classDef input fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef choice fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    classDef action fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class T input;
    class D choice;
    class A,C,S,Q,H action;
~~~

Scale the bottleneck rather than scaling every component equally. More replicas do not fix a slow query, hot partition, lock contention or downstream rate limit.

---

## 13. 🛡️ High availability and disaster recovery

High availability (HA) means the service can continue operating through expected failures. Disaster recovery (DR) is about restoring service and data after a larger outage.

- Deploy critical instances across multiple availability zones.
- Use health checks, load balancing and graceful draining during deployments.
- Avoid a single point of failure in gateways, credentials, queues and critical data stores.
- Use redundant dependencies and documented failover procedures.
- Back up data and **regularly test restores**; a successful backup job is not proof that recovery works.
- Define **RTO** (how quickly service must be restored) and **RPO** (how much data loss is acceptable).
- Use circuit breakers, timeouts, bulkheads and bounded queues to stop one failing dependency from cascading across the platform.
- Practice regional failover and verify DNS, secrets, replication, payment-provider connectivity and operational access.

Not every service needs the same HA target. Browsing may degrade gracefully; payment and order correctness require controlled recovery and reconciliation.

---

## 14. ⚖️ Consistency and the CAP theorem

### Consistency choices
- **Strong consistency:** after a successful write, a read observes that write according to the database's guarantees. Useful for inventory reservations and critical order state.
- **Eventual consistency:** replicas or derived views may temporarily differ but converge if updates stop. Useful for search indexes, recommendations, review counts and analytics.
- **Read-your-writes:** a customer sees their own cart/order update immediately, even if other readers may briefly see older data.

Use the weakest consistency model that still satisfies the business requirement. Stronger guarantees can cost latency or availability, depending on the database and failure conditions.

### CAP theorem, in plain English

CAP describes a distributed data system during a **network partition**—when nodes cannot reliably communicate. It cannot simultaneously guarantee both:

- **Consistency (C):** every successful read returns the latest committed value according to a single-copy model.
- **Availability (A):** every request to a non-failed node receives a non-error response.
- **Partition tolerance (P):** the system continues to operate despite messages being lost or delayed between parts of the system.

When a partition occurs, a system must choose whether to reject/delay some operations to preserve consistency, or continue responding with the possibility of stale/conflicting results. In real distributed systems, partition tolerance is not optional if the system must span unreliable networks; the practical trade-off is often between consistency and availability during a partition.

~~~mermaid
flowchart TD
    PART["🌐 Network partition occurs"] --> CHOICE{"Choose behavior for this operation"}
    CHOICE -->|Reject / wait for safe quorum| C["🔴 Preserve consistency"]
    CHOICE -->|Respond despite uncertain peer state| A["🟢 Favor availability"]
    C --> EX1["Example: reject a risky stock write"]
    A --> EX2["Example: show cached catalog data"]
    classDef partition fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    classDef consistent fill:#fee2e2,stroke:#dc2626,color:#7f1d1d;
    classDef available fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class PART,CHOICE partition;
    class C,EX1 consistent;
    class A,EX2 available;
~~~

**E-commerce application:** prefer correctness for inventory reservation, payment and order transitions. Allow eventual consistency for search results, recommendations and analytics. A system can make different choices for different operations; CAP is not a label that means an entire product is simply CP or AP in every circumstance.

---

## 15. 🔐 Security

Security is part of the architecture, not a final checkbox.

- **Authentication:** OAuth 2.0 / OpenID Connect with short-lived tokens; MFA for sensitive account actions.
- **Authorization:** enforce ownership and role checks on the server for customers, sellers, support staff and administrators.
- **Transport and storage:** TLS in transit, encryption at rest, managed secrets and key rotation.
- **Payments:** use a reputable payment provider and tokenization; never store CVV and avoid handling raw card data unless appropriately certified.
- **Input validation:** validate schemas, quantity limits, addresses, uploaded files and query parameters.
- **Abuse prevention:** per-user/IP rate limits, bot mitigation, checkout velocity controls and fraud signals.
- **Least privilege:** separate service identities and narrowly scoped database/cloud permissions.
- **Auditability:** log privileged actions and security events without logging passwords, tokens, card data or unnecessary personal information.
- **Supply chain:** patch dependencies, scan images and dependencies, protect CI/CD credentials and review infrastructure changes.
- **Privacy:** collect only necessary personal data, set retention rules and restrict access to order/address information.

Threat-model checkout, account takeover, coupon abuse, inventory manipulation, payment replay and data exfiltration. Security controls should be tested continuously.

---

## 16. 🔭 Monitoring and observability

Monitoring tells you **that** something is wrong; logs and traces help explain **why**.

| Area | Signals |
|---|---|
| API | Request rate, p50/p95/p99 latency, 4xx/5xx rate, timeouts |
| Database | CPU, connections, slow queries, locks, storage, replication lag |
| Cache | Hit ratio, evictions, latency, hot keys, connection errors |
| Inventory | Reservation success/failure, conflicts, expired reservations |
| Payment | Authorization/decline rate, timeouts, duplicate attempts, reconciliation backlog |
| Queue | Consumer lag, oldest message age, retries, dead-letter count |
| Infrastructure | CPU, memory, disk, network, pod restarts and saturation |
| Business | Checkout conversion, order success rate, cancellations and refund rate |

Use structured logs with request/correlation IDs, distributed traces across services, dashboards for technical health and business outcomes, alerts tied to user impact, actionable runbooks and synthetic checks. Do not put sensitive personal data, access tokens or payment information into logs.

---

## 17. 🧪 Failure scenarios to discuss in an interview

| Failure | Safer response |
|---|---|
| Cache is unavailable | Bypass carefully, limit database pressure and degrade noncritical reads |
| Inventory service times out | Do not assume stock was reserved; query status or retry idempotently |
| Payment times out | Treat result as unknown, query provider and reconcile before repeating |
| Event delivered twice | Consumer deduplicates by event ID or business key |
| Event publish fails after DB commit | Transactional outbox retries publishing |
| Replica is stale | Route critical read to leader or use read-your-writes policy |
| One product becomes extremely popular | Protect hot keys, queue requests and partition work where possible |
| A region fails | Follow tested failover procedure aligned to RTO/RPO |
| Database reaches capacity | Apply backpressure, optimize queries and plan safe partitioning |

---

## 18. 🎤 Interview-ready summary

A strong answer follows a logical sequence:
1. Clarify scope and list functional/non-functional requirements.
2. Estimate traffic, storage and bandwidth using stated assumptions.
3. Draw the high-level architecture and explain each component.
4. Define data ownership, core entities and API contracts.
5. Walk through checkout, including idempotency and failure compensation.
6. Explain caching, replication, sharding and consistent hashing only where justified.
7. Discuss consistency and CAP trade-offs for each type of data.
8. Finish with HA/DR, security, monitoring, bottlenecks and growth paths.

### Common follow-up questions
- **How do you prevent overselling?** Use atomic reservations, conditional updates, idempotency and expiration/compensation.
- **What if payment succeeds but the order update fails?** Use durable state, provider reconciliation, idempotent retries and compensating actions.
- **Why use an event bus?** It decouples slow side effects and lets independent consumers process durable events.
- **Why not shard immediately?** Sharding adds operational and query complexity. First measure, optimize and scale vertically/read replicas where appropriate.
- **What is the role of consistent hashing?** Reduce key movement as cache/partition nodes change; it does not replace replication or data migration plans.
- **Can a system be fully consistent and always available across a network partition?** Not under CAP's distributed-system assumptions; define behavior per operation.

---

## ✅ Final checklist
- [ ] Requirements and measurable NFRs
- [ ] Capacity assumptions, RPS, storage and bandwidth
- [ ] High-level architecture and checkout sequence
- [ ] Data model, ownership and indexes
- [ ] API request/response and error contracts
- [ ] Cache strategy and invalidation
- [ ] Replication and lag handling
- [ ] Sharding key and hot-partition strategy
- [ ] Consistent hashing and rebalancing
- [ ] Performance testing and scaling
- [ ] HA, backup, RTO and RPO
- [ ] Consistency and CAP trade-offs
- [ ] Authentication, authorization and data protection
- [ ] Metrics, logs, traces, alerts and runbooks

## 🏷️ Tags
#SystemDesign #SystemDesignInterview #DistributedSystems #Scalability #HighAvailability #Caching #Sharding #ConsistentHashing #CAPTheorem #DatabaseDesign #PerformanceEngineering #CloudArchitecture #BackendEngineering #SoftwareArchitecture #InterviewPreparation

> ⭐ **Takeaway:** Design for correctness first on checkout, inventory and payments; optimize browsing for speed; then scale the measured bottlenecks. A good system design is a set of explicit trade-offs, not just a large diagram.


---

## 19. 🗄️ Data storage choices — choose by access pattern

There is no single database that is best for every part of an e-commerce platform. Choose a store based on the questions the application asks of the data.

| Data / workload | Suitable starting choice | Why | Watch out for |
|---|---|---|---|
| Orders and payment state | Relational database (e.g., PostgreSQL/MySQL) | Transactions, constraints and reliable state changes | Index growth, lock contention and connection limits |
| Product catalog | Relational or document database | Flexible product attributes and clear ownership | Category-specific attributes can complicate schemas |
| Full-text search and filters | Search engine/index | Text relevance, facets and typo-tolerant search | Index is a derived view; it can lag behind the catalog |
| Cart | Redis or durable key-value/document store | Fast reads/writes by customer or cart ID | Expiry, persistence and cross-device behavior |
| Inventory | Transactional relational/key-value store with conditional writes | Atomic reservation and concurrency control | Hot SKUs and contention require careful design |
| Product images | Object storage + CDN | Cheap durable blob storage and global delivery | Image resizing, cache invalidation and access controls |
| Events and asynchronous jobs | Durable message broker/event stream | Decouples producers from consumers | Duplicate delivery, retries, ordering and backlog |
| Analytics | Warehouse/lakehouse | Large scans and historical reporting | Keep analytical workloads away from checkout databases |

**Practical rule:** use a relational database first for orders and inventory unless measurements or business requirements justify a different design. Polyglot persistence adds operational overhead; every extra database needs backups, monitoring, access controls and a recovery plan.

### Example: prevent overselling with an atomic stock reservation

A naive flow—read stock, then subtract stock in a second request—has a race condition. Two checkout requests may both read “1 item left.” Instead, perform the check and update as one atomic database operation, or use a transaction/conditional write with equivalent guarantees.

~~~sql
UPDATE inventory
SET available_quantity = available_quantity - :quantity,
    reserved_quantity = reserved_quantity + :quantity,
    version = version + 1
WHERE product_id = :product_id
  AND warehouse_id = :warehouse_id
  AND available_quantity >= :quantity;
~~~

If the update affects one row, the reservation succeeded. If it affects zero rows, stock is insufficient or the requested inventory record does not exist. In production, also create a reservation ID with an expiry time and make the operation idempotent so a retry cannot reserve twice. Release expired reservations safely and reconcile reservation state after failures.

---

## 20. 🔌 More API contracts: inventory, payment and order status

### Reserve inventory

~~~http
POST /v1/inventory/reservations
Authorization: Bearer <service-access-token>
Idempotency-Key: checkout-attempt-ord_123
Content-Type: application/json
~~~

~~~json
{
  "orderId": "ord_123",
  "items": [
    { "productId": "prd_123", "warehouseId": "wh_01", "quantity": 2 }
  ],
  "expiresInSeconds": 600
}
~~~

Example success response:

~~~json
{
  "reservationId": "res_456",
  "status": "RESERVED",
  "expiresAt": "2026-10-14T10:10:00Z"
}
~~~

Return a documented conflict response when there is not enough stock. Do not expose internal warehouse or database details unnecessarily.

### Payment status

~~~http
GET /v1/payments/pay_123
Authorization: Bearer <access-token>
~~~

~~~json
{
  "paymentId": "pay_123",
  "orderId": "ord_123",
  "status": "AUTHORIZED",
  "currency": "USD",
  "amount": "159.98"
}
~~~

Use signed webhooks from the payment provider, verify signatures and timestamps, deduplicate webhook event IDs, and reconcile with the provider when a timeout leaves the result unknown. A timeout does **not** prove that a payment failed.

### Order state machine

~~~mermaid
stateDiagram-v2
    [*] --> PENDING
    PENDING --> CONFIRMED: stock reserved + payment authorized
    PENDING --> FAILED: validation or payment failure
    PENDING --> CANCELLED: checkout expired
    CONFIRMED --> FULFILLING
    FULFILLING --> SHIPPED
    SHIPPED --> DELIVERED
    CONFIRMED --> CANCELLED: cancellation accepted
    DELIVERED --> RETURN_REQUESTED
    RETURN_REQUESTED --> REFUNDED: return approved and refund settled
    FAILED --> [*]
    CANCELLED --> [*]
    DELIVERED --> [*]
    REFUNDED --> [*]
~~~

Every transition should validate the current state and be safe to retry. Keep an auditable history for important transitions. Cancellation, stock release and refunds are workflows—not just a status-field update.

---

## 21. 🧭 Consistency by feature — make the trade-off explicit

| Feature | Suggested consistency | Reason |
|---|---|---|
| Product description and category | Eventual consistency is usually acceptable | A short indexing/cache delay is tolerable |
| Search results | Eventual consistency | Search is a derived index and may lag |
| Cart | Read-your-writes where practical | Customers expect their own edits to appear |
| Available inventory at checkout | Atomic, strongly coordinated reservation | Avoid selling the same unit twice |
| Payment status | Durable, idempotent state transitions plus reconciliation | External payment outcomes can be delayed or ambiguous |
| Order state | Transactional updates with explicit allowed transitions | Prevent contradictory lifecycle states |
| Analytics and recommendation events | Eventual consistency | Delayed aggregation does not block checkout |

Do not describe the entire platform as “strongly consistent” or “eventually consistent.” State which operation needs which guarantee, and explain how the application behaves when the guarantee cannot be provided.

---

## 22. 🚨 Example alerts and operational targets

The following are starting examples, not universal thresholds. Tune them to the service's SLO, normal traffic and customer impact.

| Alert | Example trigger | First response |
|---|---|---|
| API latency | p95 exceeds the SLO for 5–10 minutes | Check traces, dependency latency and recent deployments |
| API errors | 5xx rate rises above baseline | Check service health, saturation and dependency failures |
| Inventory conflicts | Sudden rise in reservation failures | Check stock levels, hot SKUs and sale traffic |
| Payment uncertainty | Provider timeouts or reconciliation backlog rises | Pause unsafe retries and reconcile provider state |
| Queue backlog | Oldest message age exceeds processing objective | Check consumer health, throughput and poison messages |
| Replica lag | Lag exceeds the application's read freshness requirement | Route critical reads safely and inspect replication health |
| Storage capacity | Forecast indicates low remaining headroom | Expand safely, reduce retention where permitted, or partition |
| Cache failure | Hit ratio collapses or connection errors spike | Protect the database from a cache-miss storm |

Use runbooks that specify an owner, diagnostic steps, rollback options and customer communication. An alert that nobody can act on is noise.

---

## 23. 🎨 One-page mental model

~~~mermaid
flowchart LR
    U["🔵 Customer"] --> B["🟢 Browse"] --> C["🟠 Cache / Search"] 
    U --> CO["🔴 Checkout"] --> I["🔴 Atomic stock reservation"] --> P["🔴 Idempotent payment"] --> O["🟠 Durable order state"]
    O --> E[["🟣 Outbox + event bus"]] --> F["🟢 Fulfillment / email / analytics"]
    MON["🔭 Metrics + logs + traces"] -. observes .-> B
    MON -. observes .-> CO
    MON -. observes .-> I
    MON -. observes .-> P
    classDef user fill:#dbeafe,stroke:#2563eb,color:#1e3a8a,stroke-width:2px;
    classDef browse fill:#dcfce7,stroke:#16a34a,color:#14532d,stroke-width:2px;
    classDef store fill:#ffedd5,stroke:#ea580c,color:#7c2d12,stroke-width:2px;
    classDef critical fill:#fee2e2,stroke:#dc2626,color:#7f1d1d,stroke-width:2px;
    classDef async fill:#f3e8ff,stroke:#9333ea,color:#581c87,stroke-width:2px;
    class U user;
    class B,F browse;
    class C,O store;
    class CO,I,P critical;
    class E async;
~~~

The core design principle is simple: **make browsing fast, make checkout correct, make background work durable, and make failures observable.** Add complexity only when the requirements or measured bottlenecks justify it.

#Amazon #ECommerce #SystemDesign #SystemDesignInterview #DistributedSystems #DatabaseDesign #Caching #Replication #Sharding #ConsistentHashing #CAPTheorem #HighAvailability #Scalability #PerformanceOptimization #API #Security #Observability #BackendEngineering #SoftwareArchitecture #InterviewPrep
