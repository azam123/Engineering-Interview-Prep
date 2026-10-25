from __future__ import annotations

from typing import List


class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        """Return indices of two distinct values whose sum equals target.

        Average time: O(n). Auxiliary space: O(n).
        """
        seen: dict[int, int] = {}
        for index, value in enumerate(nums):
            complement = target - value
            if complement in seen:
                return [seen[complement], index]
            seen[value] = index
        return []


def two_sum_brute_force(nums: List[int], target: int) -> List[int]:
    """Baseline pair search. Time O(n^2), auxiliary space O(1)."""
    for left in range(len(nums)):
        for right in range(left + 1, len(nums)):
            if nums[left] + nums[right] == target:
                return [left, right]
    return []


if __name__ == "__main__":
    assert Solution().twoSum([2, 7, 11, 15], 9) == [0, 1]
    assert Solution().twoSum([3, 2, 4], 6) == [1, 2]
    assert Solution().twoSum([3, 3], 6) == [0, 1]
    assert two_sum_brute_force([2, 7, 11, 15], 9) == [0, 1]
    print("Two Sum examples passed.")
