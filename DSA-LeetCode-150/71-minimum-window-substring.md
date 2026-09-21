# 71. Minimum Window Substring

**Pattern:** Sliding Window

## Approach
Expand the right pointer until all required characters are covered, then shrink from the left while the window remains valid.

## C# Solution
```csharp
public class Solution
{
    public string MinWindow(string s, string t)
    {
        if (s.Length < t.Length) return string.Empty;
        var need = new int[128];
        foreach (char c in t) need[c]++;

        int left = 0, missing = t.Length, bestStart = 0, bestLength = int.MaxValue;
        for (int right = 0; right < s.Length; right++)
        {
            if (need[s[right]] > 0) missing--;
            need[s[right]]--;

            while (missing == 0)
            {
                if (right - left + 1 < bestLength)
                {
                    bestStart = left;
                    bestLength = right - left + 1;
                }

                need[s[left]]++;
                if (need[s[left]] > 0) missing++;
                left++;
            }
        }

        return bestLength == int.MaxValue ? string.Empty : s.Substring(bestStart, bestLength);
    }
}
```

## Complexity
- Time: O(|s| + |t|).
- Space: O(1) for the fixed ASCII table.
