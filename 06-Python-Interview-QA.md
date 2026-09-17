# Python Interview Q&A

## Core concepts
- Lists are mutable; tuples are immutable.
- A generator yields values lazily and reduces memory usage.
- Decorators wrap functions to add behavior.
- Context managers manage setup and cleanup through `with`.
- Type hints improve readability and tooling but are not runtime enforcement.

```python
def squares(values):
    for value in values:
        yield value * value
```

## Interview questions
1. **List vs tuple?** Lists support mutation; tuples communicate fixed collections and can be hashable when their elements are hashable.
2. **GIL?** In CPython, the Global Interpreter Lock limits simultaneous execution of Python bytecode in threads; I/O concurrency and multiprocessing remain useful.
3. **Async vs threads?** Async is efficient for cooperative I/O concurrency; threads can integrate blocking libraries; CPU-heavy work often benefits from multiprocessing or native extensions.
4. **FastAPI practices?** Use Pydantic validation, dependency injection, async only when appropriate, structured logging, authentication, and automated tests.

## References
- [Python documentation](https://docs.python.org/3/)
- [FastAPI documentation](https://fastapi.tiangolo.com/)
