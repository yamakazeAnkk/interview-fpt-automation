// Remove Duplicates Preserving Insertion Order
// Time: O(n), Space: O(n)

public class Solution {
    // Using HashSet to track seen elements
    public List<T> RemoveDuplicatesPreserveOrder<T>(List<T> list) {
        var seen = new HashSet<T>();
        return list.Where(x => seen.Add(x)).ToList();
    }
    
    // Alternative: using Dictionary to count occurrences
    public List<T> RemoveDuplicatesWithCount<T>(List<T> list) {
        var count = new Dictionary<T, int>();
        return list.Where(x => {
            count[x] = count.GetValueOrDefault(x, 0) + 1;
            return count[x] == 1;
        }).ToList();
    }
}
