# 97. Evaluate Reverse Polish Notation

**Pattern:** Stack

## C# Solution
```csharp
public class Solution
{
    public int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>();
        foreach (string token in tokens)
        {
            if (int.TryParse(token, out int value)) stack.Push(value);
            else
            {
                int b = stack.Pop(), a = stack.Pop();
                stack.Push(token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => throw new InvalidOperationException()
                });
            }
        }
        return stack.Pop();
    }
}
```

**Complexity:** O(n) time and O(n) space.
