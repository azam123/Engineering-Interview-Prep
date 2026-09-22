# 105. Diameter of Binary Tree

## Problem
Find the longest path between any two nodes. The answer is measured in edges.

## Key Insight
At each node, the best path passing through that node is left height plus right height. Compute height once while updating the global maximum.

## C# Solution
~~~csharp
public class Solution
{
    private int diameter;

    public int DiameterOfBinaryTree(TreeNode root)
    {
        diameter = 0;
        Height(root);
        return diameter;
    }

    private int Height(TreeNode node)
    {
        if (node == null) return 0;

        int left = Height(node.left);
        int right = Height(node.right);

        diameter = Math.Max(diameter, left + right);
        return 1 + Math.Max(left, right);
    }
}
~~~

## Complexity
Time O(n), space O(h).

## Interview Follow-ups
- Why can repeated height calculations become O(n²)?
- Can the global state be avoided?
- How would this change for weighted edges?
