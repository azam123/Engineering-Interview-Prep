# 104. Maximum Depth of Binary Tree

## Problem
Return the maximum number of nodes on any root-to-leaf path.

## C# Solution
~~~csharp
public class Solution
{
    public int MaxDepth(TreeNode root)
    {
        if (root == null) return 0;
        return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
    }
}
~~~

## Complexity
Time O(n), space O(h).

## Alternative
BFS can calculate depth level-by-level and avoids recursive stack growth.

## Interview Follow-ups
- What happens for a highly skewed tree?
- How can you implement this iteratively?
- How is tree depth related to height?
