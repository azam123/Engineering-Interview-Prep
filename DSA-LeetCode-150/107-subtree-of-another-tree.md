# 107. Subtree of Another Tree

## Problem
Determine whether one binary tree occurs as a subtree of another tree.

## C# Solution
~~~csharp
public class Solution
{
    public bool IsSubtree(TreeNode root, TreeNode subRoot)
    {
        if (subRoot == null) return true;
        if (root == null) return false;

        return SameTree(root, subRoot) ||
               IsSubtree(root.left, subRoot) ||
               IsSubtree(root.right, subRoot);
    }

    private bool SameTree(TreeNode a, TreeNode b)
    {
        if (a == null || b == null) return a == b;

        return a.val == b.val &&
               SameTree(a.left, b.left) &&
               SameTree(a.right, b.right);
    }
}
~~~

## Complexity
Straightforward approach: O(n × m) worst case.

## Interview Follow-ups
- How could serialization plus string matching improve performance?
- What if node values are not unique?
