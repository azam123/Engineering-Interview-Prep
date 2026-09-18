# 11. Jump Game

- **Problem link:** https://leetcode.com/problems/jump-game/
- **Difficulty:** Medium
- **Topic:** Array, Greedy
- **Pattern:** Track the farthest reachable index
- **Data structure / algorithm:** Greedy scan

## Problem Explanation

Given `nums`, where each value represents the maximum jump length from that index, determine whether the last index is reachable.

Example: `[2,3,1,1,4]` → `true`; `[3,2,1,0,4]` → `false`.

## Clarifying Questions

1. Are all values non-negative?
2. Can the array be empty?
3. Do we need the minimum number of jumps or only reachability?
4. Can a jump be shorter than the maximum allowed distance?

## Brute Force Approach and Solution

Use recursive backtracking to try every possible jump.

```csharp
public bool CanJumpBruteForce(int[] nums)
{
    return Explore(nums, 0);
}

private bool Explore(int[] nums, int index)
{
    if (index >= nums.Length - 1) return true;

    int furthest = Math.Min(index + nums[index], nums.Length - 1);
    for (int next = index + 1; next <= furthest; next++)
        if (Explore(nums, next)) return true;

    return false;
}
```

- **Pattern / DSA:** Backtracking
- **Time:** Exponential in the worst case
- **Space:** `O(n)` recursion depth
- **Drawback:** Repeats the same subproblems many times.

## Optimized Approach and Solution

Maintain the farthest index reachable so far. If the current index exceeds it, the end cannot be reached.

```csharp
public bool CanJump(int[] nums)
{
    int farthest = 0;

    for (int i = 0; i < nums.Length; i++)
    {
        if (i > farthest) return false;
        farthest = Math.Max(farthest, i + nums[i]);
        if (farthest >= nums.Length - 1) return true;
    }

    return true;
}
```

- **Time:** `O(n)`
- **Space:** `O(1)`

## Dry Run

For `[2,3,1,1,4]`: farthest starts at `0`; index `0` extends it to `2`; index `1` extends it to `4`; since `4` is the last index, return `true`.

## Further Optimization?

The greedy solution is optimal in asymptotic complexity: every element may need to be inspected once.

See [`11-jump-game.cs`](11-jump-game.cs).
