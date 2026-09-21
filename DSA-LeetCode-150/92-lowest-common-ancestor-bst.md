# Lowest Common Ancestor of a Binary Search Tree

**Pattern:** Binary Search Tree

```csharp
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        while (root != null)
        {
            if (p.val < root.val && q.val < root.val) root = root.left;
            else if (p.val > root.val && q.val > root.val) root = root.right;
            else return root;
        }
        return null;
    }
}
```

**Complexity:** O(h) time and O(1) space, where h is tree height.
