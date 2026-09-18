# 5. Valid Palindrome

- **Problem link:** https://leetcode.com/problems/valid-palindrome/
- **Difficulty:** Easy
- **Topic:** String, Two Pointers
- **Pattern:** Converging two pointers
- **Data structure / algorithm:** String, two-pointer scan

## Problem Explanation

Determine whether a string is a palindrome after converting uppercase letters to lowercase and ignoring non-alphanumeric characters.

**Example:** `"A man, a plan, a canal: Panama"` → `true`.

## Clarifying Questions

1. Should spaces and punctuation be ignored? Yes.
2. Is comparison case-insensitive? Yes.
3. Can the input be empty? Yes.
4. Do we need to allocate a cleaned copy of the string?

## Brute Force Approach and Solution

Build a normalized string, reverse it, and compare it with the original normalized string.

```csharp
public bool IsPalindromeBruteForce(string s)
{
    var chars = s
        .Where(char.IsLetterOrDigit)
        .Select(char.ToLowerInvariant)
        .ToArray();

    return chars.SequenceEqual(chars.Reverse());
}
```

- **Pattern / DSA:** Normalization, reverse comparison
- **Time:** `O(n)`
- **Space:** `O(n)`
- **Drawback:** Creates an additional normalized representation.

## Optimized Approach and Solution

Use two pointers from both ends. Skip non-alphanumeric characters and compare normalized characters directly.

```csharp
public bool IsPalindrome(string s)
{
    int left = 0, right = s.Length - 1;

    while (left < right)
    {
        while (left < right && !char.IsLetterOrDigit(s[left])) left++;
        while (left < right && !char.IsLetterOrDigit(s[right])) right--;

        if (char.ToLowerInvariant(s[left]) != char.ToLowerInvariant(s[right]))
            return false;

        left++;
        right--;
    }

    return true;
}
```

- **Time:** `O(n)`
- **Space:** `O(1)` auxiliary space

## Dry Run

For `"A man, a plan, a canal: Panama"`, compare `a↔a`, `m↔m`, `a↔a`, skipping punctuation/spaces. All pairs match → `true`.

## Is There a Further Optimized Approach?

Not asymptotically. The two-pointer solution already uses linear time and constant auxiliary space.
