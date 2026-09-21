# 57. Number of Connected Components in an Undirected Graph

**Pattern:** Graph / DFS / Union-Find

## Approach
Build an adjacency list and run DFS from every unvisited vertex. Each new DFS represents one connected component.

```csharp
public int CountComponents(int n, int[][] edges)
{
    var graph = Enumerable.Range(0, n)
        .ToDictionary(i => i, _ => new List<int>());

    foreach (var edge in edges)
    {
        graph[edge[0]].Add(edge[1]);
        graph[edge[1]].Add(edge[0]);
    }

    var visited = new bool[n];
    int count = 0;

    void Dfs(int node)
    {
        visited[node] = true;
        foreach (var next in graph[node])
            if (!visited[next]) Dfs(next);
    }

    for (int i = 0; i < n; i++)
    {
        if (visited[i]) continue;
        count++;
        Dfs(i);
    }

    return count;
}
```

**Complexity:** O(V + E) time and O(V + E) space.

**Follow-ups:** Implement using Union-Find. How would you process edges arriving as a stream?
