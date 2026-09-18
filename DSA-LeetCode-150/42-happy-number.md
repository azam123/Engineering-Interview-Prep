# 42. Happy Number
- **Problem:** https://leetcode.com/problems/happy-number/ | **Difficulty:** Easy
- **Pattern:** Cycle Detection, Hash Set
- **Explanation:** Repeatedly replace a number with the sum of squared digits. A happy number reaches 1; otherwise it cycles.
- **Flow:** `Calculate digit square sum → Check 1 → Detect cycle`
- **Brute Force:** Track all seen values using a set: O(log n) practical space.
- **Optimized:** Floyd's cycle detection uses O(1) extra space.
- **Complexity:** O(log n) digits per iteration; bounded number of states.
- **Code:** [`42-happy-number.cs`](./42-happy-number.cs)
- **Walkthrough:** Slow advances one transformation and fast advances two. If fast reaches 1, return true; if slow meets fast, a cycle exists.
