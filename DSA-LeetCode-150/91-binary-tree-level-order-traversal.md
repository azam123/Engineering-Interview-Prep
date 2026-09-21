# Binary Tree Level Order Traversal

**Pattern:** BFS / Queue

## Approach
Process nodes level by level using a queue.

```csharp
public class Solution
{
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var result = new List<IList<int>>();
        if (root == null) return result;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            int count = queue.Count;
            var level = new List<int>();
            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();
                level.Add(node.val);
                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
            result.Add(level);
        }
        return result;
    }
}
```

**Complexity:** O(n) time, O(n) space.
