# Azure Data Factory Interview Q&A

## Core concepts
- **Pipeline:** logical workflow.
- **Activity:** unit of work such as Copy, Lookup, ForEach, or Data Flow.
- **Dataset:** data structure or location reference.
- **Linked service:** connection information for a data store or compute service.
- **Integration Runtime:** compute and network bridge for data movement and activities.
- **Trigger:** starts a pipeline on schedule, event, or tumbling window.

```text
Trigger → Pipeline → Lookup/Metadata → ForEach → Copy Activity → Validation → Monitoring
```

## Interview questions
**How do you implement incremental loading?** Store a watermark such as the last modified timestamp or ID, read the previous watermark, filter source data, load the target, and update the watermark only after successful processing.

**How do you secure ADF?** Use managed identities, Key Vault references, private endpoints, least privilege, secure input/output settings, and network controls.

**When do you use Mapping Data Flow?** When transformations require managed visual Spark-based processing. For simple movement or SQL-native transformations, Copy Activity or database-side processing may be more efficient.

**How do you improve performance?** Partition data, tune DIUs and parallel copy settings, avoid unnecessary transformations, use staged copy when appropriate, and monitor source/target bottlenecks.

**What is CI/CD?** Keep pipelines and linked configurations in source control, parameterize environment-specific values, validate changes, and deploy through automated release stages.

## References
- [ADF overview](https://learn.microsoft.com/en-us/azure/data-factory/introduction)
- [Pipelines and activities](https://learn.microsoft.com/en-us/azure/data-factory/concepts-pipelines-activities)
- [Transform data](https://learn.microsoft.com/en-us/azure/data-factory/transform-data)
