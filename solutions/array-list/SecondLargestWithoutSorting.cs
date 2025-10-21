// Second Largest Without Sorting
// Time: O(n), Space: O(1)

public class Solution {
    public int FindSecondLargest(int[] nums) {
        if (nums.Length < 2) throw new ArgumentException("Array too small");
        
        int max1 = Math.Max(nums[0], nums[1]);
        int max2 = Math.Min(nums[0], nums[1]);
        
        for (int i = 2; i < nums.Length; i++) {
            if (nums[i] > max1) {
                max2 = max1;
                max1 = nums[i];
            } else if (nums[i] > max2 && nums[i] != max1) {
                max2 = nums[i];
            }
        }
        
        return max2;
    }
    
    // Handle duplicates properly
    public int FindSecondLargestWithDuplicates(int[] nums) {
        if (nums.Length < 2) throw new ArgumentException("Array too small");
        
        int max1 = int.MinValue, max2 = int.MinValue;
        
        foreach (int num in nums) {
            if (num > max1) {
                max2 = max1;
                max1 = num;
            } else if (num > max2 && num < max1) {
                max2 = num;
            }
        }
        
        return max2 == int.MinValue ? -1 : max2;
    }
}
