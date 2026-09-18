# 26. Candy
- **Problem:** https://leetcode.com/problems/candy/
- **Difficulty:** Hard
- **Topic:** Array, Greedy
- **Pattern / DS / Algorithm:** Two directional greedy scans; array.

## Explanation
Give each child at least one candy. A child with a higher rating than an adjacent child must receive more. Example: `[1,0,2] → 5`.

## Clarifying Questions
- Are ratings non-negative integers? Can the array be empty?
- Must both left and right neighbor constraints be satisfied?

## Brute Force
Start every child with one candy and repeatedly increase values violating either neighbor rule until stable. Worst case can be **O(n²)** time and **O(n)** space.

## Optimized Approach
Scan left-to-right for increasing runs, then right-to-left for decreasing constraints; use `max` to preserve both requirements. **O(n)** time and **O(n)** space.

## Dry Run
`[1,0,2]`: left pass `[1,1,2]`; right pass gives `[2,1,2]`; sum = `5`.

## Further Optimization
A slope-based one-pass solution can use **O(1)** extra space, but is more error-prone.

## C#
See `26-candy.cs`.
