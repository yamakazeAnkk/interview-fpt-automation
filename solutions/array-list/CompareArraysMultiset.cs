// Compare Arrays as Multisets
// Time: O(n log n), Space: O(n)

public class Solution {
    // Using sorting
    public bool ArraysEqualMultiset(int[] nums1, int[] nums2) {
        if (nums1.Length != nums2.Length) return false;
        
        Array.Sort(nums1);
        Array.Sort(nums2);
        return nums1.SequenceEqual(nums2);
    }
    
    // Using frequency count
    public bool ArraysEqualMultisetCount(int[] nums1, int[] nums2) {
        if (nums1.Length != nums2.Length) return false;
        
        var freq1 = nums1.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        var freq2 = nums2.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        
        return freq1.Count == freq2.Count && 
               freq1.All(kv => freq2.GetValueOrDefault(kv.Key, 0) == kv.Value);
    }
}
