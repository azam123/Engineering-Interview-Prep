# 96. Implement Queue using Stacks

**Pattern:** Stack, Amortized Analysis

## C# Solution
```csharp
public class MyQueue
{
    private readonly Stack<int> input = new();
    private readonly Stack<int> output = new();

    public void Push(int x) => input.Push(x);

    public int Pop() { Move(); return output.Pop(); }
    public int Peek() { Move(); return output.Peek(); }
    public bool Empty() => input.Count == 0 && output.Count == 0;

    private void Move()
    {
        if (output.Count > 0) return;
        while (input.Count > 0) output.Push(input.Pop());
    }
}
```

**Complexity:** O(1) amortized per operation, O(n) space.
