// Reverse Array and List<T>
// Time: O(n), Space: O(1) for in-place

public class Solution {
    // In-place array reversal
    public void ReverseArray(int[] nums) {
        for (int i = 0, j = nums.Length - 1; i < j; i++, j--) {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
    }
    
    // List<T> reversal
    public void ReverseList<T>(List<T> list) {
        for (int i = 0, j = list.Count - 1; i < j; i++, j--) {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    
    // Using LINQ (creates new list)
    public List<T> ReverseListLINQ<T>(List<T> list) => list.AsEnumerable().Reverse().ToList();
}