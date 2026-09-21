# Word Break

**Problem:** Determine whether a string can be segmented into dictionary words.

## Approach: Dynamic Programming
`dp[i]` indicates whether the prefix ending at index `i` can be segmented. Try every dictionary word at each reachable position.

```csharp
public class Solution
{
    public bool WordBreak(string s, IList<string> wordDict)
    {
        var words = new HashSet<string>(wordDict);
        bool[] dp = new bool[s.Length + 1];
        dp[0] = true;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (dp[j] && words.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    break;
                }
            }
        }

        return dp[s.Length];
    }
}
```

**Time:** O(n² × k) approximately, where `k` is substring/hash cost  
**Space:** O(n + dictionary size)

**Follow-up:** Optimize using a Trie or maximum dictionary word length.
