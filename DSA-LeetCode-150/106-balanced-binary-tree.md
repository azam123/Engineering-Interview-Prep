# 106. Balanced Binary Tree

## Problem
Determine whether the height difference between the left and right subtree of every node is at most one.

## C# Solution
~~~csharp
public class Solution
{
    public bool IsBalanced(TreeNode root)
        => Height(root) != -1;

    private int Height(TreeNode node)
    {
        if (node == null) return 0;

        int left = Height(node.left);
        if (left == -1) return -1;

        int right = Height(node.right);
        if (right == -1) return -1;

        if (Math.Abs(left - right) > 1) return -1;

        return 1 + Math.Max(left, right);
    }
}
~~~

## Complexity
Time O(n), space O(h).

## Interview Insight
Returning -1 as a sentinel lets one DFS detect imbalance and stop propagating useful height information.
