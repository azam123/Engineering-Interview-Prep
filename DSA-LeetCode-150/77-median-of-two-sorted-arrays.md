# 77. Median of Two Sorted Arrays

**Pattern:** Binary Search on Partition

## Approach
Binary-search the smaller array and choose a partition where every value on the left is less than or equal to every value on the right.

## C# Solution
```csharp
public class Solution
{
    public double FindMedianSortedArrays(int[] a, int[] b)
    {
        if (a.Length > b.Length) return FindMedianSortedArrays(b, a);
        int m = a.Length, n = b.Length, low = 0, high = m;
        while (low <= high)
        {
            int i = (low + high) / 2, j = (m + n + 1) / 2 - i;
            int leftA = i == 0 ? int.MinValue : a[i - 1];
            int rightA = i == m ? int.MaxValue : a[i];
            int leftB = j == 0 ? int.MinValue : b[j - 1];
            int rightB = j == n ? int.MaxValue : b[j];

            if (leftA <= rightB && leftB <= rightA)
            {
                if ((m + n) % 2 == 1) return Math.Max(leftA, leftB);
                return (Math.Max(leftA, leftB) + (double)Math.Min(rightA, rightB)) / 2;
            }
            if (leftA > rightB) high = i - 1;
            else low = i + 1;
        }
        throw new ArgumentException("Arrays are not sorted.");
    }
}
```

## Complexity
- Time: O(log(min(m, n))).
- Space: O(1).
