# 59. Minimum Window Substring

**Pattern:** Sliding Window + Frequency Map

## Approach
Maintain required character counts and expand the right pointer until the window is valid. Then shrink from the left while preserving validity.

## C# Solution
```csharp
public class Solution
{
    public string MinWindow(string s, string t)
    {
        if (s.Length < t.Length) return "";
        var need = new Dictionary<char, int>();
        foreach (var c in t) need[c] = need.GetValueOrDefault(c) + 1;

        int left = 0, formed = 0, bestStart = 0, bestLength = int.MaxValue;
        var window = new Dictionary<char, int>();

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            window[c] = window.GetValueOrDefault(c) + 1;
            if (need.ContainsKey(c) && window[c] == need[c]) formed++;

            while (formed == need.Count)
            {
                if (right - left + 1 < bestLength)
                {
                    bestStart = left;
                    bestLength = right - left + 1;
                }

                char removed = s[left++];
                window[removed]--;
                if (need.ContainsKey(removed) && window[removed] < need[removed]) formed--;
            }
        }

        return bestLength == int.MaxValue ? "" : s.Substring(bestStart, bestLength);
    }
}
```

**Complexity:** O(|s| + |t|) time and O(|s| + |t|) space.
