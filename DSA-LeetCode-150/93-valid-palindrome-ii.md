# 93. Valid Palindrome II

**Pattern:** Two Pointers

## C# Solution
```csharp
public class Solution
{
    public bool ValidPalindrome(string s)
    {
        int left = 0, right = s.Length - 1;
        while (left < right)
        {
            if (s[left] != s[right])
                return IsPalindrome(s, left + 1, right) || IsPalindrome(s, left, right - 1);
            left++; right--;
        }
        return true;
    }

    private bool IsPalindrome(string s, int left, int right)
    {
        while (left < right)
            if (s[left++] != s[right--]) return false;
        return true;
    }
}
```

**Complexity:** O(n) time and O(1) auxiliary space.
