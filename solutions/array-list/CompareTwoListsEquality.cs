// Compare Two Lists Equality
// Time: O(n), Space: O(1)

public class Solution {
    // Order-sensitive comparison
    public bool ListsEqualOrderSensitive<T>(List<T> list1, List<T> list2) {
        return list1.SequenceEqual(list2);
    }
    
    // Order-insensitive comparison (sets)
    public bool ListsEqualOrderInsensitive<T>(List<T> list1, List<T> list2) {
        return list1.ToHashSet().SetEquals(list2.ToHashSet());
    }
    
    // Custom equality comparer
    public bool ListsEqualCustom<T>(List<T> list1, List<T> list2, IEqualityComparer<T> comparer) {
        return list1.ToHashSet(comparer).SetEquals(list2.ToHashSet(comparer));
    }
}