# 30. Zigzag Conversion
- **Problem:** https://leetcode.com/problems/zigzag-conversion/ | **Difficulty:** Medium
- **Topic:** String | **Pattern:** Simulation.

## Explanation
Write characters across rows in a down-and-up zigzag, then read rows. `PAYPALISHIRING`, 3 rows → `PAHNAPLSIIGYIR`.

## Clarifying Questions
- What if rows = 1 or rows exceed string length?
- Is input ASCII or Unicode?

## Brute Force
Calculate each character's row mathematically and append to row buffers: **O(n)** time, **O(n)** space.

## Optimized Approach
Simulate row movement using direction reversal at the first and last row. **O(n)** time, **O(n)** space.

## Dry Run
For 3 rows, indices move rows `0,1,2,1,0...`.

## Further Optimization
The row buffers are already appropriate because output itself is linear.

## C#
See `30-zigzag-conversion.cs`.
