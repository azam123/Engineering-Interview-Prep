# 103. Invert Binary Tree

## Problem
Invert a binary tree by swapping the left and right child of every node.

## C# Solution
~~~csharp
public class Solution
{
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null) return null;

        (root.left, root.right) = (root.right, root.left);
        InvertTree(root.left);
        InvertTree(root.right);
        return root;
    }
}
~~~

## Complexity
Time O(n), space O(h).

## Interview Follow-ups
- Write an iterative BFS version.
- What is the worst-case recursion depth?
- How would you avoid stack overflow for a skewed tree?
