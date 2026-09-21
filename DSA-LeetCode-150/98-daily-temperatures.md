# 98. Daily Temperatures

**Pattern:** Monotonic Stack

## C# Solution
```csharp
public class Solution
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        int[] result = new int[temperatures.Length];
        var stack = new Stack<int>();

        for (int i = 0; i < temperatures.Length; i++)
        {
            while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()])
            {
                int previous = stack.Pop();
                result[previous] = i - previous;
            }
            stack.Push(i);
        }
        return result;
    }
}
```

**Complexity:** O(n) time and O(n) space.
