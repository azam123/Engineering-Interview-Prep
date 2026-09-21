# 47. Clone Graph

**Problem:** Return a deep copy of an undirected connected graph.

## Approach
Use BFS and a dictionary mapping each original node to its clone. Create a clone the first time a node is discovered, then connect cloned neighbors while traversing.

## C# Solution
```csharp
public class Node
{
    public int val;
    public IList<Node> neighbors;
    public Node(int value) { val = value; neighbors = new List<Node>(); }
}

public class Solution
{
    public Node CloneGraph(Node node)
    {
        if (node == null) return null;

        var clones = new Dictionary<Node, Node>
        {
            [node] = new Node(node.val)
        };
        var queue = new Queue<Node>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var neighbor in current.neighbors)
            {
                if (!clones.ContainsKey(neighbor))
                {
                    clones[neighbor] = new Node(neighbor.val);
                    queue.Enqueue(neighbor);
                }

                clones[current].neighbors.Add(clones[neighbor]);
            }
        }

        return clones[node];
    }
}
```

**Complexity:** `O(V + E)` time and `O(V)` space.

**Follow-ups:** Clone a directed graph, handle disconnected components, and serialize/deserialize a graph.
