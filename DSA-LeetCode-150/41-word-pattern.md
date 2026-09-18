# 41. Word Pattern
- **Problem:** https://leetcode.com/problems/word-pattern/ | **Difficulty:** Easy
- **Pattern:** Bijective Hash Mapping
- **Explanation:** Pattern characters and words must map one-to-one.
- **Flow:** `Split words → Map both directions → Validate`
- **Complexity:** O(n) time, O(n) space.
- **Code:** [`41-word-pattern.cs`](./41-word-pattern.cs)
- **Walkthrough:** Two dictionaries prevent two pattern symbols mapping to one word or vice versa.
