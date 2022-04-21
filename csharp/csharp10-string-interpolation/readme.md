# 🌳 Sustainable Code - String Interpolation with C# 10 📊

This small example should show the new string interpolation features with .NET 6 and C# 10

Docs:
- [String Interpolation in C# 10 and .NET 6](https://devblogs.microsoft.com/dotnet/string-interpolation-in-c-10-and-net-6/?WT.mc_id=DT-MVP-5001507)
- [stackalloc](https://docs.microsoft.com/de-de/dotnet/csharp/language-reference/operators/stackalloc?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.300-preview.22204.3
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


|       Method |     Mean |    Error |   StdDev |  Gen 0 | Allocated |
|------------- |---------:|---------:|---------:|-------:|----------:|
|       String | 36.41 ns | 0.142 ns | 0.126 ns | 0.0033 |      56 B |
| StringCreate | 23.10 ns | 0.267 ns | 0.250 ns | 0.0033 |      56 B |
```



## 🏁 Results

- 🔋 Both have the same allocation stats
- 🚀 The new `string.Create` interpolation feature is ~30% faster!

## Remarks

- The `string.Create` API is quite hard to understand
- `stackalloc` is still safe during runtime, but the size must be checked!

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark runs several minutes (0:41 min on my workstation)
