# 🌳 Sustainable Code - Count() vs Count 📊

These snippets show the basic behavior of [Count()](https://docs.microsoft.com/dotnet/api/system.linq.enumerable.count?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Count](https://docs.microsoft.com/dotnet/api/system.collections.icollection.count?view=net-6.0&WT.mc_id=DT-MVP-5001507).
```

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.1889/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2


|                   Method |  Categories | Count |      Mean |     Error |    StdDev |    Median |  Ratio |
|------------------------- |------------ |------ |----------:|----------:|----------:|----------:|-------:|
|   HashSet_Count_Property |     HashSet |   100 | 0.0185 ns | 0.0124 ns | 0.0110 ns | 0.0145 ns |   1.00 |
|     HashSet_Count_Method |     HashSet |   100 | 2.3573 ns | 0.0370 ns | 0.0346 ns | 2.3338 ns | 171.63 |
|                          |             |       |           |           |           |           |        |
|   HashSet_Count_Property |     HashSet |  1000 | 0.0202 ns | 0.0114 ns | 0.0101 ns | 0.0184 ns |   1.00 |
|     HashSet_Count_Method |     HashSet |  1000 | 2.3606 ns | 0.0294 ns | 0.0246 ns | 2.3512 ns | 137.21 |
|                          |             |       |           |           |           |           |        |
|   HashSet_Count_Property |     HashSet | 10000 | 0.0208 ns | 0.0100 ns | 0.0089 ns | 0.0159 ns |   1.00 |
|     HashSet_Count_Method |     HashSet | 10000 | 2.3480 ns | 0.0043 ns | 0.0034 ns | 2.3475 ns | 129.74 |
|                          |             |       |           |           |           |           |        |
| IEnumerable_Count_Method | IEnumerable |   100 | 5.7990 ns | 0.0768 ns | 0.0681 ns | 5.7593 ns |      ? |
| IEnumerable_Count_Method | IEnumerable |  1000 | 5.7819 ns | 0.0847 ns | 0.0792 ns | 5.7658 ns |      ? |
| IEnumerable_Count_Method | IEnumerable | 10000 | 5.7495 ns | 0.0820 ns | 0.0767 ns | 5.7078 ns |      ? |
|                          |             |       |           |           |           |           |        |
|      List_Count_Property |        List |   100 | 0.0101 ns | 0.0109 ns | 0.0102 ns | 0.0047 ns |      ? |
|        List_Count_Method |        List |   100 | 2.3632 ns | 0.0353 ns | 0.0276 ns | 2.3543 ns |      ? |
|                          |             |       |           |           |           |           |        |
|      List_Count_Property |        List |  1000 | 0.0180 ns | 0.0111 ns | 0.0104 ns | 0.0232 ns |   1.00 |
|        List_Count_Method |        List |  1000 | 2.3701 ns | 0.0399 ns | 0.0373 ns | 2.3514 ns | 209.29 |
|                          |             |       |           |           |           |           |        |
|      List_Count_Property |        List | 10000 | 0.0125 ns | 0.0134 ns | 0.0126 ns | 0.0054 ns |      ? |
|        List_Count_Method |        List | 10000 | 2.3699 ns | 0.0382 ns | 0.0358 ns | 2.3519 ns |      ? |

```



## 🏁 Results

- The difference is clear: accessing the `Count` property is much faster - and does not change even if the list contains more entries.
- Thus, when a [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) state exists, the property is always faster (>100x).

## Remarks

- While the property is information that is available immediately (behind a field), `Count()` causes the enumerator to go over all the elements.
- However, Count() is [now implemented to check if the underlying collection is materialized](https://github.com/dotnet/runtime/blob/4cf1383c8458945b7eb27ae5f57338c10ed25d54/src/libraries/System.Linq/src/System/Linq/Count.cs#L11) and has a `Count` property (`ICollection`), so in the best case the enumerator does not need to be called. Therefore, the time does not increase if the materialized lists have more entries. But the overhead of calling the Count() method still remains.
- The general use of `Count()` is therefore not advisable, if you have `Count` and don't need additional capabilities of Count (e.g. Linq Filter).

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~10mins to run on my machine.
