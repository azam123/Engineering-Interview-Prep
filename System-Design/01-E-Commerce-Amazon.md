# 🛒 E-Commerce System Design — Beginner Interview Guide

![Interview Prep](https://img.shields.io/badge/Goal-System%20Design%20Interview-6f42c1?style=for-the-badge)
![Level](https://img.shields.io/badge/Level-Beginner-2ea44f?style=for-the-badge)
![Format](https://img.shields.io/badge/Format-Step--by--Step-0969da?style=for-the-badge)
![Focus](https://img.shields.io/badge/Focus-Interview%20Prep-orange?style=for-the-badge)

> 🎯 **Purpose:** Prepare for the interview question: “Design an online shopping website like Amazon.”
>
> This guide is for beginners preparing for system design interviews—not a production implementation manual. Start simple, use plain language, draw the main components, and explain why you chose them.

## 🧭 Interview answer roadmap

1. Clarify the requirements.
2. Estimate traffic, storage and bandwidth.
3. Draw the high-level architecture.
4. Explain the data model and API examples.
5. Discuss caching, replication, sharding and consistent hashing.
6. Explain performance, scaling, availability, consistency and CAP.
7. Finish with security, monitoring and trade-offs.

## 1. 🛍️ Requirements

Imagine a website where customers find products, add them to a cart and place orders.

### Functional requirements (FR): what it does

| Requirement | Example |
|---|---|
| Browse and search | Find a phone or headphones |
| View details | See description, image and price |
| Manage cart | Add or remove an item |
| Checkout | Confirm items and delivery details |
| Make payment | Pay through a payment provider |
| Track order | See whether an order is confirmed or shipped |

### Non-functional requirements (NFR): how well it works

| Requirement | Meaning |
|---|---|
| Performance | Pages should load quickly |
| Availability | The website should usually be reachable |
| Scalability | It should handle more users as it grows |
| Reliability | A confirmed order should not disappear |
| Security | Customers can access only their own private information |
| Correctness | The system should not sell the same last item twice |

**Interview tip:** State your scope before designing. For example: “I’ll cover product browsing, cart, checkout, payment and order tracking. I’ll leave recommendations and seller tools out of the first version.”

## 2. 📊 Capacity estimation

Use simple assumptions and explain that the numbers are estimates, not real Amazon traffic.

Assume:
- 1 million active users per day.
- Each user views 10 product pages daily.
- Peak traffic is 3 times the average.
- One product API response is about 20 KB.
- 100,000 orders are placed per day.
- One order and its items average 2 KB in this simplified example.

### Requests per second (RPS)

**Product requests**
- Daily requests = 1,000,000 × 10 = 10,000,000.
- Average RPS = 10,000,000 ÷ 86,400 ≈ **116 RPS**.
- Peak RPS = 116 × 3 ≈ **350 RPS**.

**Orders**
- Average = 100,000 ÷ 86,400 ≈ **1.2 orders/second**.
- At a 3× peak, approximately **3.5 orders/second**.

RPS means requests per second. One user action can create several requests.

### Storage estimate

| Data | Calculation | Approximate size |
|---|---|---:|
| Product records | 1 million × 2 KB | 2 GB |
| Orders for one year | 100,000/day × 2 KB × 365 | 73 GB |
| Product images | Store separately | Depends on image count and size |

These estimates exclude indexes, backups, replicas and other overhead.

### Network and bandwidth

At 350 requests/second × 20 KB per response, response traffic is about **7 MB/second**. Images can add much more traffic, so a CDN can help deliver images closer to users.

**Remember these formulas**
- Average RPS = daily requests ÷ 86,400.
- Peak RPS = average RPS × peak factor.
- Bandwidth ≈ requests/second × average response size.
- Storage ≈ record count × average record size.

## 3. 🏗️ High-level architecture

This diagram shows the main building blocks and how they connect.

~~~mermaid
flowchart TD
    U["🔵 Customer / App"] --> CDN["🌍 CDN"]
    CDN --> API["🔷 Load Balancer / API"]
    API --> CAT["🟢 Product Service"]
    API --> CART["🟢 Cart Service"]
    API --> ORD["🟢 Order Service"]
    CAT --> CACHE[("🟠 Cache")]
    CAT --> PDB[("🟠 Product Database")]
    CART --> CDB[("🟠 Cart Database")]
    ORD --> ODB[("🔴 Order Database")]
    ORD --> INV["🔴 Inventory"]
    ORD --> PAY["🔴 Payment Provider"]
    ORD --> Q[["🟣 Message Queue"]]
    Q --> N["🟢 Notifications"]
    classDef user fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef service fill:#dcfce7,stroke:#16a34a,color:#14532d;
    classDef store fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    classDef critical fill:#fee2e2,stroke:#dc2626,color:#7f1d1d;
    classDef async fill:#f3e8ff,stroke:#9333ea,color:#581c87;
    class U,API,CDN user;
    class CAT,CART,ORD,N service;
    class CACHE,PDB,CDB,ODB store;
    class INV,PAY critical;
    class Q async;
~~~

| Component | What it does |
|---|---|
| CDN | Delivers images and public content from nearby locations |
| Load balancer | Shares traffic across application instances |
| Product service | Returns product details |
| Cart service | Stores selected items |
| Order service | Coordinates checkout and order status |
| Inventory | Checks and reserves stock |
| Payment provider | Processes payment |
| Database | Saves information |
| Cache | Keeps frequently used data ready |
| Message queue | Holds background work, such as sending email |

A first version can be a single application and database. Separate services only when there is a clear reason.

## 4. 🛒 Checkout flow

~~~mermaid
sequenceDiagram
    actor C as Customer
    participant A as Shopping API
    participant O as Order Service
    participant I as Inventory
    participant P as Payment Provider
    participant D as Database
    C->>A: Place order
    A->>O: Validate checkout
    O->>I: Check and reserve stock
    I-->>O: Stock available
    O->>P: Request payment
    P-->>O: Payment result
    O->>D: Save order status
    D-->>O: Saved
    O-->>C: Show order result
~~~

Explain it in your own words:
1. The customer clicks **Place Order**.
2. The system validates the cart, current price and delivery details.
3. Inventory checks and reserves stock.
4. The payment provider processes payment.
5. The order status is saved.
6. The customer sees the result.

If payment fails, do not mark the order as paid; release the stock reservation when appropriate. If payment times out, check the payment status before trying again.

## 5. 🗂️ Data model

A data model describes the main information the application stores and how it is related.

~~~mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : places
    ORDER ||--|{ ORDER_ITEM : contains
    PRODUCT ||--o{ ORDER_ITEM : included_in
    CUSTOMER ||--o{ CART_ITEM : owns
    PRODUCT ||--o{ CART_ITEM : selected
    CUSTOMER {
        string customer_id PK
        string name
        string email
    }
    PRODUCT {
        string product_id PK
        string title
        decimal price
    }
    ORDER {
        string order_id PK
        string customer_id FK
        string status
        decimal total
    }
    ORDER_ITEM {
        string order_id FK
        string product_id FK
        int quantity
        decimal price_at_purchase
    }
    CART_ITEM {
        string customer_id FK
        string product_id FK
        int quantity
    }
~~~

- **Customer:** who is shopping.
- **Product:** what is being sold.
- **Cart item:** what the customer plans to buy.
- **Order:** the purchase and its status.
- **Order item:** each product, quantity and purchase-time price.

Store the price at the time of purchase. A later price change should not rewrite an older order.

### Where should data be stored?

| Data | Beginner-friendly choice | Why |
|---|---|---|
| Customers and orders | Relational database, such as PostgreSQL | Relationships and transactions |
| Product search | Search index, if advanced search is needed | Text search and filters |
| Frequently read products | Cache, such as Redis | Faster repeated reads |
| Product images | Object storage + CDN | Large files and efficient delivery |
| Background work | Message queue | Work can happen outside the customer request |

Explain the reason for each choice instead of just listing technologies.

## 6. 🔌 API contracts

An API lets one part of an application ask another part to do something. The method and path describe the action.

### Find products

~~~http
GET /v1/products?query=headphones
~~~

~~~json
{
  "items": [
    {
      "productId": "p101",
      "title": "Wireless Headphones",
      "price": 79.99,
      "currency": "USD"
    }
  ]
}
~~~

### Add an item to the cart

~~~http
POST /v1/cart/items
Content-Type: application/json
~~~

~~~json
{
  "productId": "p101",
  "quantity": 2
}
~~~

### Place an order

~~~http
POST /v1/orders
Content-Type: application/json
Idempotency-Key: checkout-abc-123
~~~

~~~json
{
  "cartId": "cart123",
  "shippingAddressId": "address456",
  "paymentMethodToken": "provider-token"
}
~~~

Example response:

~~~json
{
  "orderId": "order789",
  "status": "PENDING"
}
~~~

The server must calculate the real price and check stock; do not trust a price sent by the browser. An idempotency key helps prevent a repeated checkout request from creating duplicate orders or charges.

**Common HTTP responses**
- `200 OK` — request succeeded.
- `201 Created` — a resource was created.
- `400 Bad Request` — input is invalid.
- `401 Unauthorized` — sign-in or valid authentication is required.
- `403 Forbidden` — the user is not allowed to do this.
- `404 Not Found` — resource does not exist.
- `409 Conflict` — for example, stock is no longer available.
- `429 Too Many Requests` — too many requests were sent.
- `500 Internal Server Error` — unexpected server problem.

## 7. ⚡ Caching

Caching keeps a copy of frequently requested data so the application does not need to read the database every time. Think of it as keeping popular products on a nearby shelf.

~~~mermaid
flowchart TD
    R["🟦 Product request"] --> C{"⚡ In cache?"}
    C -->|Yes| V["🟢 Return cached data"]
    C -->|No| D[("🟠 Read database")]
    D --> S["⚡ Save in cache"]
    S --> V
    classDef req fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef cache fill:#f3e8ff,stroke:#9333ea,color:#581c87;
    classDef db fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    class R,V req;
    class C,S cache;
    class D db;
~~~

- **Cache hit:** the item is already cached.
- **Cache miss:** read it from the database, then optionally cache it.
- **TTL:** how long cached data stays before it expires.
- **Invalidation:** remove or update cached data when the original changes.

Cache product descriptions and images. Always verify stock with the inventory system before confirming an order.

## 8. 🪞 Replication

Replication means keeping copies of the same data on more than one database server. It can improve availability and help serve more reads.

~~~mermaid
flowchart TD
    A["🟦 Application"] --> P[("🟠 Primary Database")]
    P --> R1[("🟢 Read Replica 1")]
    P --> R2[("🟢 Read Replica 2")]
    A -. "Read some data" .-> R1
    A -. "Read some data" .-> R2
    classDef app fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef primary fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    classDef replica fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class A app;
    class P primary;
    class R1,R2 replica;
~~~

- The **primary** usually handles writes.
- **Replicas** keep copies and may serve some reads.
- Replicas can lag behind the primary for a short time.
- If a user just changed an order, read from a source that can show the latest update.

**Remember:** replication copies data; sharding splits data into separate parts.

## 9. 🧩 Sharding

Sharding splits a large dataset across multiple databases. Imagine distributing customers' orders between several storage rooms instead of keeping every order in one room.

~~~mermaid
flowchart TD
    A["🟦 Order Service"] --> R{"🧭 Choose shard"}
    R --> S1[("🟠 Shard A: Customers 1–1000")]
    R --> S2[("🟠 Shard B: Customers 1001–2000")]
    R --> S3[("🟠 Shard C: Customers 2001–3000")]
    classDef app fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef router fill:#f3e8ff,stroke:#9333ea,color:#581c87;
    classDef shard fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    class A app;
    class R router;
    class S1,S2,S3 shard;
~~~

Common approaches:
- **Range-based:** assign ranges of customer IDs to different shards.
- **Hash-based:** calculate a hash from an ID to choose a shard.
- **Directory-based:** look up which shard owns a customer.

Sharding can increase capacity, but adds complexity. First consider indexes, query optimization and read replicas. Explain what problem sharding solves before recommending it.

## 10. 🔄 Consistent hashing

Consistent hashing assigns keys to servers so adding or removing a server usually moves fewer keys than a simple modulo-based approach.

Imagine servers around a circle. Hash a product key onto the same circle and assign it to the next server clockwise.

~~~mermaid
flowchart TD
    K["🔑 Product key"] --> H["#️⃣ Calculate hash"]
    H --> R["⭕ Find next server on ring"]
    R --> N["🟢 Selected server"]
    A["➕ Add a server"] --> M["🔁 Move only affected keys"]
    classDef key fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef hash fill:#f3e8ff,stroke:#9333ea,color:#581c87;
    classDef server fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class K key;
    class H,R,M hash;
    class N,A server;
~~~

**Why it matters in interviews:** it helps distribute cache keys or data across servers while reducing how much needs to move when the server group changes. It does not automatically provide backups or copy the data.

## 11. 🚀 Performance and scaling

Performance means how quickly the system responds. Scaling means increasing capacity when more people use it.

| Problem | Possible solution |
|---|---|
| Product pages are slow | Check database queries and add useful indexes |
| Popular products are read repeatedly | Use a cache and CDN |
| One application server is overloaded | Add more application instances |
| Database reads are too high | Cache reads or consider read replicas |
| Emails delay checkout | Send emails through a background queue |
| One product gets extremely popular | Protect the hot key and control incoming traffic |

- **Vertical scaling:** give one server more CPU, memory or storage.
- **Horizontal scaling:** add more servers and share traffic between them.

~~~mermaid
flowchart TD
    T["📈 Traffic increases"] --> Q{"Where is the bottleneck?"}
    Q -->|Application| H["↔️ Add app instances"]
    Q -->|Repeated reads| C["⚡ Cache / CDN"]
    Q -->|Database reads| R["🪞 Read replicas"]
    Q -->|Database capacity| S["🧩 Consider sharding"]
    classDef start fill:#dbeafe,stroke:#2563eb,color:#1e3a8a;
    classDef choice fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    classDef solution fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class T start;
    class Q choice;
    class H,C,R,S solution;
~~~

In an interview, explain why your solution helps. Adding servers will not automatically fix a slow database query.

## 12. 🛡️ High availability

High availability means the website continues working when a component fails, or recovers quickly.

- Run more than one application instance.
- Use a load balancer to send traffic to healthy instances.
- Back up important data and test restoring it.
- Use replicas and a tested failover process where appropriate.
- Use timeouts and limited retries when calling other services.
- Avoid a single component whose failure brings down everything.

**Interview example:** If one application server fails, the load balancer sends requests to healthy instances. If the database fails, recovery depends on its replication, backups and failover design.

## 13. ⚖️ Consistency and CAP theorem

### Consistency in simple terms

- **Strong consistency:** reads see the latest successful update according to the system's guarantees. Useful for inventory and important order changes.
- **Eventual consistency:** copies may briefly differ but catch up later. Often acceptable for search results and recommendations.
- **Read-your-writes:** a customer sees their own recent change, such as an item added to their cart.

### CAP theorem

CAP describes a distributed system when a network partition occurs—when parts of the system cannot reliably communicate.

- **C — Consistency:** reads follow the system's latest-data guarantee.
- **A — Availability:** working nodes return responses to requests.
- **P — Partition tolerance:** the system can deal with communication failures between nodes.

During a partition, a distributed system generally must choose between rejecting or delaying some requests to preserve consistency, or continuing to respond when data may be stale or conflicting.

~~~mermaid
flowchart TD
    P["🌐 Network connection breaks"] --> D{"Choose behavior for this operation"}
    D --> C["🔴 Preserve consistency: wait or reject"]
    D --> A["🟢 Favor availability: respond with possible stale data"]
    C --> X["Example: protect the last item in stock"]
    A --> Y["Example: show cached product details"]
    classDef problem fill:#ffedd5,stroke:#ea580c,color:#7c2d12;
    classDef con fill:#fee2e2,stroke:#dc2626,color:#7f1d1d;
    classDef avail fill:#dcfce7,stroke:#16a34a,color:#14532d;
    class P,D problem;
    class C,X con;
    class A,Y avail;
~~~

**Interview takeaway:** protect correctness for inventory and orders. Product browsing can often tolerate slightly old information. Explain the trade-off for each feature rather than labelling the entire website “CP” or “AP.”

## 14. 🔐 Security basics

- **Authentication:** confirm who the customer is.
- **Authorization:** check that the customer is allowed to view an order.
- **HTTPS/TLS:** protect data while it travels over the network.
- **Input validation:** reject invalid quantities and malformed requests.
- **Rate limiting:** restrict excessive requests and help prevent abuse.
- **Payment safety:** use a trusted payment provider and avoid storing raw card details.
- **Privacy:** do not expose passwords, tokens or private customer data in logs.

Example: knowing an order ID must not be enough for a stranger to view another customer's delivery address.

## 15. 🔭 Monitoring basics

Monitoring helps the team notice when something is wrong.

| Signal | Why it matters |
|---|---|
| Request count | Shows incoming traffic |
| Response time | Shows whether the website is slow |
| Error rate | Shows how many requests fail |
| CPU and memory | Helps find overloaded servers |
| Database health | Helps identify slow queries or connection problems |
| Payment failures | Shows checkout problems |
| Queue backlog | Shows whether background work is falling behind |

- **Metrics:** numbers over time, such as response time.
- **Logs:** records of events and errors.
- **Traces:** follow one request across services.

For an interview, explain what you would check first rather than designing a complete monitoring platform.

## 16. 🎤 Common interview questions

**Why use a cache?** To reduce repeated database reads and improve response time.

**Replication vs sharding?** Replication keeps copies; sharding divides data into parts.

**How do you avoid overselling?** Reserve stock atomically in the inventory system. Do not rely only on cached stock.

**Why use a queue?** To handle background tasks, such as sending email, without making the customer wait.

**What is horizontal scaling?** Adding more application instances to share traffic.

**When would you shard a database?** When measurements show one database cannot meet storage or throughput needs and simpler optimizations are insufficient.

**What is consistent hashing?** A way to distribute keys across servers while reducing key movement when servers change.

**What is CAP?** During a network partition, a system faces a trade-off between responding to requests and preserving a consistent view of data.

**How do you protect customer orders?** Authenticate users, verify order ownership, encrypt traffic and restrict access to private data.

**What would you do if the website became slow?** Check response-time metrics and logs, find whether the bottleneck is the application, database or another dependency, then optimize that part.

## ✅ Beginner interview checklist

- [ ] Explain functional and non-functional requirements.
- [ ] Calculate basic RPS, storage and bandwidth.
- [ ] Draw a simple high-level architecture.
- [ ] Explain the data model and API examples.
- [ ] Describe caching.
- [ ] Explain replication vs sharding.
- [ ] Describe consistent hashing at a high level.
- [ ] Compare vertical and horizontal scaling.
- [ ] Explain high availability and basic failure handling.
- [ ] Explain consistency and CAP trade-offs.
- [ ] Cover security and monitoring basics.

## ⭐ Final interview advice

You do not need to design every detail perfectly. Start with the requirements, draw a simple design, explain how a customer request flows through it, and introduce advanced concepts only when they solve a clear problem.

**A good beginner answer is clear, structured and easy to follow—not packed with jargon.**

## 🏷️ Hashtags

#SystemDesign #SystemDesignInterview #BeginnerFriendly #InterviewPreparation #SoftwareEngineering #BackendEngineering #HighLevelDesign #DatabaseDesign #API #Caching #Scalability #HighAvailability #CAPTheorem #DistributedSystems #TechInterviews
