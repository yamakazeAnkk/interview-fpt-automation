// Max/Min of Unsorted Array
// Time: O(n), Space: O(1)

public class Solution {
    public (int max, int min) FindMaxMin(int[] nums) {
        if (nums.Length == 0) throw new ArgumentException("Empty array");
        
        int max = nums[0], min = nums[0];
        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] > max) max = nums[i];
            if (nums[i] < min) min = nums[i];
        }
        return (max, min);
    }
}