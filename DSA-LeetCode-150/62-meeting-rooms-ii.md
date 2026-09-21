# 62. Meeting Rooms II

**Pattern:** Sorting + Min Heap

## Approach
Sort intervals by start time. Reuse the room whose meeting ends earliest when possible; otherwise allocate a new room.

## C# Solution
```csharp
public class Solution
{
    public int MinMeetingRooms(int[][] intervals)
    {
        if (intervals.Length == 0) return 0;
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        var ends = new PriorityQueue<int, int>();

        foreach (var interval in intervals)
        {
            if (ends.Count > 0 && ends.Peek() <= interval[0]) ends.Dequeue();
            ends.Enqueue(interval[1], interval[1]);
        }
        return ends.Count;
    }
}
```

**Complexity:** O(n log n) time and O(n) space.
