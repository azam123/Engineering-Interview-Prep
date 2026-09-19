# Design Facebook / Instagram

## 🌈 Visual Learning Edition

> 🎯 **Mental model:** A social feed is a personalized newspaper assembled from many creators, while celebrity posts behave like sudden traffic storms.

> 🎨 **Visual legend:** 🔵 Client · 🟢 Services · 🟠 Storage · 🟣 Async processing · 🔴 Hot path

## 🧭 Requirement Gathering
Clarify whether the scope includes profiles, follow/friend relationships, photo/video upload, feed ranking, stories, comments, notifications and search.

## ⚙️ Functional Requirements
- Create profiles and follow/friend users.
- Upload photos/videos with privacy controls.
- Generate a personalized feed.
- Like, comment, share and notify users.
- Support pagination and content reporting.

## 🛡️ Non-Functional Requirements
High availability, fast feed reads, scalable media delivery, privacy, eventual consistency for likes and feed ranking, and graceful degradation.

## 📊 Capacity Estimation
Assume 300 million daily active users and 20 feed opens/user/day: 6 billion feed requests/day, around 69,000 average requests/second; peak may be 5x. Assume 20 million media uploads/day.

## 💾 Data Estimation
If each media object averages 2 MB, uploads create about 40 TB/day. Use object storage and CDN. Store only metadata and references in the database.

## 🌐 Network Estimation
Feed responses should contain metadata and small thumbnails; full media comes from a CDN. Compress JSON and use adaptive image/video sizes.

## 🏗️ High-Level System Design

```mermaid
flowchart LR
    U([👤 User]) --> G[🚪 API Gateway]
    G --> F[🟢 Feed Service]
    F --> C[(🟠 Feed Cache)]
    F --> Graph[(🟠 Social Graph)]
    F --> Posts[(🟠 Post Store)]
    U --> Upload[🟢 Media Upload]
    Upload --> Object[(🔵 Object Storage)]
    Object --> CDN[🌍 CDN]
    Events[[🟣 Post Events]] --> Fanout[🟣 Fan-out Workers]
    Fanout --> C
    Posts --> Hot{🔴 Celebrity?}
    Hot -->|Yes| Index[(🔴 Celebrity Index)]
    Hot -->|No| Fanout

    classDef client fill:#E3F2FD,stroke:#1976D2,stroke-width:2px;
    classDef service fill:#E8F5E9,stroke:#388E3C,stroke-width:2px;
    classDef data fill:#FFF3E0,stroke:#EF6C00,stroke-width:2px;
    classDef async fill:#F3E5F5,stroke:#7B1FA2,stroke-width:2px;
    classDef hot fill:#FFEBEE,stroke:#C62828,stroke-width:3px;
    class U client;
    class G,F,Upload service;
    class C,Graph,Posts,Object,CDN,Index data;
    class Events,Fanout async;
    class Hot hot;
```

### 🎬 Feed request sequence

```mermaid
sequenceDiagram
    autonumber
    actor User
    participant API as 🚪 Feed API
    participant Cache as 🟠 Feed Cache
    participant Graph as 🟠 Social Graph
    participant Index as 🔴 Celebrity Index
    participant Rank as 🟢 Ranking Service

    User->>API: Open home feed
    API->>Cache: Read precomputed feed
    API->>Graph: Read followed accounts
    API->>Index: Read recent celebrity posts
    Cache-->>API: Normal-user candidates
    Graph-->>API: Relationship candidates
    Index-->>API: Celebrity candidates
    API->>Rank: Merge, deduplicate and rank
    Rank-->>User: Cursor-paginated feed
```

### 🌪️ Celebrity post protection

```mermaid
flowchart TD
    A[🔴 Celebrity publishes] --> B[(🟠 Post Store)]
    B --> C[[🟣 Event Stream]]
    C --> D[🟣 Celebrity Index]
    D --> E[(🟠 Replicated Cache)]
    E --> F[🌍 CDN / Edge]
    F --> G[👥 Millions of readers]
    G --> H[[🟣 Async likes/comments]]
    H --> I[(🟠 Sharded Counters)]

    classDef hot fill:#FFEBEE,stroke:#C62828,stroke-width:3px;
    classDef async fill:#F3E5F5,stroke:#7B1FA2,stroke-width:2px;
    classDef data fill:#FFF3E0,stroke:#EF6C00,stroke-width:2px;
    class A,G hot;
    class C,D,H async;
    class B,E,F,I data;
```

**Fun explanation:** Do not hand-deliver the same post to 50 million inboxes. Store it once, cache it close to readers, and merge it into feeds when needed. Engagement writes are distributed across shards so one counter row does not become a bottleneck.

## 👑 Celebrity Problem and Consistent Hashing

- Use fan-out-on-write for normal accounts.
- Use fan-out-on-read for celebrities and very large creators.
- Replicate hot post metadata across cache nodes.
- Split counters into shards using `hash(user_id) % N`.
- Use consistent hashing to distribute keys while minimizing remapping during node changes.
- Remember: consistent hashing distributes keys, but replication and key sharding are still required for a single hot key.

## 🗂️ Data Model
`User(user_id, profile)`; `Follow(follower_id, followee_id, created_at)`; `Post(post_id, author_id, media_ref, visibility, created_at)`; `FeedEntry(user_id, post_id, score, created_at)`; `Like(user_id, post_id)`; `Comment(comment_id, post_id, user_id, body)`.

For celebrity support, add or maintain:
- `CelebrityProfile(user_id, follower_count, fanout_mode, updated_at)`
- `CelebrityPostIndex(author_id, post_id, created_at, ranking_score)`
- `CounterShard(entity_id, shard_id, like_count, comment_count)`

## 🔌 API Endpoints
`GET /feed?cursor=`, `POST /posts`, `GET /posts/{id}`, `POST /posts/{id}/likes`, `POST /posts/{id}/comments`, `POST /users/{id}/follow`, `POST /media/upload-session`.

## 🚀 Performance and Caching
Cache feed pages, profiles and popular posts. Use cursor pagination rather than deep offsets. Cache immutable media through a CDN. Batch fan-out and protect against celebrity hot spots. Use request coalescing and sharded counters for viral posts.

## 📈 Scaling
Scale feed readers, ranking workers and media delivery separately. Partition posts by author or time. Use queues for fan-out, notifications and analytics. Horizontal scaling is the primary approach; vertical scaling helps individual database nodes but does not remove single-node limits.

## 🔐 Security
Enforce privacy on every read, protect media with signed URLs, use OAuth2/OIDC, rate limit interactions, detect abuse and encrypt sensitive data. Do not expose private post metadata through caches.

## 🔭 Monitoring
Track feed latency, cache hit rate, fan-out queue lag, ranking errors, media upload success, CDN hit ratio, notification delay and abuse detection outcomes. Add celebrity-specific metrics such as hot-key rate, celebrity-post read QPS, fan-out suppression rate, counter-shard skew and queue backlog.

## 🎤 Interview Follow-ups
- Fan-out on write vs read? Use a hybrid model because celebrity accounts create huge write amplification.
- How do you prevent duplicate likes? Unique constraint on `(user_id, post_id)`.
- How do you handle a viral post? CDN, cache replication, request coalescing and asynchronous counters.
- How do you isolate celebrity traffic? Dedicated read paths, cache namespaces, separate queues and autoscaling policies.
- What happens if ranking is unavailable? Return chronologically ordered candidates from cached and recent sources as a graceful fallback.
