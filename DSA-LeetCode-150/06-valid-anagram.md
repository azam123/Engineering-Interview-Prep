# 6. Valid Anagram

- **Problem link:** https://leetcode.com/problems/valid-anagram/
- **Difficulty:** Easy
- **Topic:** String, Hash Table
- **Pattern:** Frequency counting
- **Data structure / algorithm:** Dictionary / fixed frequency array

## Problem Explanation

Return `true` when two strings contain exactly the same characters with the same frequencies.

**Example:** `"anagram"`, `"nagaram"` → `true`; `"rat"`, `"car"` → `false`.

## Clarifying Questions

1. Are inputs limited to lowercase English letters?
2. Should whitespace and punctuation be treated as characters?
3. Do both strings need to have the same length?
4. Is Unicode possible?

## Brute Force Approach and Solution

Sort both strings and compare the resulting character sequences.

```csharp
public bool IsAnagramBruteForce(string s, string t)
{
    if (s.Length != t.Length) return false;
    return s.OrderBy(c => c).SequenceEqual(t.OrderBy(c => c));
}
```

- **Pattern / DSA:** Sorting
- **Time:** `O(n log n)`
- **Space:** `O(n)` depending on sorting implementation
- **Drawback:** Sorting performs unnecessary ordering work when only frequencies matter.

## Optimized Approach and Solution

Count each character in the first string and subtract counts using the second string.

```csharp
public bool IsAnagram(string s, string t)
{
    if (s.Length != t.Length) return false;

    var counts = new Dictionary<char, int>();

    foreach (char c in s)
        counts[c] = counts.GetValueOrDefault(c) + 1;

    foreach (char c in t)
    {
        if (!counts.TryGetValue(c, out int count)) return false;
        if (count == 1) counts.Remove(c);
        else counts[c] = count - 1;
    }

    return counts.Count == 0;
}
```

- **Time:** `O(n)` average
- **Space:** `O(k)`, where `k` is the number of distinct characters

## Dry Run

`anagram` creates counts `{a:3,n:1,g:1,r:1,m:1}`. Processing `nagaram` decrements each count to zero → valid anagram.

## Is There a Further Optimized Approach?

If the alphabet is known to be lowercase English letters, use `int[26]` for simpler constant-sized storage.
