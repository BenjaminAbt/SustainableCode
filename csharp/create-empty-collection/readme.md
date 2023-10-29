# 🌳 Sustainable Code - Create Empty Collections 📊

A small example of how an empty collection can be created efficiently.

Docs:
- [Enumerable.Empty<TResult> Method](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.empty?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- Source Code of [Array.Empty](https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/mscorlib/system/array.cs#L3080)
- Source Code of [Enumerable.Empty](https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/System.Core/System/Linq/Enumerable.cs#L2147)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method          | Job      | Runtime  | Mean      | Error     | StdDev    | Gen0   | Allocated |
|---------------- |--------- |--------- |----------:|----------:|----------:|-------:|----------:|
| List            | .NET 7.0 | .NET 7.0 | 15.012 ns | 0.3203 ns | 0.7486 ns | 0.0043 |      72 B |
| List            | .NET 8.0 | .NET 8.0 |  8.932 ns | 0.1964 ns | 0.1741 ns | 0.0019 |      32 B |
|                 |          |          |           |           |           |        |           |
| Array           | .NET 7.0 | .NET 7.0 |  9.971 ns | 0.1997 ns | 0.1868 ns | 0.0014 |      24 B |
| Array           | .NET 8.0 | .NET 8.0 |  8.507 ns | 0.1887 ns | 0.2098 ns | 0.0014 |      24 B |
|                 |          |          |           |           |           |        |           |
| ArrayEmpty      | .NET 7.0 | .NET 7.0 |  7.033 ns | 0.0773 ns | 0.0604 ns |      - |         - |
| ArrayEmpty      | .NET 8.0 | .NET 8.0 |  6.069 ns | 0.1249 ns | 0.1169 ns |      - |         - |
|                 |          |          |           |           |           |        |           |
| EnumerableEmpty | .NET 7.0 | .NET 7.0 |  3.844 ns | 0.0942 ns | 0.0881 ns |      - |         - |
| EnumerableEmpty | .NET 8.0 | .NET 8.0 |  1.935 ns | 0.0260 ns | 0.0203 ns |      - |         - |
```

## 🏁 Results

- 🚀 The most efficient way to create an empty collection is `Enumerable.Empty`. Here no allocation is necessary, because no object is created.

## Remarks

- Using `List` is by far the most inefficient way, because under the hood an array is created, which in turn has 4 elements by default.
- Creating an empty array is better, but it still creates an object unnecessarily and thus requires an allocation.
- You can use `Array.Empty` for all array-based collections (like `ICollection`), which has the same static object implementation like `Enumerable.Empty`.
- All use cases became significantly faster in .NET 8, with the `List` case also seeing a reduction in allocation.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
