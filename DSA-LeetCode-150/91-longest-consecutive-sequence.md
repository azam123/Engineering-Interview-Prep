# 91. Longest Consecutive Sequence

**Pattern:** Hash Set

## Approach
Store all values in a `HashSet<int>`. Start a sequence only when `num - 1` is absent, then count forward.

## C# Solution
```csharp
public class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        var set = new HashSet<int>(nums);
        int best = 0;

        foreach (int num in set)
        {
            if (set.Contains(num - 1)) continue;

            int length = 1;
            while (set.Contains(num + length)) length++;
            best = Math.Max(best, length);
        }

        return best;
    }
}
```

**Complexity:** O(n) average time, O(n) space.

**Interview Note:** The sequence does not need to be contiguous in the input array.
