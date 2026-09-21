# 85. Permutations

**Pattern:** Backtracking

## C# Solution
```csharp
public class Solution
{
    public IList<IList<int>> Permute(int[] nums)
    {
        var result = new List<IList<int>>();
        void Dfs(List<int> path, bool[] used)
        {
            if (path.Count == nums.Length) { result.Add(new List<int>(path)); return; }
            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i]) continue;
                used[i] = true; path.Add(nums[i]);
                Dfs(path, used);
                path.RemoveAt(path.Count - 1); used[i] = false;
            }
        }
        Dfs(new List<int>(), new bool[nums.Length]);
        return result;
    }
}
```

**Complexity:** `O(n·n!)` time and `O(n)` recursion space, excluding output.
