# 44. Course Schedule

**Problem:** Determine whether all courses can be completed given prerequisite pairs.

## Approach
Build a directed graph and calculate indegrees. Use Kahn's topological sort (BFS): start with zero-indegree courses, remove them, and reduce the indegree of their neighbors. If every course is processed, no cycle exists.

## C# Solution
```csharp
public class Solution
{
    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        var graph = new List<int>[numCourses];
        var indegree = new int[numCourses];
        for (int i = 0; i < numCourses; i++) graph[i] = new List<int>();

        foreach (var pair in prerequisites)
        {
            graph[pair[1]].Add(pair[0]);
            indegree[pair[0]]++;
        }

        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
            if (indegree[i] == 0) queue.Enqueue(i);

        int processed = 0;
        while (queue.Count > 0)
        {
            int course = queue.Dequeue();
            processed++;

            foreach (int next in graph[course])
                if (--indegree[next] == 0) queue.Enqueue(next);
        }

        return processed == numCourses;
    }
}
```

**Complexity:** `O(V + E)` time and `O(V + E)` space.

**Follow-ups:** Return a valid course order and detect/report the cycle.
