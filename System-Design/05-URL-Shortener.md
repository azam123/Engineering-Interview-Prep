# URL Shortener System Design

## Requirement Gathering
Create a service that converts long URLs into short URLs and redirects users when the short URL is opened.

## Functional Requirements
- Create a short URL.
- Redirect short URL to the original URL.
- Support custom aliases optionally.
- Set expiration time optionally.
- Show basic click analytics.
- Prevent malicious or invalid URLs.

## Non-Functional Requirements
- Very fast redirects.
- High availability.
- Short codes must be unique.
- Read traffic is much higher than creation traffic.
- Prevent abuse and excessive scanning.

## High Level System Design
Client → Load Balancer → URL API → Cache → URL Database. Creation service generates a unique ID and encodes it using Base62. Redirect service reads from cache first and then database. Analytics events are sent asynchronously to Kafka.

## Capacity Estimation
Assume 100 million new URLs/month and 1 billion redirects/month. Redirects are read-heavy and should be served mostly from Redis and edge caching.

## Data Estimation
If each URL record averages 500 bytes, 100 million URLs need about 50 GB before indexes and replication. Storage grows steadily, so expiration and archival may be needed.

## Network Estimation
A redirect response is small, but high request volume matters. Keep redirect services close to users and avoid synchronous analytics processing in the redirect path.

## Data Model
- UrlMapping(short_code, long_url, owner_id, created_at, expires_at, status)
- ClickEvent(event_id, short_code, timestamp, country, device)

## API Endpoints
- `POST /urls` — create short URL
- `GET /{shortCode}` — redirect
- `GET /urls/{shortCode}/stats` — analytics
- `DELETE /urls/{shortCode}` — disable URL

## Performance and Caching
Cache short-code to long-URL mappings in Redis. Use TTL based on expiration. Negative-cache invalid codes briefly to reduce database load. Use HTTP redirect caching carefully because URLs can be revoked.

## Scaling: Vertical vs Horizontal
Start with a simple service and database. Scale redirect APIs horizontally. Use database read replicas, partitioning, and multi-region replicas as traffic grows. A distributed ID generator avoids collisions across regions.

## Security, Authentication and Authorization
Validate URL schemes, block malware and phishing domains, require authentication for management APIs, rate-limit creation, and protect analytics data. Do not allow open redirect abuse where possible.

## Monitoring/Observability
Monitor redirect latency, cache hit ratio, 404 rate, database latency, creation failures, abuse reports, traffic by code, and regional error rates.

## Interview Summary
Base62 encoding alone is not encryption. The key decisions are unique ID generation, cache-first redirects, expiration, abuse prevention, and asynchronous analytics.
