# Longest Increasing Subsequence

**Problem:** Find the length of the longest strictly increasing subsequence.

## Approach: Patience Sorting / Binary Search
Maintain the smallest possible tail value for each subsequence length. Replace the first tail greater than or equal to the current number.

```csharp
public class Solution
{
    public int LengthOfLIS(int[] nums)
    {
        var tails = new List<int>();

        foreach (int number in nums)
        {
            int left = 0, right = tails.Count;

            while (left < right)
            {
                int middle = left + (right - left) / 2;
                if (tails[middle] < number) left = middle + 1;
                else right = middle;
            }

            if (left == tails.Count) tails.Add(number);
            else tails[left] = number;
        }

        return tails.Count;
    }
}
```

**Time:** O(n log n)  
**Space:** O(n)

**Follow-up:** Return the actual subsequence, not only its length.
