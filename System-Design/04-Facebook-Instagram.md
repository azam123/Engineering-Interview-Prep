# Facebook / Instagram System Design

## Requirement Gathering
Build a social platform where users create posts, upload photos/videos, follow friends, view a feed, like, comment, and receive notifications.

## Functional Requirements
- Create profiles and follow users.
- Publish text, photo, and video posts.
- Generate home feed.
- Like, comment, save, and share posts.
- Search users and hashtags.
- Send notifications.

## Non-Functional Requirements
- High availability.
- Fast feed loading.
- Eventual consistency is acceptable for likes and counters.
- Secure private accounts and media.
- Scale for read-heavy traffic.

## High Level System Design
Client → API Gateway → User, Social Graph, Post, Feed, Engagement, Media, Search, and Notification services. Store media in object storage and deliver it through a CDN. Use Kafka for events. Feed can use fan-out-on-write for normal users and fan-out-on-read for celebrities.

## Capacity Estimation
Assume 300 million daily active users and 10% creating one post/day: 30 million posts/day. Feed reads will be much larger than post writes, so precomputed feeds and caching are useful.

## Data Estimation
If post metadata averages 2 KB, 30 million posts require about 60 GB/day before indexes. Media size is much larger and belongs in object storage with lifecycle policies.

## Network Estimation
Feed responses should contain metadata and optimized image URLs, not full media binaries. CDN delivery handles most image/video bandwidth. Use thumbnails and adaptive image sizes.

## Data Model
- User(user_id, profile, privacy)
- Follow(follower_id, followee_id, created_at)
- Post(post_id, author_id, text, media_url, created_at, visibility)
- Like(post_id, user_id)
- Comment(comment_id, post_id, user_id, text)
- FeedEntry(user_id, post_id, ranking_score)

## API Endpoints
- `POST /posts`
- `GET /feed?cursor=`
- `POST /users/{id}/follow`
- `POST /posts/{id}/like`
- `POST /posts/{id}/comments`
- `GET /users/{id}/posts`
- `GET /search?q=`

## Performance and Caching
Cache popular posts, profiles, and feed pages. Use CDN for media. Precompute feeds for normal accounts. For high-follower accounts, merge recent posts at read time. Use asynchronous counters and ranking jobs.

## Scaling: Vertical vs Horizontal
Scale stateless APIs horizontally. Partition posts by author or post ID. Separate feed generation from feed serving. Use replicas for read-heavy workloads and a search index for discovery.

## Security, Authentication and Authorization
Use OAuth2/OIDC or secure sessions. Check privacy and block lists before returning content. Apply rate limits, content moderation, malware scanning, and signed media URLs.

## Monitoring/Observability
Track feed latency, cache hit rate, post creation errors, media processing time, engagement event lag, notification delays, and moderation queue size.

## Interview Summary
Feed generation is the key design decision. Explain fan-out-on-write, fan-out-on-read, celebrity handling, ranking, privacy checks, and eventual consistency.
