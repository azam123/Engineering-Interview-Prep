# 53. Serialize and Deserialize Binary Tree

**Pattern:** Binary Tree / BFS

## Approach
Serialize the tree using preorder traversal and `null` markers. Deserialize by consuming tokens recursively.

```csharp
public class Codec
{
    public string Serialize(TreeNode root)
    {
        var values = new List<string>();
        void Dfs(TreeNode node)
        {
            if (node == null) { values.Add("#"); return; }
            values.Add(node.val.ToString());
            Dfs(node.left);
            Dfs(node.right);
        }
        Dfs(root);
        return string.Join(",", values);
    }

    public TreeNode Deserialize(string data)
    {
        var tokens = data.Split(',');
        int index = 0;
        TreeNode Dfs()
        {
            string token = tokens[index++];
            if (token == "#") return null;
            var node = new TreeNode(int.Parse(token));
            node.left = Dfs();
            node.right = Dfs();
            return node;
        }
        return Dfs();
    }
}
```

**Complexity:** O(n) time and O(n) space.

**Follow-ups:** How would you serialize iteratively? How would you support very large trees?
