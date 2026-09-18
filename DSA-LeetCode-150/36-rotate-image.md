# 36. Rotate Image
- **Problem:** https://leetcode.com/problems/rotate-image/ | **Difficulty:** Medium
- **Pattern:** Matrix, Transpose + Reverse
- **Explanation:** Transpose the square matrix, then reverse every row to rotate 90° clockwise.
- **Flow:** `Transpose → Reverse rows → Rotated matrix`
- **Brute Force:** Copy into another matrix: O(n²) time, O(n²) space.
- **Optimized:** In-place transpose and row reversal: O(n²) time, O(1) space.
- **Dry Run:** `[[1,2],[3,4]] → [[3,1],[4,2]]`.
- **Code:** [`36-rotate-image.cs`](./36-rotate-image.cs)
- **Code Walkthrough:** Swap `matrix[r][c]` with `matrix[c][r]`, then reverse each row using two pointers.
