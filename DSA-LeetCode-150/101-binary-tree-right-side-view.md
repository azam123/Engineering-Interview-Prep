# 101. Binary Tree Right Side View

## Problem
Given a binary tree, return the values visible from the right side.

## C# Solution
~~~csharp
public class Solution
{
    public IList<int> RightSideView(TreeNode root)
    {
        var result = new List<int>();
        if (root == null) return result;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
                if (i == size - 1) result.Add(node.val);
            }
        }
        return result;
    }
}
~~~

## Complexity
Time O(n), space O(w), where w is maximum tree width.

## Interview Follow-ups
- Can you solve it with DFS?
- Why is the last BFS node of each level visible?
- How would you handle a very deep tree?
