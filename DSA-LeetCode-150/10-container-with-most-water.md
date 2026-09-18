# 10. Container With Most Water

- **Problem link:** https://leetcode.com/problems/container-with-most-water/
- **Difficulty:** Medium
- **Topic:** Array, Two Pointers, Greedy
- **Pattern:** Opposite-direction two pointers
- **Data structure / algorithm:** Array scan with left/right pointers

## Problem Explanation

Given heights of vertical lines, find the maximum water area formed by two lines.

Example: `height = [1,8,6,2,5,4,8,3,7]` → `49`.

## Clarifying Questions

1. Can the array contain fewer than two elements?
2. Are heights non-negative?
3. Is the width calculated as the index difference?
4. Do we need the pair of indexes or only the maximum area?

## Brute Force Approach and Solution

Try every pair of lines.

```csharp
public int MaxAreaBruteForce(int[] height)
{
    int best = 0;
    for (int i = 0; i < height.Length; i++)
    {
        for (int j = i + 1; j < height.Length; j++)
        {
            int area = Math.Min(height[i], height[j]) * (j - i);
            best = Math.Max(best, area);
        }
    }
    return best;
}
```

- **Pattern / DSA:** Exhaustive pair enumeration
- **Time:** `O(n²)`
- **Space:** `O(1)`
- **Drawback:** Evaluates every pair, which is expensive for large arrays.

## Optimized Approach and Solution

Start at both ends. The area is limited by the shorter line, so move the pointer at the shorter line inward.

```csharp
public int MaxArea(int[] height)
{
    int left = 0, right = height.Length - 1, best = 0;

    while (left < right)
    {
        int width = right - left;
        int area = Math.Min(height[left], height[right]) * width;
        best = Math.Max(best, area);

        if (height[left] <= height[right]) left++;
        else right--;
    }

    return best;
}
```

- **Time:** `O(n)`
- **Space:** `O(1)`

## Dry Run

For `[1,8,6,2,5,4,8,3,7]`, begin with indexes `0` and `8`: area = `min(1,7) * 8 = 8`. Move the left pointer because height `1` is smaller. Continue until pointers meet; the maximum area found is `49`.

## Further Optimization?

The two-pointer solution is asymptotically optimal because both boundaries must be considered and the array requires linear inspection.

See the executable implementation in [`10-container-with-most-water.cs`](10-container-with-most-water.cs).
