# 73. K Closest Points to Origin

**Pattern:** Heap / Quickselect

## C# Solution
```csharp
public class Solution
{
    public int[][] KClosest(int[][] points, int k)
    {
        Array.Sort(points, (a, b) => Distance(a).CompareTo(Distance(b)));
        return points.Take(k).ToArray();
    }

    private static long Distance(int[] p) => (long)p[0] * p[0] + (long)p[1] * p[1];
}
```

## Complexity
- Time: O(n log n).
- Space: O(log n) auxiliary sorting stack, excluding output.
