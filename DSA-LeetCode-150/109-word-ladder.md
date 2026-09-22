# 109. Word Ladder

## Problem
Find the length of the shortest transformation sequence where each step changes exactly one character and every intermediate word exists in the dictionary.

## Approach
Treat words as graph nodes and use BFS because every valid transformation has equal cost.

## C# Solution
~~~csharp
public class Solution
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        var words = new HashSet<string>(wordList);
        if (!words.Contains(endWord)) return 0;

        var queue = new Queue<string>();
        queue.Enqueue(beginWord);
        int depth = 1;

        while (queue.Count > 0)
        {
            for (int level = queue.Count; level > 0; level--)
            {
                string word = queue.Dequeue();
                char[] chars = word.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    char original = chars[i];

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == original) continue;
                        chars[i] = c;
                        string next = new string(chars);

                        if (next == endWord) return depth + 1;
                        if (words.Remove(next)) queue.Enqueue(next);
                    }

                    chars[i] = original;
                }
            }
            depth++;
        }

        return 0;
    }
}
~~~

## Complexity
Approximately O(N × L × 26), where N is dictionary size and L is word length.

## Interview Follow-ups
- When is bidirectional BFS useful?
- How can wildcard patterns reduce neighbor generation?
- Why is DFS not appropriate for shortest path?
