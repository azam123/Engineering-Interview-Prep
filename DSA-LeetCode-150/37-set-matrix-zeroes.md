# 37. Set Matrix Zeroes
- **Problem:** https://leetcode.com/problems/set-matrix-zeroes/ | **Difficulty:** Medium
- **Pattern:** In-place Matrix Marking
- **Explanation:** Use first row and column as markers; preserve whether they originally contain zero.
- **Flow:** `Record first row/column → Mark → Zero marked rows/columns`
- **Brute Force:** Store rows/columns in sets: O(mn) time, O(m+n) space.
- **Optimized:** Marker cells: O(mn) time, O(1) extra space.
- **Dry Run:** A zero at `(1,2)` clears row 1 and column 2.
- **Code:** [`37-set-matrix-zeroes.cs`](./37-set-matrix-zeroes.cs)
- **Code Walkthrough:** Boolean flags protect first row/column while markers guide the final pass.
