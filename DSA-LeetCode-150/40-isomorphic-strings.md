# 40. Isomorphic Strings
- **Problem:** https://leetcode.com/problems/isomorphic-strings/ | **Difficulty:** Easy
- **Pattern:** Two-way Mapping
- **Explanation:** Characters must map consistently in both directions.
- **Flow:** `Map s→t and t→s → Detect conflict`
- **Complexity:** O(n) time, O(1) space for ASCII.
- **Code:** [`40-isomorphic-strings.cs`](./40-isomorphic-strings.cs)
- **Walkthrough:** Two arrays store last mappings; mismatched mappings return false.
