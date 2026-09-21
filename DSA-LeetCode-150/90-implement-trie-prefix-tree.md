# 90. Implement Trie (Prefix Tree)

**Pattern:** Trie

## C# Solution
```csharp
public class Trie
{
    private class Node
    {
        public Node[] Children = new Node[26];
        public bool IsWord;
    }

    private readonly Node root = new();

    public void Insert(string word)
    {
        Node node = root;
        foreach (char c in word)
        {
            int index = c - 'a';
            node.Children[index] ??= new Node();
            node = node.Children[index];
        }
        node.IsWord = true;
    }

    public bool Search(string word)
    {
        Node node = Find(word);
        return node != null && node.IsWord;
    }

    public bool StartsWith(string prefix) => Find(prefix) != null;

    private Node Find(string text)
    {
        Node node = root;
        foreach (char c in text)
        {
            node = node.Children[c - 'a'];
            if (node == null) return null;
        }
        return node;
    }
}
```

**Complexity:** `O(L)` per operation, where `L` is the word or prefix length. Space is `O(total characters inserted)`.
