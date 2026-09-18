# WhatsApp System Design

## Requirement Gathering
Build a chat system for one-to-one and group messaging, with delivery status, online presence, media sharing, and notifications.

## Functional Requirements
- Register users and manage contacts.
- Send and receive text messages.
- Support one-to-one and group chats.
- Show sent, delivered, and read states.
- Support offline users and push notifications.
- Send images, videos, documents, and voice notes.

## Non-Functional Requirements
- Low message latency.
- Messages must not be lost.
- High availability and privacy.
- Ordered messages within a conversation.
- Support millions of concurrent connections.

## High Level System Design
Mobile Client → Load Balancer → WebSocket Gateway → Message Service → Message Store. Presence service tracks connections. Kafka or another durable log distributes events. Push Notification service wakes offline clients. Media uses object storage and CDN.

## Capacity Estimation
Assume 500 million daily active users and 100 messages per active user per day. That equals 50 billion messages/day, or roughly 580,000 messages/second average; design for much higher peak traffic.

## Data Estimation
If a text message averages 1 KB including metadata, 50 billion messages require about 50 TB/day before replication and indexes. Use partitioned distributed storage and retention policies.

## Network Estimation
Text messages are small, but concurrent WebSocket connections are large. Use connection-aware load balancing and horizontally scaled gateway servers. Media should bypass message servers through object storage URLs.

## Data Model
- User(user_id, phone, profile)
- Conversation(conversation_id, type)
- Member(conversation_id, user_id, role)
- Message(message_id, conversation_id, sender_id, sequence, body, created_at)
- MessageStatus(message_id, user_id, status, timestamp)
- Device(user_id, device_id, push_token)

## API Endpoints
- `POST /conversations`
- `GET /conversations`
- `POST /messages`
- `GET /conversations/{id}/messages?cursor=`
- `POST /messages/{id}/read`
- `POST /media/upload-url`
- `GET /presence/{userId}`

## Performance and Caching
Maintain WebSocket connections for real-time delivery. Cache recent conversations and presence in Redis with short TTLs. Use cursor pagination. Use an outbox/event pattern so database writes and message events are reliable.

## Scaling: Vertical vs Horizontal
Vertical scaling helps individual nodes but has limits. Horizontally scale WebSocket gateways, message processors, and consumers. Partition messages by conversation ID. Use consistent routing or a shared event bus for connected devices.

## Security, Authentication and Authorization
Use phone verification or another strong identity mechanism. Encrypt messages in transit and consider end-to-end encryption. Verify conversation membership, protect media URLs, rate-limit abuse, and minimize stored metadata.

## Monitoring/Observability
Monitor active connections, message delivery latency, failed deliveries, reconnect rate, queue lag, database write latency, push notification success, and message loss indicators.

## Interview Summary
The important challenges are connection management, offline delivery, ordering, retries, duplicate messages, and multi-device synchronization. Use message IDs and idempotent consumers to handle retries safely.
