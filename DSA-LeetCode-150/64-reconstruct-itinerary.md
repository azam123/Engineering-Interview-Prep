# 64. Reconstruct Itinerary

**Pattern:** Eulerian Path + DFS

## Approach
Use lexical ordering and Hierholzer's algorithm. Always consume the smallest available destination and prepend nodes during backtracking.

## C# Solution
```csharp
public class Solution
{
    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        var graph = new Dictionary<string, PriorityQueue<string, string>>();
        foreach (var ticket in tickets)
        {
            if (!graph.ContainsKey(ticket[0])) graph[ticket[0]] = new();
            graph[ticket[0]].Enqueue(ticket[1], ticket[1]);
        }

        var result = new LinkedList<string>();
        void Dfs(string airport)
        {
            if (graph.TryGetValue(airport, out var destinations))
                while (destinations.Count > 0) Dfs(destinations.Dequeue());
            result.AddFirst(airport);
        }

        Dfs("JFK");
        return result.ToList();
    }
}
```

**Complexity:** O(E log E) time and O(E) space.
