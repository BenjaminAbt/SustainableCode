# 🌳 Sustainable Code - Count() vs Count 📊

These snippets show the basic behavior of [Count()](https://docs.microsoft.com/dotnet/api/system.linq.enumerable.count?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Count](https://docs.microsoft.com/dotnet/api/system.collections.icollection.count?view=net-6.0&WT.mc_id=DT-MVP-5001507).

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method                 | Job      | Runtime  | Categories | Count | Mean      | Error     | StdDev    | Median    | Ratio  | RatioSD |
|----------------------- |--------- |--------- |----------- |------ |----------:|----------:|----------:|----------:|-------:|--------:|
| HashSet_Count_Property | .NET 7.0 | .NET 7.0 | HashSet    | 1000  | 0.0137 ns | 0.0158 ns | 0.0148 ns | 0.0111 ns |      ? |       ? |
| HashSet_Count_Method   | .NET 7.0 | .NET 7.0 | HashSet    | 1000  | 2.0003 ns | 0.0583 ns | 0.0545 ns | 1.9817 ns |      ? |       ? |
|                        |          |          |            |       |           |           |           |           |        |         |
| HashSet_Count_Property | .NET 8.0 | .NET 8.0 | HashSet    | 1000  | 0.0240 ns | 0.0172 ns | 0.0161 ns | 0.0251 ns |   1.00 |    0.00 |
| HashSet_Count_Method   | .NET 8.0 | .NET 8.0 | HashSet    | 1000  | 1.6695 ns | 0.0259 ns | 0.0243 ns | 1.6611 ns | 109.91 |   70.79 |
|                        |          |          |            |       |           |           |           |           |        |         |
| HashSet_Count_Property | .NET 7.0 | .NET 7.0 | HashSet    | 10000 | 0.0282 ns | 0.0184 ns | 0.0173 ns | 0.0195 ns |   1.00 |    0.00 |
| HashSet_Count_Method   | .NET 7.0 | .NET 7.0 | HashSet    | 10000 | 2.2915 ns | 0.0672 ns | 0.0774 ns | 2.3251 ns | 109.94 |   59.14 |
|                        |          |          |            |       |           |           |           |           |        |         |
| HashSet_Count_Property | .NET 8.0 | .NET 8.0 | HashSet    | 10000 | 0.0342 ns | 0.0211 ns | 0.0197 ns | 0.0305 ns |   1.00 |    0.00 |
| HashSet_Count_Method   | .NET 8.0 | .NET 8.0 | HashSet    | 10000 | 1.6984 ns | 0.0415 ns | 0.0388 ns | 1.7006 ns |  71.45 |   47.24 |
|                        |          |          |            |       |           |           |           |           |        |         |
| List_Count_Property    | .NET 7.0 | .NET 7.0 | List       | 1000  | 0.0012 ns | 0.0027 ns | 0.0021 ns | 0.0000 ns |      ? |       ? |
| List_Count_Method      | .NET 7.0 | .NET 7.0 | List       | 1000  | 2.1422 ns | 0.0638 ns | 0.0597 ns | 2.1150 ns |      ? |       ? |
|                        |          |          |            |       |           |           |           |           |        |         |
| List_Count_Property    | .NET 8.0 | .NET 8.0 | List       | 1000  | 0.0001 ns | 0.0005 ns | 0.0005 ns | 0.0000 ns |      ? |       ? |
| List_Count_Method      | .NET 8.0 | .NET 8.0 | List       | 1000  | 1.7115 ns | 0.0502 ns | 0.0469 ns | 1.7149 ns |      ? |       ? |
|                        |          |          |            |       |           |           |           |           |        |         |
| List_Count_Property    | .NET 7.0 | .NET 7.0 | List       | 10000 | 0.0397 ns | 0.0202 ns | 0.0179 ns | 0.0457 ns |   1.00 |    0.00 |
| List_Count_Method      | .NET 7.0 | .NET 7.0 | List       | 10000 | 2.2953 ns | 0.0404 ns | 0.0378 ns | 2.3143 ns |  78.86 |   53.57 |
|                        |          |          |            |       |           |           |           |           |        |         |
| List_Count_Property    | .NET 8.0 | .NET 8.0 | List       | 10000 | 0.0398 ns | 0.0203 ns | 0.0190 ns | 0.0441 ns |   1.00 |    0.00 |
| List_Count_Method      | .NET 8.0 | .NET 8.0 | List       | 10000 | 1.6535 ns | 0.0313 ns | 0.0278 ns | 1.6401 ns |  61.39 |   45.47 |
```
*Some columns of the output were removed*

## 🏁 Results

- The difference is clear: accessing the `Count` property is much faster - and does not change even if the list contains more entries.
- Thus, when a [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) state exists, the property is always faster (>100x).
- Comparing .NET 6 to .NET 7 you can see a huge performance jump [Performance Improvements in .NET 7](https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/?WT.mc_id=DT-MVP-5001507)
- Across all cases, .NET 8 is either equally fast or significantly faster

## Remarks

- While the property is information that is available immediately (behind a field), `Count()` causes the enumerator to go over all the elements.
- However, Count() is [now implemented to check if the underlying collection is materialized](https://github.com/dotnet/runtime/blob/4cf1383c8458945b7eb27ae5f57338c10ed25d54/src/libraries/System.Linq/src/System/Linq/Count.cs#L11) and has a `Count` property (`ICollection`), so in the best case the enumerator does not need to be called. Therefore, the time does not increase if the materialized lists have more entries. But the overhead of calling the Count() method still remains.
- The general use of `Count()` is therefore not advisable, if you have `Count` and don't need additional capabilities of Count (e.g. Linq Filter).

## ⌨️ Run this sample

```shell
dotnet run -c Release -f net7.0 --runtimes net6.0 net7.0
```

## Updates

- 2023/11 - Add .NET 8
