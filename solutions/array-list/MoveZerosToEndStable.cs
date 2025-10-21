// Move Zeros to End (Stable)
// Time: O(n), Space: O(1)

public class Solution {
    public void MoveZeroes(int[] nums) {
        int writeIndex = 0;
        
        // Move all non-zero elements to the front
        for (int i = 0; i < nums.Length; i++) {
            if (nums[i] != 0) {
                nums[writeIndex++] = nums[i];
            }
        }
        
        // Fill remaining positions with zeros
        while (writeIndex < nums.Length) {
            nums[writeIndex++] = 0;
        }
    }
    
    // Alternative: Two pointers approach
    public void MoveZeroesTwoPointers(int[] nums) {
        int left = 0, right = 0;
        
        while (right < nums.Length) {
            if (nums[right] != 0) {
                (nums[left], nums[right]) = (nums[right], nums[left]);
                left++;
            }
            right++;
        }
    }
}
