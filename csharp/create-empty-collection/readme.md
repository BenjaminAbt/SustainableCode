# 🌳 Sustainable Code - Create Empty Collections 📊

A small example of how an empty collection can be created efficiently.

Docs:
- [Enumerable.Empty<TResult> Method](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.empty?WT.mc_id=DT-MVP-5001507)
- Source Code of [Array.Empty](https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/mscorlib/system/array.cs#L3080)
- Source Code of [Enumerable.Empty](https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/System.Core/System/Linq/Enumerable.cs#L2147)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method          | Runtime  | Mean     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |--------- |---------:|----------:|------:|--------:|-------:|----------:|------------:|
| List            | .NET 7.0 | 9.466 ns | 0.1372 ns |  2.20 |    0.05 | 0.0043 |      72 B |        2.25 |
| List            | .NET 8.0 | 6.700 ns | 0.0149 ns |  1.56 |    0.02 | 0.0019 |      32 B |        1.00 |
| List            | .NET 9.0 | 4.309 ns | 0.0699 ns |  1.00 |    0.02 | 0.0019 |      32 B |        1.00 |
|                 |          |          |           |       |         |        |           |             |
| Array           | .NET 7.0 | 6.641 ns | 0.0951 ns |  1.62 |    0.03 | 0.0014 |      24 B |        1.00 |
| Array           | .NET 8.0 | 5.121 ns | 0.0530 ns |  1.25 |    0.02 | 0.0014 |      24 B |        1.00 |
| Array           | .NET 9.0 | 4.089 ns | 0.0311 ns |  1.00 |    0.01 | 0.0014 |      24 B |        1.00 |
|                 |          |          |           |       |         |        |           |             |
| ArrayEmpty      | .NET 7.0 | 4.849 ns | 0.0388 ns |  1.84 |    0.01 |      - |         - |          NA |
| ArrayEmpty      | .NET 8.0 | 3.863 ns | 0.0064 ns |  1.47 |    0.00 |      - |         - |          NA |
| ArrayEmpty      | .NET 9.0 | 2.630 ns | 0.0031 ns |  1.00 |    0.00 |      - |         - |          NA |
|                 |          |          |           |       |         |        |           |             |
| EnumerableEmpty | .NET 7.0 | 2.958 ns | 0.0069 ns |  1.13 |    0.00 |      - |         - |          NA |
| EnumerableEmpty | .NET 8.0 | 1.317 ns | 0.0066 ns |  0.50 |    0.00 |      - |         - |          NA |
| EnumerableEmpty | .NET 9.0 | 2.623 ns | 0.0067 ns |  1.00 |    0.00 |      - |         - |          NA |
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
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
