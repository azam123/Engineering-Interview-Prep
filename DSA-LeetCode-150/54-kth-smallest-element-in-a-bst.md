# 54. Kth Smallest Element in a BST

**Pattern:** BST / Inorder Traversal

## Approach
Inorder traversal of a binary search tree visits values in ascending order. Stop when the kth node is visited.

```csharp
public int KthSmallest(TreeNode root, int k)
{
    var stack = new Stack<TreeNode>();
    var current = root;

    while (current != null || stack.Count > 0)
    {
        while (current != null)
        {
            stack.Push(current);
            current = current.left;
        }

        current = stack.Pop();
        if (--k == 0) return current.val;
        current = current.right;
    }

    throw new ArgumentOutOfRangeException(nameof(k));
}
```

**Complexity:** O(h + k) time, O(h) space, where h is tree height.

**Follow-ups:** How can you optimize repeated kth-element queries? What changes for a balanced tree?
