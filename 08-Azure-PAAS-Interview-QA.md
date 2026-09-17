# Azure PaaS Interview Q&A

## Services to know
- App Service: managed web hosting.
- Azure Functions: event-driven serverless compute.
- Container Apps: managed container platform with scaling and revisions.
- AKS: managed Kubernetes control plane.
- API Management: API gateway, policies, security, and developer portal.
- Service Bus: reliable enterprise messaging.
- Event Grid: event routing and reactive integration.
- Cosmos DB: globally distributed NoSQL database.
- Key Vault: secrets, keys, and certificates.
- Managed Identity: passwordless access to Azure resources.

## Architecture questions
**How do you secure an API?** Use Entra ID/OAuth2, APIM policies, managed identities, private endpoints where appropriate, Key Vault, input validation, rate limiting, and centralized monitoring.

**How do you design for resilience?** Use availability zones where supported, retries with backoff, circuit breakers, queues, idempotent consumers, health checks, backups, and disaster recovery runbooks.

**How do you observe production?** Combine Application Insights/Azure Monitor, structured logs, metrics, distributed tracing, correlation IDs, dashboards, and actionable alerts.

## References
- [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [Azure App Service](https://learn.microsoft.com/en-us/azure/app-service/)
- [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [Azure Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/)
