# 33. Valid Sudoku

- **Problem:** https://leetcode.com/problems/valid-sudoku/
- **Difficulty:** Medium
- **Topic:** Matrix, Hashing
- **Pattern:** Constraint validation
- **Data Structure:** HashSet

## Explanation
Validate every row, column, and 3×3 box. A digit may appear only once in each constraint. We scan each cell once and record a composite key for row, column, and box.

## Clarifying Questions
- Are only digits `1-9` and `.` present?
- Should the board be modified?

## Brute Force
For every digit, repeatedly scan its row, column, and box. This repeats work and costs roughly O(9³) for a fixed board.

## Optimized Approach
Use three sets per row, column, and box. For cell `(r,c)`, box index is `(r / 3) * 3 + c / 3`.

**Flow:** Read cell → skip `.` → calculate box → check duplicate → insert.

- **Time:** O(1) for a 9×9 board; generally O(n²)
- **Space:** O(1) fixed board; generally O(n²)

## Dry Run
For `5` at `(0,0)`, check row 0, column 0, and box 0. If any already contains `5`, return false.

## Further Optimization
The set-based solution is already optimal for one board scan.

See [`33-Valid-Sudoku.cs`](./33-Valid-Sudoku.cs).