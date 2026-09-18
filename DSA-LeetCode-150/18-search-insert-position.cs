public static class SearchInsertPosition
{
    public static int SearchBruteForce(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
            if (nums[i] >= target) return i;
        return nums.Length;
    }

    public static int Search(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1, answer = nums.Length;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] >= target) { answer = mid; right = mid - 1; }
            else left = mid + 1;
        }
        return answer;
    }
}