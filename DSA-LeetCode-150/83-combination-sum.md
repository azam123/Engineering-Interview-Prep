# 83. Combination Sum

**Pattern:** Backtracking

## Approach
Try each candidate from the current index onward. Reusing the same candidate is allowed, so recursive calls keep the current index. Stop when the remaining target becomes zero or negative.

## C# Solution
```csharp
public class Solution
{
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        var result = new List<IList<int>>();
        Array.Sort(candidates);
        Backtrack(candidates, target, 0, new List<int>(), result);
        return result;
    }

    private void Backtrack(int[] a, int remain, int start, List<int> path,
        IList<IList<int>> result)
    {
        if (remain == 0) { result.Add(new List<int>(path)); return; }
        for (int i = start; i < a.Length && a[i] <= remain; i++)
        {
            path.Add(a[i]);
            Backtrack(a, remain - a[i], i, path, result);
            path.RemoveAt(path.Count - 1);
        }
    }
}
```

**Complexity:** Exponential in the worst case; recursion space is `O(target)` approximately.
