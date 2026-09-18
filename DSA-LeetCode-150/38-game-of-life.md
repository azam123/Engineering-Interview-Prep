# 38. Game of Life
- **Problem:** https://leetcode.com/problems/game-of-life/ | **Difficulty:** Medium
- **Pattern:** Simulation, In-place State Encoding
- **Explanation:** Count live neighbors and encode old/new states so updates do not affect later calculations.
- **Flow:** `Count neighbors → Encode transition → Normalize states`
- **Brute Force:** Copy board: O(mn) time, O(mn) space.
- **Optimized:** In-place encoding: O(mn) time, O(1) extra space.
- **Code:** [`38-game-of-life.cs`](./38-game-of-life.cs)
- **Walkthrough:** `2` means dead→live and `3` means live→dead; use `value % 2` to read the original state.
