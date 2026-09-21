# 70. LRU Cache

**Pattern:** HashMap + Doubly Linked List

## Approach
Use a dictionary for O(1) lookup and a doubly linked list to maintain recency. The most recently used item stays at the front; the least recently used item is removed from the end.

## C# Solution
```csharp
public class LRUCache
{
    private readonly int _capacity;
    private readonly Dictionary<int, LinkedListNode<(int Key, int Value)>> _map = new();
    private readonly LinkedList<(int Key, int Value)> _list = new();

    public LRUCache(int capacity) => _capacity = capacity;

    public int Get(int key)
    {
        if (!_map.TryGetValue(key, out var node)) return -1;
        _list.Remove(node);
        _list.AddFirst(node);
        return node.Value.Value;
    }

    public void Put(int key, int value)
    {
        if (_map.TryGetValue(key, out var existing))
        {
            _list.Remove(existing);
            _map.Remove(key);
        }

        var node = new LinkedListNode<(int Key, int Value)>((key, value));
        _list.AddFirst(node);
        _map[key] = node;

        if (_map.Count > _capacity)
        {
            var last = _list.Last!;
            _list.RemoveLast();
            _map.Remove(last.Value.Key);
        }
    }
}
```

## Complexity
- Time: O(1) for `Get` and `Put`.
- Space: O(capacity).
