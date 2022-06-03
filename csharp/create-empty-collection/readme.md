# 🌳 Sustainable Code - Create Empty Collections 📊

A small example of how an empty collection can be created efficiently.

Docs:
- [Enumerable.Empty<TResult> Method](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.empty?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- Source Code of [Array.Empty](https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/mscorlib/system/array.cs#L3080)
- Source Code of [Enumerable.Empty](https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/System.Core/System/Linq/Enumerable.cs#L2147)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1706 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT

|          Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
|---------------- |----------:|----------:|----------:|-------:|----------:|
|            List | 14.112 ns | 0.2331 ns | 0.2775 ns | 0.0043 |      72 B |
|           Array |  9.364 ns | 0.1745 ns | 0.1632 ns | 0.0014 |      24 B |
|      ArrayEmpty |  7.712 ns | 0.0427 ns | 0.0378 ns |      - |         - |
| EnumerableEmpty |  4.690 ns | 0.0252 ns | 0.0223 ns |      - |         - |

```

## 🏁 Results

- 🚀 The most efficient way to create an empty collection is `Enumerable.Empty`. Here no allocation is necessary, because no object is created.

## Remarks

- Using `List` is by far the most inefficient way, because under the hood an array is created, which in turn has 4 elements by default.
- Creating an empty array is better, but it still creates an object unnecessarily and thus requires an allocation.
- You can use `Array.Empty` for all array-based collections (like `ICollection`), which has the same static object implementation like `Enumerable.Empty`

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~1.2mins to run on my machine.

## Updates

- Added `Array.Empty` sample
