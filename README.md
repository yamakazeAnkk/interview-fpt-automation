## Tài liệu phỏng vấn (C#)

### Mục lục
- **C# One-Line Concepts**
- **C# Dictionary (tương đương Java HashMap) – Interview Q&A**
- **Giải thuật phổ biến – phiên bản C# đến Automation Testing **

---

### C# One-Line Concepts
Tập hợp các câu lệnh ngắn gọn giúp ghi nhớ nhanh các khái niệm tương đương phần "JAVA ONE LINE CONCEPTS" nhưng viết bằng C#.

- **Khai báo biến**: `int x = 42; var y = "hello";`
- **String interpolation**: `var s = $"Name={name}, Age={age}";`
- **Null-coalescing**: `var v = maybe ?? defaultValue;`
- **Null-conditional**: `var len = str?.Length;`
- **Ternary**: `var abs = n >= 0 ? n : -n;`
- **Lambda**: `Func<int,int> sq = x => x * x;`
- **List nhanh**: `var list = new List<int>{1,2,3};`
- **Dictionary nhanh**: `var map = new Dictionary<string,int>{{"a",1},{"b",2}};`
- **LINQ chọn lọc**: `var evens = nums.Where(n => n % 2 == 0).ToList();`
- **Sắp xếp**: `var sorted = nums.OrderBy(n => n).ToList();`
- **Distinct**: `var uniq = nums.Distinct().ToList();`
- **Aggregate/Sum**: `var sum = nums.Sum(); var prod = nums.Aggregate(1,(a,b)=>a*b);`
- **Range**: `var range = Enumerable.Range(1,10).ToList();`
- **Read only property**: `public int Count { get; }`
- **Expression-bodied member**: `public int Area => W * H;`
- **Record (C# 9+)**: `public record User(string Name,int Age);`
- **Tuples**: `var (a,b) = (1, "x");`
- **Switch expression**: `var sign = n switch { >0 => "+", <0 => "-", _ => "0" };`
- **Pattern matching**: `if (obj is string t && t.Length>0) { ... }`
- **Async/await**: `var data = await http.GetStringAsync(url);`
- **File IO nhanh**: `var lines = File.ReadAllLines(path);`
- **TryParse**: `if (int.TryParse(s, out var val)) { ... }`
- **Readonly struct**: `public readonly struct Point(int X,int Y);`
- **Init-only setter**: `public string Name { get; init; }`

Ghi chú: Sử dụng `var` khi biến có thể suy luận kiểu rõ ràng, và ưu tiên LINQ cho các thao tác tập hợp.

---

### C# Dictionary (HashMap) – Interview Q&A

1) **Khác biệt `Dictionary<TKey,TValue>` vs `ConcurrentDictionary<TKey,TValue>`?**
- Dictionary không thread-safe; ConcurrentDictionary an toàn trong môi trường đa luồng với cơ chế lock tinh vi.

2) **Độ phức tạp trung bình của get/put?**
- Trung bình O(1); tệ nhất O(n) khi nhiều va chạm hash dẫn tới chuỗi/cluster.

3) **Điều gì xảy ra khi load factor tăng?**
- Bảng băm tự mở rộng (resize) và rehash, tốn chi phí tạm thời. Nên cấu hình capacity ban đầu nếu biết trước kích thước.

4) **Key yêu cầu gì?**
- Cần `GetHashCode()` và `Equals()` nhất quán. Tránh key mutable vì thay đổi state sẽ làm sai lệch vị trí băm.

5) **So sánh `Dictionary` vs `SortedDictionary` vs `SortedList`?**
- Dictionary: tra cứu O(1) trung bình, không có thứ tự. SortedDictionary: cây đỏ-đen, O(log n), có thứ tự key. SortedList: mảng có sắp xếp, tra cứu O(log n), chèn xóa tốn O(n).

6) **Xử lý va chạm (collision) ra sao?**
- .NET dùng mảng bucket với chain; các mục có hash trùng nằm trong cùng bucket, duyệt qua liên kết/entry.

7) **Iterate trong khi sửa đổi?**
- Sửa đổi cấu trúc trong lúc iterate sẽ ném `InvalidOperationException`. Dùng snapshot (ToList) hoặc dùng `ConcurrentDictionary` với APIs phù hợp.

8) **Cách đếm tần suất phần tử nhanh?**
```csharp
var freq = new Dictionary<int,int>();
foreach (var x in nums)
    freq[x] = freq.TryGetValue(x, out var c) ? c + 1 : 1;
```

9) **Tra cứu có điều kiện/khởi tạo mặc định?**
```csharp
var val = map.TryGetValue(key, out var v) ? v : defaultVal;
```

10) **Duyệt theo key tăng dần?**
- Dùng `SortedDictionary<TKey,TValue>` hoặc `OrderBy(kv => kv.Key)` trên Dictionary.

11) **So sánh reference type key theo tham chiếu hay giá trị?**
- Theo `Equals`/`GetHashCode` của key. Ghi đè đúng chuẩn hoặc cung cấp `IEqualityComparer<TKey>` tùy biến.

12) **Khóa đồng thời khi cập nhật giá trị?**
- Dùng `lock` bên ngoài hoặc `ConcurrentDictionary.AddOrUpdate/GetOrAdd` khi đa luồng.

Ví dụ ngắn:
```csharp
var map = new Dictionary<string,int>();
map["a"] = 1;
map["b"] = map.GetValueOrDefault("b", 0) + 1; // .NET 8+
if (map.TryGetValue("a", out var va)) { /* dùng va */ }
foreach (var (k,v) in map) { /* iterate */ }
```

---

### Giải thuật phổ biến – phiên bản C#

- **Two Sum (O(n))**
```csharp
int[] TwoSum(int[] nums, int target) {
    var idx = new Dictionary<int,int>();
    for (int i = 0; i < nums.Length; i++) {
        int need = target - nums[i];
        if (idx.TryGetValue(need, out var j)) return new[]{j, i};
        idx[nums[i]] = i;
    }
    return Array.Empty<int>();
}
```

- **Anagram Check (O(n))**
```csharp
bool IsAnagram(string a, string b) {
    if (a.Length != b.Length) return false;
    var cnt = new int[26];
    foreach (var ch in a) cnt[ch - 'a']++;
    foreach (var ch in b) if (--cnt[ch - 'a'] < 0) return false;
    return true;
}
```

- **Top K Frequent (O(n log k))**
```csharp
IList<int> TopKFrequent(int[] nums, int k) {
    var freq = nums.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
    return freq.OrderByDescending(kv => kv.Value).Take(k).Select(kv => kv.Key).ToList();
}
```

- **Two Pointers – Remove Duplicates (sorted array)**
```csharp
int RemoveDuplicates(int[] nums) {
    if (nums.Length == 0) return 0;
    int w = 1;
    for (int r = 1; r < nums.Length; r++)
        if (nums[r] != nums[r-1]) nums[w++] = nums[r];
    return w;
}
```

- **Sliding Window – Longest Substring Without Repeat**
```csharp
int LengthOfLongestSubstring(string s) {
    var last = new Dictionary<char,int>();
    int best = 0, left = 0;
    for (int r = 0; r < s.Length; r++) {
        if (last.TryGetValue(s[r], out var p)) left = Math.Max(left, p + 1);
        last[s[r]] = r;
        best = Math.Max(best, r - left + 1);
    }
    return best;
}
```

- **Binary Search (template)**
```csharp
int LowerBound(int[] a, int x) {
    int l = 0, r = a.Length; // [l, r)
    while (l < r) {
        int m = l + (r - l)/2;
        if (a[m] < x) l = m + 1; else r = m;
    }
    return l;
}
```

- **BFS trên ma trận (grid shortest path)**
```csharp
int ShortestPath(int[][] grid, (int r,int c) src, (int r,int c) dst) {
    int R = grid.Length, C = grid[0].Length;
    var q = new Queue<(int r,int c,int d)>();
    var seen = new bool[R,C];
    q.Enqueue((src.r, src.c, 0));
    seen[src.r, src.c] = true;
    int[] dr = {1,-1,0,0}, dc = {0,0,1,-1};
    while (q.Count>0) {
        var (r,c,d) = q.Dequeue();
        if (r==dst.r && c==dst.c) return d;
        for (int k=0;k<4;k++) {
            int nr=r+dr[k], nc=c+dc[k];
            if (nr>=0 && nr<R && nc>=0 && nc<C && grid[nr][nc]==0 && !seen[nr,nc]) {
                seen[nr,nc]=true; q.Enqueue((nr,nc,d+1));
            }
        }
    }
    return -1;
}
```

- **DFS – số thành phần liên thông trong đồ thị vô hướng**
```csharp
int CountComponents(int n, int[][] edges) {
    var g = Enumerable.Range(0,n).ToDictionary(i => i, _ => new List<int>());
    foreach (var e in edges) { g[e[0]].Add(e[1]); g[e[1]].Add(e[0]); }
    var seen = new bool[n];
    int comp = 0;
    void Dfs(int u) {
        var st = new Stack<int>(); st.Push(u); seen[u]=true;
        while (st.Count>0) {
            int v = st.Pop();
            foreach (var w in g[v]) if (!seen[w]) { seen[w]=true; st.Push(w);} 
        }
    }
    for (int i=0;i<n;i++) if (!seen[i]) { comp++; Dfs(i);} 
    return comp;
}
```

- **Heap – K phần tử nhỏ nhất**
```csharp
IList<int> KSmallest(int[] nums, int k) {
    var pq = new PriorityQueue<int,int>(); // max-heap bằng cách đảo priority
    foreach (var x in nums) {
        pq.Enqueue(x, -x);
        if (pq.Count > k) pq.Dequeue();
    }
    var res = new List<int>();
    while (pq.Count>0) res.Add(pq.Dequeue());
    return res;
}
```

- **Prefix Sum – số lượng subarray có tổng = k**
```csharp
int SubarraySum(int[] nums, int k) {
    var count = new Dictionary<int,int>{{0,1}};
    int sum = 0, ans = 0;
    foreach (var x in nums) {
        sum += x;
        ans += count.GetValueOrDefault(sum - k, 0);
        count[sum] = count.GetValueOrDefault(sum, 0) + 1;
    }
    return ans;
}
```

Ghi chú: Mỗi giải thuật được tối ưu về độ phức tạp thời gian/bộ nhớ và dùng các cấu trúc dữ liệu chuẩn của .NET.



---

### Câu hỏi phỏng vấn: từ String One-Line đến Abstraction (C#)

Lưu ý: Danh sách chỉ nêu câu hỏi (không kèm đáp án) để bạn tự ôn luyện. Nội dung bám theo mạch chủ đề từ String one-line concepts tới OOP Abstraction trong C# (tương đương phần Java trong tài liệu gốc).

#### String (one-line concepts, API căn bản)
- Sự khác nhau giữa `string`, `String`, và `StringBuilder` trong C# là gì?
- Vì sao `string` là immutable? Hệ quả với hiệu năng khi nối chuỗi?
- Khi nào dùng `StringBuilder` thay vì toán tử `+` hoặc `$"..."`?
- So sánh `string.Equals`, `==`, và so sánh theo `StringComparison`.
- Cách xử lý culture/locale khi so sánh, sắp xếp chuỗi (`CultureInfo`, `StringComparer`).
- Khác biệt `ToString()` vs `string.Format` vs interpolation `$"..."`.
- Tìm kiếm/chia tách: `IndexOf`, `Contains`, `Split`, `Substring` – độ phức tạp và corner cases.
- Xóa/ký tự trắng: `Trim`, `TrimStart`, `TrimEnd` và khác biệt Unicode whitespace.
- Chuyển đổi hoa/thường: `ToUpper`, `ToLower` và chú ý culture invariant.
- Regex cơ bản với `System.Text.RegularExpressions` – khi nào nên/không nên dùng?

#### Collections/Generics nền tảng
- Khác biệt `List<T>`, `LinkedList<T>`, `Queue<T>`, `Stack<T>` về độ phức tạp và use-cases.
- Khi nào dùng `HashSet<T>` so với `List<T>`? Điều kiện để `HashSet` hoạt động đúng.
- Sự khác nhau giữa `Dictionary<TKey,TValue>` và `ConcurrentDictionary<TKey,TValue>` trong bối cảnh đa luồng.
- Cách chọn `SortedDictionary` vs `SortedList` vs `Dictionary` theo đặc tính truy cập/chèn/xóa.
- Vai trò của `IEnumerable<T>` vs `IQueryable<T>` vs `ICollection<T>`.

#### LINQ (one-liners điển hình)
- Giải thích các toán tử LINQ phổ biến: `Where`, `Select`, `SelectMany`, `OrderBy`, `GroupBy`, `Distinct`, `Any`, `All`, `First/Single/Last` (và các biến thể `OrDefault`).
- Sự khác nhau giữa truy vấn deferred và immediate trong LINQ.
- Tác động của `ToList()`/`ToArray()` đến hiệu năng và bộ nhớ.
- Khi nào dùng biểu thức truy vấn (query syntax) vs method syntax.

#### Nullability, Exceptions, Parsing
- Dùng `??`, `?.`, `??=` để đơn giản hóa xử lý null như thế nào?
- Sự khác biệt giữa `TryParse` và `Parse`. Khi nào nên dùng mỗi cái?
- Thực hành tốt khi xử lý exception; khi nào nên bắt và khi nào để ném ra?

#### OOP Core (Encapsulation, Inheritance, Polymorphism)
- Định nghĩa và ví dụ ngắn về Encapsulation trong C#.
- Inheritance: `virtual`, `override`, `sealed` – ý nghĩa và trường hợp sử dụng.
- Polymorphism: runtime vs compile-time; phương thức ẩn `new` vs override.
- Interface vs Abstract class: tiêu chí lựa chọn trong thiết kế.
- Record vs class: khác biệt về equality, immutability mặc định và use-cases.

#### Abstraction
- Định nghĩa Abstraction trong C# và lợi ích thiết kế.
- Ví dụ tạo abstraction cho kho dữ liệu: `IRepository<T>` và các triển khai khác nhau.
- Áp dụng Dependency Inversion với Abstraction để dễ test/mock.
- Khi nào không nên trừu tượng hóa quá mức? Dấu hiệu over-engineering.

---

### Câu hỏi thao tác Array/List (C#) – dạng liệt kê ngắn

- How to find max/min of an unsorted array? // tìm lớn/nhỏ nhất trong mảng chưa sắp xếp (duyệt 1 lần)
- How to reverse an array? How to reverse a `List<T>`? // đảo ngược mảng/danh sách in-place hoặc dùng API có sẵn
- How to remove duplicate elements from a list? (Hint: `HashSet<T>`/`Distinct()`) // khử trùng lặp bằng set hoặc LINQ
- How to maintain insertion order while removing duplicates? (Hint: `Distinct()` giữ thứ tự gặp đầu tiên) // giữ thứ tự ban đầu
- How to compare two lists for equality (order-sensitive vs order-insensitive)? // so sánh theo thứ tự hoặc theo tập hợp/phân bố phần tử
- How to check two arrays contain the same multiset of elements? (Hint: sort or frequency map) // so sánh đa tập bằng sort hoặc đếm tần suất
- How to rotate an array k steps (right/left)? // xoay vòng k bước, chú ý k % n
- How to move all zeros to the end while keeping order of non-zeros? // hai con trỏ, ổn định thứ tự
- How to find second largest element without sorting? // theo dõi max1, max2 khi duyệt
- How to merge two sorted arrays into one sorted array? // trộn hai con trỏ
- How to find intersection/union/difference of two arrays/lists? // dùng set hoặc sort + hai con trỏ
- How to remove elements while iterating a `List<T>` safely? // duyệt ngược, hoặc lọc tạo danh sách mới
- How to find subarray with given sum `k` (positive vs có số âm)? // two pointers cho dương, prefix-sum + map cho có âm
- How to find longest increasing subsequence (LIS) length? // O(n log n) với mảng tail
- How to find duplicate numbers in an array of range-limited integers? // đánh dấu/đếm hoặc set
- How to detect a cycle using fast/slow pointers (in arrays modeled as next-index)? // Floyd’s tortoise and hare
- How to implement binary search and find first/last occurrence? // biến thể lower_bound/upper_bound
- How to partition array by predicate (e.g., even before odd) in-place? // hai con trỏ/partition
- How to group items by key (frequency count) efficiently? // từ điển đếm tần suất
- How to stable-sort a list by multiple keys (thenBy)? // sort theo nhiều khóa ổn định
- How to shuffle an array uniformly (Fisher–Yates)? // hoán đổi từ cuối xuống đầu với random
- How to sample k items uniformly without replacement? // reservoir sampling
- How to chunk/split a list into fixed-size batches? // chia thành lô kích thước cố định
- How to flatten a nested list structure (one level vs deep)? // SelectMany hoặc DFS
- How to deduplicate objects using custom equality (`IEqualityComparer<T>`) in a set/list? // so sánh tùy biến
- How to find k smallest/largest elements efficiently (heap vs sort)? // heap kích thước k hoặc sort rồi lấy đầu/cuối
- How to compute running prefix sums and use them for range queries? // mảng prefix để truy vấn nhanh
- How to window over a list (sliding window) to compute moving average/max? // cửa sổ trượt; deque cho max trượt
- How to remove duplicates while preserving last occurrence? // duyệt ngược + set đã thấy
- How to pick the majority element (Boyer–Moore)? // thuật toán Boyer–Moore O(1) bộ nhớ
