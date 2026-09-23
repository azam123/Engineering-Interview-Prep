# Production RAG: Grounding, Security & Evaluation — Architect Q&A

## Scenario
An enterprise RAG assistant answers correctly in demos but occasionally retrieves documents from the wrong business unit. What is the architectural problem?

### Answer
Treat retrieval authorization as a first-class security boundary. Document metadata must carry tenant/business-unit permissions, and retrieval must enforce those filters before context reaches the model. Do not rely on the LLM to decide whether a user is authorized to see a document.

## Secure RAG Flow
```mermaid
flowchart LR
    U[User] --> A[API + Identity]
    A --> F[Authorization Filter]
    F --> E[Embedding Query]
    E --> V[Vector Search + Metadata Filter]
    V --> R[Reranker]
    R --> C[Grounded Context]
    C --> L[LLM]
    L --> G[Answer + Citations]
```

## Interview Answer: How do you measure RAG quality?
Use separate retrieval and generation metrics. Retrieval can be evaluated with recall@k, precision@k, MRR, or nDCG against a curated evaluation set. Generation should be evaluated for groundedness/faithfulness, answer relevance, citation correctness, and refusal behavior. Include adversarial authorization tests.

## Practical Metadata Model
```python
chunk = {
    "document_id": "policy-123",
    "tenant_id": "tenant-a",
    "allowed_groups": ["finance-readers"],
    "source": "sharepoint",
    "chunk_text": "..."
}

# Authorization is applied before the LLM receives context.
filters = {
    "tenant_id": current_tenant,
    "allowed_groups": {"$in": user_groups}
}
```

## Principal-Level Follow-ups
- How do you detect retrieval drift after an index refresh?
- What is the difference between RAG evaluation and model evaluation?
- How do you prevent prompt injection from retrieved content?
- When would you use hybrid search instead of vector-only search?

## Official Documentation
- https://learn.microsoft.com/azure/architecture/ai-ml/guide/rag
- https://learn.microsoft.com/azure/ai-services/openai/concepts/evaluations
- https://learn.microsoft.com/azure/search/vector-search-overview
