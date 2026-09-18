# YouTube System Design

## Requirement Gathering
Build a video platform where users upload, watch, search, like, comment, and share videos.

## Functional Requirements
- Upload videos and thumbnails.
- Transcode videos into multiple resolutions.
- Stream videos with pause, resume, and adaptive quality.
- Search videos and channels.
- Like, comment, subscribe, and view history.
- Recommend relevant videos.

## Non-Functional Requirements
- High availability.
- Low startup delay and smooth playback.
- Support large video files and global traffic.
- Durable storage and secure content access.

## High Level System Design
Client → API Gateway → Auth, Video Metadata, Upload, Transcoding, Search, Recommendation and Engagement services.
Upload service stores original files in object storage and publishes a job to a queue. Workers create HLS/DASH segments and multiple resolutions. CDN serves video segments globally.

## Capacity Estimation
Assume 100 million daily viewers and 1 million daily uploads. Most traffic is video delivery, not metadata APIs. CDN offload is essential.

## Data Estimation
If one uploaded original averages 500 MB, 1 million uploads/day would be about 500 TB/day before transcoded copies. Use lifecycle policies, compression, tiered storage, and retention rules.

## Network Estimation
For 1 million concurrent viewers at an average 2 Mbps stream, egress is approximately 2 Tbps. The application should not stream directly from API servers; CDN edge nodes should deliver segments.

## Data Model
- User(user_id, name)
- Video(video_id, owner_id, title, status, visibility)
- VideoAsset(video_id, resolution, codec, storage_url)
- Channel(channel_id, owner_id)
- Comment(comment_id, video_id, user_id, text)
- VideoLike(video_id, user_id)
- Subscription(user_id, channel_id)

## API Endpoints
- `POST /videos/upload-url`
- `POST /videos/{id}/complete`
- `GET /videos/{id}`
- `GET /videos/{id}/manifest`
- `GET /search?q=`
- `POST /videos/{id}/comments`
- `POST /videos/{id}/like`

## Performance and Caching
Use CDN caching for immutable video segments and thumbnails. Cache video metadata and popular searches. Use asynchronous queues for transcoding, notifications, analytics, and recommendations.

## Scaling: Vertical vs Horizontal
Transcoding workers scale horizontally based on queue length. API services are stateless and scale horizontally. Storage and CDN scale independently. Use partitioning for engagement events and time-series analytics.

## Security, Authentication and Authorization
Use signed upload URLs, virus scanning, content moderation, access controls, and rate limits. Check video visibility before issuing private playback URLs. Encrypt data in transit and at rest.

## Monitoring/Observability
Track upload success, transcoding delay, queue depth, playback startup time, rebuffering rate, CDN hit ratio, bitrate errors, API latency, and storage costs.

## Interview Summary
Separate control plane (metadata and permissions) from data plane (large video delivery). Object storage, asynchronous transcoding, and CDN delivery are the central design choices.
