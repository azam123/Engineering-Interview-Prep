# 58. LRU Cache

**Pattern:** HashMap + Doubly Linked List

## Approach
Use a dictionary for O(1) lookup and a linked list to track recency. The head is most recently used; the tail is least recently used.

## C# Solution
```csharp
public class LRUCache
{
    private readonly int capacity;
    private readonly Dictionary<int, LinkedListNode<(int Key, int Value)>> map = new();
    private readonly LinkedList<(int Key, int Value)> list = new();

    public LRUCache(int capacity) => this.capacity = capacity;

    public int Get(int key)
    {
        if (!map.TryGetValue(key, out var node)) return -1;
        list.Remove(node);
        list.AddFirst(node);
        return node.Value.Value;
    }

    public void Put(int key, int value)
    {
        if (map.TryGetValue(key, out var existing))
        {
            list.Remove(existing);
            map.Remove(key);
        }

        var node = new LinkedListNode<(int Key, int Value)>((key, value));
        list.AddFirst(node);
        map[key] = node;

        if (map.Count > capacity)
        {
            var leastRecent = list.Last!;
            list.RemoveLast();
            map.Remove(leastRecent.Value.Key);
        }
    }
}
```

**Complexity:** O(1) time per operation; O(capacity) space.
