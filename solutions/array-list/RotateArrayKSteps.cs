// Rotate Array k Steps
// Time: O(n), Space: O(1)

public class Solution {
    // Right rotation
    public void RotateRight(int[] nums, int k) {
        int n = nums.Length;
        k %= n;
        if (k == 0) return;
        
        Reverse(nums, 0, n - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, n - 1);
    }
    
    // Left rotation
    public void RotateLeft(int[] nums, int k) {
        int n = nums.Length;
        k %= n;
        if (k == 0) return;
        
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, n - 1);
        Reverse(nums, 0, n - 1);
    }
    
    private void Reverse(int[] nums, int start, int end) {
        while (start < end) {
            (nums[start], nums[end]) = (nums[end], nums[start]);
            start++;
            end--;
        }
    }
}