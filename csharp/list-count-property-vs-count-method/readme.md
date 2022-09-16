# 🌳 Sustainable Code - Count() vs Count 📊

These snippets show the basic behavior of [Count()](https://docs.microsoft.com/dotnet/api/system.linq.enumerable.count?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Count](https://docs.microsoft.com/dotnet/api/system.collections.icollection.count?view=net-6.0&WT.mc_id=DT-MVP-5001507).
```

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100-rc.1.22431.12
  [Host]   : .NET 7.0.0 (7.0.22.42610), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.0 (7.0.22.42610), X64 RyuJIT AVX2


|                 Method |  Runtime | Categories | Count |      Mean |
|----------------------- |--------- |----------- |------ |----------:|
| HashSet_Count_Property | .NET 6.0 |    HashSet |  1000 | 0.0169 ns |
|   HashSet_Count_Method | .NET 6.0 |    HashSet |  1000 | 2.3655 ns |
|                        |          |            |       |           |
| HashSet_Count_Property | .NET 7.0 |    HashSet |  1000 | 0.0046 ns |
|   HashSet_Count_Method | .NET 7.0 |    HashSet |  1000 | 2.4629 ns |
|                        |          |            |       |           |
| HashSet_Count_Property | .NET 6.0 |    HashSet | 10000 | 0.0123 ns |
|   HashSet_Count_Method | .NET 6.0 |    HashSet | 10000 | 2.3504 ns |
|                        |          |            |       |           |
| HashSet_Count_Property | .NET 7.0 |    HashSet | 10000 | 0.0049 ns |
|   HashSet_Count_Method | .NET 7.0 |    HashSet | 10000 | 2.0832 ns |
|                        |          |            |       |           |
|    List_Count_Property | .NET 6.0 |       List |  1000 | 0.0136 ns |
|      List_Count_Method | .NET 6.0 |       List |  1000 | 2.3448 ns |
|                        |          |            |       |           |
|    List_Count_Property | .NET 7.0 |       List |  1000 | 0.0067 ns |
|      List_Count_Method | .NET 7.0 |       List |  1000 | 2.4606 ns |
|                        |          |            |       |           |
|    List_Count_Property | .NET 6.0 |       List | 10000 | 0.0319 ns |
|      List_Count_Method | .NET 6.0 |       List | 10000 | 2.3814 ns |
|                        |          |            |       |           |
|    List_Count_Property | .NET 7.0 |       List | 10000 | 0.0015 ns |
|      List_Count_Method | .NET 7.0 |       List | 10000 | 2.1750 ns |

*Some columns of the output were removed*

```



## 🏁 Results

- The difference is clear: accessing the `Count` property is much faster - and does not change even if the list contains more entries.
- Thus, when a [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) state exists, the property is always faster (>100x).
- Comparing .NET 6 to .NET 7 you can see a huge performance jump [Performance Improvements in .NET 7](https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/?WT.mc_id=DT-MVP-5001507)

## Remarks

- While the property is information that is available immediately (behind a field), `Count()` causes the enumerator to go over all the elements.
- However, Count() is [now implemented to check if the underlying collection is materialized](https://github.com/dotnet/runtime/blob/4cf1383c8458945b7eb27ae5f57338c10ed25d54/src/libraries/System.Linq/src/System/Linq/Count.cs#L11) and has a `Count` property (`ICollection`), so in the best case the enumerator does not need to be called. Therefore, the time does not increase if the materialized lists have more entries. But the overhead of calling the Count() method still remains.
- The general use of `Count()` is therefore not advisable, if you have `Count` and don't need additional capabilities of Count (e.g. Linq Filter).

## ⌨️ Run this sample

```shell
dotnet run -c Release -f net7.0 --runtimes net6.0 net7.0
```

This benchmark takes ~10mins to run on my machine.
