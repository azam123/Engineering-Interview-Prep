# E-Commerce System Design (Amazon-like)

## Requirement Gathering
Build an online shopping platform where users browse products, add items to a cart, place orders, pay, and track delivery.

## Functional Requirements
- Register/login users.
- Search and filter products.
- View product details and stock.
- Add/remove cart items.
- Create orders and make payments.
- Track order status and receive notifications.
- Admin can manage products, prices, and inventory.

## Non-Functional Requirements
- High availability and reliability.
- Fast product search and checkout.
- Secure payments and personal data.
- Support large traffic spikes.
- No duplicate orders or payments.

## High Level System Design
Client → API Gateway → Auth, Catalog, Search, Cart, Order, Payment, Inventory, Delivery and Notification services.
- PostgreSQL/MySQL: orders, payments, users.
- Elasticsearch/OpenSearch: product search.
- Redis: cart, sessions, hot products.
- Kafka: order and inventory events.
- Object storage + CDN: product images.

## Capacity Estimation
Assume 10 million users, 1 million daily active users, 100,000 orders/day, and 10 product views per active user. Read traffic is much higher than write traffic, so cache product data and use read replicas.

## Data Estimation
If each order record averages 5 KB, 100,000 orders/day needs about 500 MB/day before indexes, logs, and replicas. Product images should be stored in object storage, not the relational database.

## Network Estimation
Assume 100,000 orders/day: average order requests are low, but peak traffic may be 10–20 times higher. Product images and videos should be served through a CDN to reduce application bandwidth.

## Data Model
- User(user_id, name, email, password_hash)
- Product(product_id, title, price, category_id)
- Inventory(product_id, quantity, reserved_quantity)
- Cart(cart_id, user_id)
- Order(order_id, user_id, status, total, created_at)
- OrderItem(order_id, product_id, quantity, price)
- Payment(payment_id, order_id, status, provider_reference)

## API Endpoints
- `GET /products?query=`
- `GET /products/{id}`
- `POST /cart/items`
- `POST /orders`
- `POST /payments`
- `GET /orders/{id}`
- `PATCH /inventory/{productId}`

## Performance and Caching
Cache product details, categories, and popular searches in Redis. Use CDN caching for images. Use database indexes and read replicas. Use idempotency keys for checkout and payment requests.

## Scaling: Vertical vs Horizontal
Vertical scaling increases CPU/RAM on one server and is simple but limited. Horizontal scaling adds more servers and is preferred for stateless APIs. Partition orders by user or time when data becomes very large.

## Security, Authentication and Authorization
Use OAuth2/OIDC or secure sessions. Hash passwords with a strong password-hashing algorithm. Encrypt traffic with TLS. Never store raw card details. Apply role-based access control, rate limiting, input validation, and audit logging.

## Monitoring/Observability
Monitor latency, error rate, checkout success, payment failures, inventory mismatches, CPU, memory, database connections, Kafka lag, and cache hit ratio. Use centralized logs, metrics, distributed tracing, and alerts.

## Interview Summary
The hardest parts are inventory consistency, payment retries, order state transitions, and handling traffic spikes during sales. Use events for asynchronous work and a saga-style workflow for order processing.
