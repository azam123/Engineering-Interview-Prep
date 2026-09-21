# 61. K Closest Points to Origin

**Pattern:** Max Heap

## Approach
Keep a max heap of size `k`. If a new point is closer than the farthest point currently stored, replace the farthest point.

## C# Solution
```csharp
public class Solution
{
    public int[][] KClosest(int[][] points, int k)
    {
        var heap = new PriorityQueue<int[], int>();
        foreach (var point in points)
        {
            int distance = point[0] * point[0] + point[1] * point[1];
            heap.Enqueue(point, -distance);
            if (heap.Count > k) heap.Dequeue();
        }

        var result = new int[k][];
        for (int i = 0; i < k; i++) result[i] = heap.Dequeue();
        return result;
    }
}
```

**Complexity:** O(n log k) time and O(k) space.
