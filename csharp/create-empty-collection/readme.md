# 🌳 Sustainable Code - List vs. HashSet 📊

A small example of how an empty collection can be created efficiently.

Docs:
- [Enumerable.Empty<TResult> Method](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.empty?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1706 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


| Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
|------- |----------:|----------:|----------:|-------:|----------:|
|   List | 14.574 ns | 0.2194 ns | 0.2052 ns | 0.0043 |      72 B |
|  Array | 10.163 ns | 0.1851 ns | 0.1732 ns | 0.0014 |      24 B |
|  Empty |  4.739 ns | 0.0956 ns | 0.0894 ns |      - |         - |

```



## 🏁 Results

- 🚀 The most efficient way to create an empty collection is `Enumerable.Empty`. Here no allocation is necessary, because no object is created.

## Remarks

- Using `List` is by far the most inefficient way, because under the hood an array is created, which in turn has 4 elements by default.
- Creating an empty array is better, but it still creates an object unnecessarily and thus requires an allocation.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~3.5mins to run on my machine.
