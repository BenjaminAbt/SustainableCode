# 🌳 Sustainable Code - Any vs Count > 0 📊 

You often see the use of `Any()` to check whether collections contain elements. However, some code analysis tools recommend using the `Count` property for performance reasons. Is this correct?

Yes, lists that are already materialized (HashSet, List..) have the property that already contains how many elements the list has. The enumerator does not have to be triggered, only a comparison is sufficient.
This is many times faster (because O(1)) than `Any()`, which has an additional overhead.

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.4046/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.201
  [Host]   : .NET 7.0.16 (7.0.1624.6629), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX2

Job=.NET 8.0  Runtime=.NET 8.0

| Method | Items | Mean      | Error     | StdDev    | Median    | Ratio | Allocated | Alloc Ratio |
|------- |------ |----------:|----------:|----------:|----------:|------:|----------:|------------:|
| Any    | 0     | 2.2388 ns | 0.0686 ns | 0.0842 ns | 2.1820 ns |  1.00 |         - |          NA |
| Count  | 0     | 0.1936 ns | 0.0214 ns | 0.0200 ns | 0.1843 ns |  0.09 |         - |          NA |
|        |       |           |           |           |           |       |           |             |
| Any    | 1     | 2.2226 ns | 0.0396 ns | 0.0371 ns | 2.2002 ns |  1.00 |         - |          NA |
| Count  | 1     | 0.1668 ns | 0.0284 ns | 0.0266 ns | 0.1574 ns |  0.08 |         - |          NA |
|        |       |           |           |           |           |       |           |             |
| Any    | 10    | 2.2597 ns | 0.0659 ns | 0.0617 ns | 2.2794 ns |  1.00 |         - |          NA |
| Count  | 10    | 0.1898 ns | 0.0199 ns | 0.0187 ns | 0.1924 ns |  0.08 |         - |          NA |
|        |       |           |           |           |           |       |           |             |
| Any    | 100   | 2.2284 ns | 0.0677 ns | 0.0665 ns | 2.1989 ns |  1.00 |         - |          NA |
| Count  | 100   | 0.1625 ns | 0.0108 ns | 0.0096 ns | 0.1594 ns |  0.07 |         - |          NA |
```

## 🏁 Results

The results are very descriptive and very clear

- Count>0 is always 10x faster than Any()


## ⌨️ Run this sample

```shell
dotnet run -c Release
```
