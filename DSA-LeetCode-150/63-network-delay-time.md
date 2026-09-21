# 63. Network Delay Time

**Pattern:** Dijkstra's Algorithm

## Approach
Build an adjacency list and use a min-priority queue to always process the node with the smallest known distance.

## C# Solution
```csharp
public class Solution
{
    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        var graph = new List<(int To, int Weight)>[n + 1];
        for (int i = 1; i <= n; i++) graph[i] = new();
        foreach (var edge in times) graph[edge[0]].Add((edge[1], edge[2]));

        var dist = Enumerable.Repeat(int.MaxValue, n + 1).ToArray();
        var pq = new PriorityQueue<int, int>();
        dist[k] = 0;
        pq.Enqueue(k, 0);

        while (pq.Count > 0)
        {
            int node = pq.Dequeue();
            foreach (var (to, weight) in graph[node])
            {
                int next = dist[node] + weight;
                if (next < dist[to])
                {
                    dist[to] = next;
                    pq.Enqueue(to, next);
                }
            }
        }

        int answer = dist.Skip(1).Max();
        return answer == int.MaxValue ? -1 : answer;
    }
}
```

**Complexity:** O((V + E) log V) time and O(V + E) space.
