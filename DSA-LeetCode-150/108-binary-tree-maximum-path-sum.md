# 108. Binary Tree Maximum Path Sum

## Problem
Find the maximum sum of any non-empty path. A path may start and end at any nodes.

## Key Insight
A path through the current node may use both children, but only one child branch can be returned to the parent.

## C# Solution
~~~csharp
public class Solution
{
    private int best = int.MinValue;

    public int MaxPathSum(TreeNode root)
    {
        Gain(root);
        return best;
    }

    private int Gain(TreeNode node)
    {
        if (node == null) return 0;

        int left = Math.Max(0, Gain(node.left));
        int right = Math.Max(0, Gain(node.right));

        best = Math.Max(best, node.val + left + right);
        return node.val + Math.Max(left, right);
    }
}
~~~

## Complexity
Time O(n), space O(h).

## Interview Follow-ups
- Why can only one child branch be returned upward?
- Why discard negative child contributions?
- How would you guard against integer overflow?
