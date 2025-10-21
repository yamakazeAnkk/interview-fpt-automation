// Remove Duplicates from List
// Time: O(n), Space: O(n)

public class Solution {
    // Using HashSet
    public List<T> RemoveDuplicatesHashSet<T>(List<T> list) {
        return new HashSet<T>(list).ToList();
    }
    
    // Using LINQ Distinct
    public List<T> RemoveDuplicatesLINQ<T>(List<T> list) {
        return list.Distinct().ToList();
    }
    
    // Preserving order (first occurrence)
    public List<T> RemoveDuplicatesPreserveOrder<T>(List<T> list) {
        var seen = new HashSet<T>();
        return list.Where(x => seen.Add(x)).ToList();
    }
}