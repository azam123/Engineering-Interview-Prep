# 25. Gas Station

- **Problem:** https://leetcode.com/problems/gas-station/
- **Difficulty:** Medium
- **Topic:** Array, Greedy
- **Pattern:** Greedy / running balance
- **Data Structure:** Array
- **Algorithm:** Track total gas feasibility and the current tank; reset the starting point whenever the running balance becomes negative.

## Problem Explanation

There are circular gas stations. `gas[i]` is the gas available at station `i`, while `cost[i]` is required to travel to the next station. Return a starting station index that lets you complete the circuit, or `-1` if impossible.

**Example:** `gas=[1,2,3,4,5]`, `cost=[3,4,5,1,2]` → `3`.

## Clarifying Questions

1. Is the route circular? Yes.
2. Can gas and cost contain zero? Yes.
3. Is there guaranteed to be at most one valid start? The problem guarantees uniqueness when a solution exists.

## Brute Force

Try each station as the starting point and simulate the complete circuit.

- **Time:** O(n²)
- **Space:** O(1)
- **Drawback:** Repeats the same route simulation from many starting positions.

## Optimized Approach

Let `diff[i] = gas[i] - cost[i]`. If total gas is less than total cost, no solution exists. While scanning, maintain `tank`. If `tank` becomes negative at station `i`, none of the stations from the current start through `i` can be a valid start, so set `start = i + 1` and reset `tank`.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run

`gas=[1,2,3,4,5]`, `cost=[3,4,5,1,2]` → differences `[-2,-2,-2,3,3]`.

Running balance fails after stations 0, 1, and 2, so the candidate moves forward. Start at index `3`: balance becomes `3`, then `6`, then `4`, then `2`, then `0` → complete circuit.

## Further Optimization

The greedy one-pass solution is asymptotically optimal: O(n) time and O(1) auxiliary space.

## C# Solution

See [`25-Gas-Station.cs`](25-Gas-Station.cs).
