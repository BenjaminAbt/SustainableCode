# 🌳 Sustainable Code - String ToUpper vs. ToLower 📊

A small example of string case methods.

Docs:
- [ToUpper](https://docs.microsoft.com/en-us/dotnet/api/system.string.toupper?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [ToUpperInvariant](https://docs.microsoft.com/en-us/dotnet/api/system.string.toupperinvariant?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [ToLower](https://docs.microsoft.com/en-us/dotnet/api/system.string.tolower?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [ToLowerInvariant](https://docs.microsoft.com/en-us/dotnet/api/system.string.tolowerinvariant?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1706 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


|           Method |     Mean |    Error |   StdDev |  Gen 0 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|----------:|
|          ToUpper | 27.21 ns | 0.538 ns | 0.503 ns | 0.0076 |     128 B |
| ToUpperInvariant | 22.72 ns | 0.466 ns | 0.436 ns | 0.0076 |     128 B |
|          ToLower | 27.66 ns | 0.592 ns | 0.811 ns | 0.0076 |     128 B |
| ToLowerInvariant | 23.00 ns | 0.507 ns | 0.833 ns | 0.0076 |     128 B |

```

## 🏁 Results

- 🚀 Culuture Invariant-methods are faster, and in most use cases general more correct because invariant casing rules are applied.

## Remarks

- Do not use a specific case by performance first. The use case is more important!
- With `ToUpper` and `ToLower` pay attention to the fact that the respective language and the character set can affect the results!

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~1.40mins to run on my machine.
