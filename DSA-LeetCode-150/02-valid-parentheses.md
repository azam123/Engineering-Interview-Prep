# 2. Valid Parentheses

- **Problem link:** https://leetcode.com/problems/valid-parentheses/
- **Difficulty:** Easy
- **Topic:** Stack, String
- **Pattern:** Matching pairs / last-in-first-out
- **Data structure / algorithm:** Stack

## Problem Explanation

Given a string containing `()`, `{}`, and `[]`, determine whether brackets are correctly opened and closed in the right order.

Examples:

```text
"()[]{}" → true
"([)]" → false
"{[]}" → true
```

## Clarifying Questions

1. Can the string contain characters other than brackets?
2. Is an empty string valid?
3. Must opening and closing bracket types match exactly?
4. Can brackets be nested? (Yes.)

## Brute Force Approach

Repeatedly remove valid adjacent pairs such as `()`, `[]`, and `{}` until no replacement is possible. The string is valid only if it becomes empty.

- **Pattern / DSA:** String replacement
- **Time:** Up to `O(n²)` due to repeated scans and allocations
- **Space:** `O(n)`
- **Drawback:** Repeated string modifications are expensive and obscure the nesting logic.

## Optimized Approach

Push opening brackets onto a stack. For every closing bracket, verify that the stack is non-empty and its top is the matching opening bracket. The stack must be empty at the end.

```csharp
public bool IsValid(string s)
{
    var stack = new Stack<char>();
    var pairs = new Dictionary<char, char>
    {
        [')'] = '(',
        [']'] = '[',
        ['}'] = '{'
    };

    foreach (char ch in s)
    {
        if (pairs.ContainsValue(ch))
        {
            stack.Push(ch);
        }
        else if (!pairs.TryGetValue(ch, out char opening) ||
                 stack.Count == 0 || stack.Pop() != opening)
        {
            return false;
        }
    }

    return stack.Count == 0;
}
```

- **Time:** `O(n)`
- **Space:** `O(n)` worst case

## Dry Run

Input: `"{[]}"`

| Character | Action | Stack |
|---|---|---|
| `{` | Push | `{` |
| `[` | Push | `{ [` |
| `]` | Pop `[` | `{` |
| `}` | Pop `{` | empty |

Result: `true`.

## Further Optimization?

The asymptotic complexity cannot be improved beyond `O(n)` because every character must be examined. A fixed-size array can replace the stack implementation for lower allocation overhead, but the complexity remains the same.
