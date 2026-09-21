# 81. Palindromic Substrings

**Pattern:** Expand Around Center

## Approach
Every palindrome has a center. Expand from each character (odd length) and each gap (even length), counting valid expansions.

## C# Solution
```csharp
public class Solution
{
    public int CountSubstrings(string s)
    {
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            count += Expand(s, i, i);
            count += Expand(s, i, i + 1);
        }
        return count;
    }

    private int Expand(string s, int left, int right)
    {
        int count = 0;
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            count++; left--; right++;
        }
        return count;
    }
}
```

**Complexity:** `O(n²)` time and `O(1)` extra space.
