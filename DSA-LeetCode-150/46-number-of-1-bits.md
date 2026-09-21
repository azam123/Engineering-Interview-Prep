# 46. Number of 1 Bits

**Problem:** Count the set bits in the binary representation of an unsigned integer.

## Approach
Use Brian Kernighan's algorithm. Each operation `n &= n - 1` removes the lowest set bit.

## C# Solution
```csharp
public class Solution
{
    public int HammingWeight(uint n)
    {
        int count = 0;
        while (n != 0)
        {
            n &= n - 1;
            count++;
        }

        return count;
    }
}
```

**Complexity:** `O(k)` time, where `k` is the number of set bits, and `O(1)` space.

**Follow-ups:** Reverse bits, find the missing number, and compute Hamming distance.
