# 89. Number of Ways to Arrive at Destination

**Pattern:** Dijkstra + Counting Paths

## Approach
Run Dijkstra while maintaining both the shortest distance and the number of ways to reach each node using that shortest distance.

## C# Solution
```csharp
public class Solution
{
    public int CountPaths(int n, int[][] roads)
    {
        const long Mod = 1_000_000_007;
        var graph = new List<(int to, int weight)>[n];
        for (int i = 0; i < n; i++) graph[i] = new();
        foreach (var r in roads)
        {
            graph[r[0]].Add((r[1], r[2]));
            graph[r[1]].Add((r[0], r[2]));
        }

        var dist = Enumerable.Repeat(long.MaxValue, n).ToArray();
        var ways = new long[n];
        var pq = new PriorityQueue<int, long>();
        dist[0] = 0; ways[0] = 1; pq.Enqueue(0, 0);

        while (pq.Count > 0)
        {
            int node = pq.Dequeue();
            long current = dist[node];
            foreach (var edge in graph[node])
            {
                long next = current + edge.weight;
                if (next < dist[edge.to])
                {
                    dist[edge.to] = next; ways[edge.to] = ways[node];
                    pq.Enqueue(edge.to, next);
                }
                else if (next == dist[edge.to])
                    ways[edge.to] = (ways[edge.to] + ways[node]) % Mod;
            }
        }
        return (int)ways[n - 1];
    }
}
```

**Complexity:** `O((n + e) log n)` time and `O(n + e)` space.
