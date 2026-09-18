# 39. Ransom Note
- **Problem:** https://leetcode.com/problems/ransom-note/ | **Difficulty:** Easy
- **Pattern:** Frequency Counting
- **Explanation:** Count magazine characters, then consume counts for ransomNote.
- **Flow:** `Count magazine → Consume ransom characters → Validate`
- **Brute Force:** Repeated search/removal: O(mn) time.
- **Optimized:** Frequency array: O(m+n) time, O(1) space.
- **Code:** [`39-ransom-note.cs`](./39-ransom-note.cs)
- **Walkthrough:** Increment counts first; decrement for each required character and fail if count is zero.
