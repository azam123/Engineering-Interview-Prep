# 12. Roman to Integer

- **Problem:** https://leetcode.com/problems/roman-to-integer/
- **Difficulty:** Easy
- **Topic:** String, Hashing
- **Pattern / Data Structure / Algorithm:** Greedy scan with a lookup dictionary

## Explanation
Convert a Roman numeral into an integer. If a symbol is smaller than the next symbol, subtract it; otherwise, add it.

Examples:
- `III` → `3`
- `IV` → `4`
- `MCMXCIV` → `1994`

## Clarifying Questions
- Are Roman numerals guaranteed to be valid?
- Should lowercase input be accepted?
- Is the input limited to the standard Roman numeral range?

## Brute Force Approach
Repeatedly match two-character subtractive combinations (`IV`, `IX`, `XL`, `XC`, `CD`, `CM`) and then process individual symbols. This requires extra conditional logic and string handling.

- **Time:** O(n)
- **Space:** O(1)
- **Drawback:** More special-case logic and less maintainable.

## Optimized Approach
Scan from left to right using a value map. When the current value is smaller than the next value, subtract it; otherwise, add it. The final symbol is always added.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
For `MCMIV`: add `M` (1000), subtract `C` (100), add `M` (1000), subtract `I` (1), add `V` (5) → `1904`.

## Further Optimization
The asymptotic complexity cannot be improved because every character must be inspected. A switch expression can reduce dictionary overhead but does not change complexity.

See the matching C# implementation: `12-roman-to-integer.cs`.