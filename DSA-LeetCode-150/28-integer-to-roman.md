# 28. Integer to Roman
- **Problem:** https://leetcode.com/problems/integer-to-roman/ | **Difficulty:** Medium
- **Topic:** String, Greedy | **Pattern:** Greedy denomination matching.

## Explanation
Convert an integer using descending Roman numeral values, including subtractive pairs such as `IV`, `IX`, `XL`, `XC`, `CD`, and `CM`. Example: `1994 → MCMXCIV`.

## Clarifying Questions
- Is the input within `1..3999`?
- Should output use standard Roman notation?

## Brute Force
Build each numeral through repeated single-value subtraction; it remains correct but performs unnecessary iterations. **O(n)** time, **O(1)** auxiliary space.

## Optimized Approach
Store values and symbols in descending order and greedily append while the value fits. **O(1)** time for bounded Roman range and **O(1)** auxiliary space.

## Dry Run
`58`: append `L`, then `V`, then `III` → `LVIII`.

## Further Optimization
No meaningful asymptotic improvement is needed for the fixed input range.

## C#
See `28-integer-to-roman.cs`.
