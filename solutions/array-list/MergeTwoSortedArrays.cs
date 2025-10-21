// Merge Two Sorted Arrays
// Time: O(m + n), Space: O(m + n)

public class Solution {
    public int[] MergeSortedArrays(int[] nums1, int[] nums2) {
        int[] result = new int[nums1.Length + nums2.Length];
        int i = 0, j = 0, k = 0;
        
        while (i < nums1.Length && j < nums2.Length) {
            if (nums1[i] <= nums2[j]) {
                result[k++] = nums1[i++];
            } else {
                result[k++] = nums2[j++];
            }
        }
        
        while (i < nums1.Length) result[k++] = nums1[i++];
        while (j < nums2.Length) result[k++] = nums2[j++];
        
        return result;
    }
    
    // In-place merge (nums1 has enough space)
    public void MergeInPlace(int[] nums1, int m, int[] nums2, int n) {
        int i = m - 1, j = n - 1, k = m + n - 1;
        
        while (i >= 0 && j >= 0) {
            if (nums1[i] > nums2[j]) {
                nums1[k--] = nums1[i--];
            } else {
                nums1[k--] = nums2[j--];
            }
        }
        
        while (j >= 0) nums1[k--] = nums2[j--];
    }
}
