# 88. Largest Rectangle in Histogram

**Pattern:** Monotonic Stack

## C# Solution
```csharp
public class Solution
{
    public int LargestRectangleArea(int[] heights)
    {
        var stack = new Stack<int>();
        int best = 0;
        for (int i = 0; i <= heights.Length; i++)
        {
            int current = i == heights.Length ? 0 : heights[i];
            while (stack.Count > 0 && current < heights[stack.Peek()])
            {
                int height = heights[stack.Pop()];
                int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                best = Math.Max(best, height * width);
            }
            stack.Push(i);
        }
        return best;
    }
}
```

**Complexity:** `O(n)` time and `O(n)` space.
