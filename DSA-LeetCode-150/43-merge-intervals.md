# 43. Merge Intervals

**Problem:** Merge all overlapping intervals.

## Approach
Sort intervals by start time. Keep a current interval and extend its end whenever the next interval overlaps; otherwise, store the current interval and start a new one.

## C# Solution
```csharp
public class Solution
{
    public int[][] Merge(int[][] intervals)
    {
        if (intervals.Length <= 1) return intervals;

        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        var result = new List<int[]>();
        var currentStart = intervals[0][0];
        var currentEnd = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i][0] <= currentEnd)
            {
                currentEnd = Math.Max(currentEnd, intervals[i][1]);
            }
            else
            {
                result.Add(new[] { currentStart, currentEnd });
                currentStart = intervals[i][0];
                currentEnd = intervals[i][1];
            }
        }

        result.Add(new[] { currentStart, currentEnd });
        return result.ToArray();
    }
}
```

**Complexity:** `O(n log n)` time for sorting and `O(n)` auxiliary space for the result.

**Follow-ups:** Insert an interval, meeting-room scheduling, and interval intersection.
