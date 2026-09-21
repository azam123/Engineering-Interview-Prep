# 84. Subsets

**Pattern:** Backtracking

## C# Solution
```csharp
public class Solution
{
    public IList<IList<int>> Subsets(int[] nums)
    {
        var result = new List<IList<int>>();
        void Dfs(int index, List<int> path)
        {
            result.Add(new List<int>(path));
            for (int i = index; i < nums.Length; i++)
            {
                path.Add(nums[i]);
                Dfs(i + 1, path);
                path.RemoveAt(path.Count - 1);
            }
        }
        Dfs(0, new List<int>());
        return result;
    }
}
```

**Complexity:** `O(n·2ⁿ)` time and `O(n)` recursion space, excluding output.
