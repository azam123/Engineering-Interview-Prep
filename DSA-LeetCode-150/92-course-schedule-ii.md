# 92. Course Schedule II

**Pattern:** Graph, Topological Sort

## Approach
Build a directed graph and use Kahn's algorithm. Process nodes with indegree zero; if all courses are processed, the ordering is valid.

## C# Solution
```csharp
public class Solution
{
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var graph = new List<int>[numCourses];
        var indegree = new int[numCourses];
        for (int i = 0; i < numCourses; i++) graph[i] = new List<int>();

        foreach (var p in prerequisites)
        {
            graph[p[1]].Add(p[0]);
            indegree[p[0]]++;
        }

        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
            if (indegree[i] == 0) queue.Enqueue(i);

        var order = new int[numCourses];
        int index = 0;
        while (queue.Count > 0)
        {
            int course = queue.Dequeue();
            order[index++] = course;
            foreach (int next in graph[course])
                if (--indegree[next] == 0) queue.Enqueue(next);
        }

        return index == numCourses ? order : Array.Empty<int>();
    }
}
```

**Complexity:** O(V + E) time and space.
