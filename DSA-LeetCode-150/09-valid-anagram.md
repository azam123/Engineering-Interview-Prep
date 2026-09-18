# 9. Valid Anagram

- **Problem link:** https://leetcode.com/problems/valid-anagram/
- **Difficulty:** Easy
- **Topic:** String, Hash Map, Sorting
- **Pattern:** Frequency counting
- **Data structure / algorithm:** Dictionary or fixed-size frequency array

## Problem Explanation

Given two strings `s` and `t`, return `true` if `t` is an anagram of `s`; otherwise return `false`.

Examples:

```text
s = "anagram", t = "nagaram" => true
s = "rat", t = "car" => false
```

## Clarifying Questions

1. Are the strings limited to lowercase English letters?
2. Should spaces and punctuation be treated as characters?
3. Is case sensitivity required?
4. Can the strings have different lengths?

## Brute Force Approach and Solution

Sort both strings and compare the resulting character sequences.

```csharp
public bool IsAnagramBruteForce(string s, string t)
{
    if (s.Length != t.Length) return false;

    char[] first = s.ToCharArray();
    char[] second = t.ToCharArray();
    Array.Sort(first);
    Array.Sort(second);

    return first.SequenceEqual(second);
}
```

- **Pattern / DSA:** Sorting and comparison
- **Time:** `O(n log n)`
- **Space:** `O(n)` for character arrays
- **Drawback:** Sorting performs more work than necessary when only character frequencies matter.

## Optimized Approach and Solution

Count each character from `s` and decrement the count using `t`. Every count must end at zero.

```csharp
public bool IsAnagram(string s, string t)
{
    if (s.Length != t.Length) return false;

    var counts = new Dictionary<char, int>();

    foreach (char c in s)
        counts[c] = counts.GetValueOrDefault(c) + 1;

    foreach (char c in t)
    {
        if (!counts.TryGetValue(c, out int count) || count == 0)
            return false;

        counts[c] = count - 1;
    }

    return true;
}
```

- **Time:** `O(n)` average
- **Space:** `O(k)`, where `k` is the number of distinct characters

## Dry Run

For `s = "anagram"`, `t = "nagaram"`:

1. Build frequencies: `a=3, n=1, g=1, r=1, m=1`.
2. Decrement counts while reading `t`.
3. Every decrement is valid and all counts reach zero.
4. Return `true`.

## Further Optimization?

If inputs contain only lowercase English letters, use an integer array of length 26. This gives `O(n)` time and `O(1)` auxiliary space.

```csharp
public bool IsAnagramLowercase(string s, string t)
{
    if (s.Length != t.Length) return false;

    int[] counts = new int[26];

    foreach (char c in s) counts[c - 'a']++;
    foreach (char c in t) counts[c - 'a']--;

    return counts.All(value => value == 0);
}
```
