# 56. Time Based Key-Value Store

**Pattern:** HashMap + Binary Search

## Approach
Store values for each key in timestamp order. Use binary search to find the latest timestamp not greater than the requested time.

```csharp
public class TimeMap
{
    private readonly Dictionary<string, List<(int Time, string Value)>> _store = new();

    public void Set(string key, string value, int timestamp)
    {
        if (!_store.ContainsKey(key)) _store[key] = new();
        _store[key].Add((timestamp, value));
    }

    public string Get(string key, int timestamp)
    {
        if (!_store.TryGetValue(key, out var items)) return string.Empty;
        int left = 0, right = items.Count - 1;
        string result = string.Empty;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (items[mid].Time <= timestamp)
            {
                result = items[mid].Value;
                left = mid + 1;
            }
            else right = mid - 1;
        }
        return result;
    }
}
```

**Complexity:** `Set` O(1) amortized; `Get` O(log n) time; O(n) storage.

**Follow-ups:** How would you handle out-of-order timestamps? How would you make the store thread-safe?
