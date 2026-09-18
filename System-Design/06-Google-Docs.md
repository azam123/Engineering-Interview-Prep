# Google Docs System Design

## Requirement Gathering
Build a collaborative document editor where many users can edit the same document, see changes quickly, comment, and access version history.

## Functional Requirements
- Create, rename, share, and delete documents.
- Edit text collaboratively.
- Show presence and cursors.
- Support comments and permissions.
- Save document changes automatically.
- Provide version history and restore.
- Work with temporary network disconnections.

## Non-Functional Requirements
- Low editing latency.
- No lost updates.
- Correct convergence between clients.
- Fine-grained sharing and access control.
- Durable history and high availability.

## High Level System Design
Client → API Gateway → Auth and Document Service → Collaboration Session Service → Operation Log / Document Store. WebSocket connections broadcast operations to active users. Object storage can hold exports and large attachments. Redis stores presence and short-lived session data.

## Capacity Estimation
Assume 10 million daily active users and 1 million concurrently open documents. If each active editor sends 2 operations/second, the system may receive millions of operations per second during peak usage. Partition sessions by document ID.

## Data Estimation
Store document snapshots periodically and operations separately. A document snapshot may be KBs to MBs, while individual operations are usually small. Compact old operations into snapshots to control storage growth.

## Network Estimation
Use WebSockets for active collaboration. Send small operations instead of the complete document. Compress payloads and reconnect clients using the last acknowledged operation number.

## Data Model
- Document(document_id, owner_id, title, version)
- DocumentMember(document_id, user_id, permission)
- Operation(operation_id, document_id, client_id, sequence, payload, timestamp)
- Snapshot(document_id, version, content, created_at)
- Comment(comment_id, document_id, user_id, anchor, text)
- Presence(document_id, user_id, cursor, last_seen)

## API Endpoints
- `POST /documents`
- `GET /documents/{id}`
- `POST /documents/{id}/share`
- `WS /documents/{id}/collaborate`
- `GET /documents/{id}/versions`
- `POST /documents/{id}/restore`
- `POST /documents/{id}/comments`

## Performance and Caching
Keep active sessions in memory with durable operation logs. Cache document metadata and permissions. Periodically create snapshots. Use batching and backpressure when clients send many operations.

## Scaling: Vertical vs Horizontal
Horizontal scaling is required for collaboration sessions. Route the same document to a session leader or use a distributed coordination layer. Partition by document ID. Replicate durable operations and use failover when a session node fails.

## Security, Authentication and Authorization
Authenticate users with OAuth2/OIDC. Check document permissions for every connection and operation. Encrypt traffic and stored data. Keep audit history, protect sharing links, and prevent unauthorized exports.

## Monitoring/Observability
Monitor operation latency, WebSocket connections, reconnect rate, conflict rate, dropped operations, session failover, storage lag, snapshot duration, and permission-denied events.

## Interview Summary
Explain Operational Transformation (OT) or CRDTs. OT transforms concurrent operations against each other; CRDTs use data structures designed to converge. The design must preserve ordering, acknowledgements, offline edits, permissions, and durable history.
