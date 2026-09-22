# 102. Validate Binary Search Tree

## Problem
Determine whether a binary tree is a valid BST.

## Key Insight
Each node must satisfy the complete range inherited from its ancestors, not just its parent's value.

## C# Solution
~~~csharp
public class Solution
{
    public bool IsValidBST(TreeNode root)
        => Validate(root, long.MinValue, long.MaxValue);

    private bool Validate(TreeNode node, long min, long max)
    {
        if (node == null) return true;
        if (node.val <= min || node.val >= max) return false;

        return Validate(node.left, min, node.val) &&
               Validate(node.right, node.val, max);
    }
}
~~~

## Complexity
Time O(n), space O(h).

## Interview Follow-ups
- How would you implement this iteratively?
- What changes if duplicate values are allowed?
- Why is checking only immediate children incorrect?
