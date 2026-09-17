# GenAI and RAG Interview Q&A

## What is RAG?
Retrieval-Augmented Generation retrieves relevant external context and provides it to an LLM so answers can be grounded in enterprise data.

```text
Documents → Parse → Chunk → Embed → Vector/Hybrid Index
                                      ↓
Question → Query Rewrite → Retrieve → Rerank → Prompt → LLM → Citations
```

## Key questions
- **Chunking:** Balance semantic completeness, retrieval precision, and token cost. Prefer structure-aware chunking with metadata.
- **Embeddings:** Dense vectors representing semantic meaning; evaluate model fit and multilingual/domain performance.
- **Hybrid search:** Combines lexical and vector retrieval to handle exact terms and semantic intent.
- **Reranking:** Reorders retrieved candidates using a stronger relevance model.
- **Hallucination reduction:** Ground answers in retrieved evidence, require citations, constrain prompts, and evaluate factuality.
- **Prompt injection:** Treat retrieved content as untrusted data; isolate instructions, filter content, enforce authorization, and avoid tool execution based solely on retrieved text.
- **Multi-tenancy:** Apply tenant filters before or during retrieval and enforce authorization independently of the LLM.
- **Evaluation:** Measure retrieval recall/precision, groundedness, answer relevance, latency, cost, and safety.

## Agentic RAG
An agent may plan, retrieve iteratively, call tools, and verify results. Add bounded steps, permission checks, timeouts, audit logs, and human approval for high-impact actions.

## Token optimization
Use query rewriting, metadata filtering, top-k tuning, deduplication, compression, summarization, and prompt caching. Measure quality before reducing context.

## References
- [Azure AI Search](https://learn.microsoft.com/en-us/azure/search/)
- [Azure OpenAI](https://learn.microsoft.com/en-us/azure/ai-services/openai/)
- [Azure architecture for RAG](https://learn.microsoft.com/en-us/azure/architecture/ai-ml/guide/rag/)
