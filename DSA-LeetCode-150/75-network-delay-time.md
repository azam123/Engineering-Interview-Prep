# 75. Network Delay Time

**Pattern:** Dijkstra's Algorithm

## C# Solution
```csharp
public class Solution
{
    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        var graph = Enumerable.Range(0, n + 1).Select(_ => new List<(int To, int Weight)>()).ToArray();
        foreach (var t in times) graph[t[0]].Add((t[1], t[2]));
        var dist = Enumerable.Repeat(int.MaxValue, n + 1).ToArray();
        var pq = new PriorityQueue<int, int>();
        dist[k] = 0; pq.Enqueue(k, 0);

        while (pq.Count > 0)
        {
            int node = pq.Dequeue();
            foreach (var edge in graph[node])
            {
                int next = dist[node] + edge.Weight;
                if (next < dist[edge.To])
                {
                    dist[edge.To] = next;
                    pq.Enqueue(edge.To, next);
                }
            }
        }

        int answer = dist.Skip(1).Max();
        return answer == int.MaxValue ? -1 : answer;
    }
}
```

## Complexity
- Time: O((V + E) log V).
- Space: O(V + E).
