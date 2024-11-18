# 🌳 Sustainable Code - Count() vs Count 📊

These snippets show the basic behavior of [Count()](https://docs.microsoft.com/dotnet/api/system.linq.enumerable.count?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Count](https://docs.microsoft.com/dotnet/api/system.collections.icollection.count?view=net-6.0&WT.mc_id=DT-MVP-5001507).

## 🔥 Benchmark

```shell
| Method                 | Runtime  | Count | Mean      | Error     | StdDev    | Median    |
|----------------------- |--------- |------ |----------:|----------:|----------:|----------:|
| HashSet_Count_Property | .NET 7.0 | 1000  | 0.0063 ns | 0.0014 ns | 0.0012 ns | 0.0061 ns |
| HashSet_Count_Property | .NET 8.0 | 1000  | 0.0065 ns | 0.0004 ns | 0.0003 ns | 0.0064 ns |
| HashSet_Count_Property | .NET 9.0 | 1000  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|                        |          |       |           |           |           |           |
| HashSet_Count_Method   | .NET 7.0 | 1000  | 1.3498 ns | 0.0152 ns | 0.0142 ns | 1.3488 ns |
| HashSet_Count_Method   | .NET 8.0 | 1000  | 1.2249 ns | 0.0090 ns | 0.0084 ns | 1.2222 ns |
| HashSet_Count_Method   | .NET 9.0 | 1000  | 0.3804 ns | 0.0023 ns | 0.0020 ns | 0.3797 ns |
|                        |          |       |           |           |           |           |
| List_Count_Property    | .NET 7.0 | 1000  | 0.0045 ns | 0.0030 ns | 0.0025 ns | 0.0042 ns |
| List_Count_Property    | .NET 8.0 | 1000  | 0.0018 ns | 0.0032 ns | 0.0027 ns | 0.0008 ns |
| List_Count_Property    | .NET 9.0 | 1000  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|                        |          |       |           |           |           |           |
| List_Count_Method      | .NET 7.0 | 1000  | 1.3579 ns | 0.0107 ns | 0.0095 ns | 1.3559 ns |
| List_Count_Method      | .NET 8.0 | 1000  | 1.2283 ns | 0.0059 ns | 0.0052 ns | 1.2295 ns |
| List_Count_Method      | .NET 9.0 | 1000  | 0.3772 ns | 0.0026 ns | 0.0021 ns | 0.3772 ns |
```

## 🏁 Results

- The difference is clear: accessing the `Count` property is much faster - and does not change even if the list contains more entries.
- Thus, when a [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) state exists, the property is always faster (>100x).
- Across all cases, .NET 9 is either equally fast or significantly faster

## Remarks

- While the property is information that is available immediately (behind a field), `Count()` causes the enumerator to go over all the elements.
- However, Count() is [now implemented to check if the underlying collection is materialized](https://github.com/dotnet/runtime/blob/4cf1383c8458945b7eb27ae5f57338c10ed25d54/src/libraries/System.Linq/src/System/Linq/Count.cs#L11) and has a `Count` property (`ICollection`), so in the best case the enumerator does not need to be called. Therefore, the time does not increase if the materialized lists have more entries. But the overhead of calling the Count() method still remains.
- The general use of `Count()` is therefore not advisable, if you have `Count` and don't need additional capabilities of Count (e.g. Linq Filter).

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
