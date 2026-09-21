# 76. Reconstruct Itinerary

**Pattern:** Eulerian Path + DFS

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

        var result = new List<string>();
        void Visit(string airport)
        {
            if (graph.ContainsKey(airport))
                while (graph[airport].Count > 0) Visit(graph[airport].Dequeue());
            result.Add(airport);
        }

        Visit("JFK");
        result.Reverse();
        return result;
    }
}
```

## Complexity
- Time: O(E log E).
- Space: O(E).
