# 🌳 Sustainable Code - String ToUpper vs. ToLower 📊

A small example of string case methods.

Docs:
- [ToUpper](https://docs.microsoft.com/en-us/dotnet/api/system.string.toupper?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [ToUpperInvariant](https://docs.microsoft.com/en-us/dotnet/api/system.string.toupperinvariant?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [ToLower](https://docs.microsoft.com/en-us/dotnet/api/system.string.tolower?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [ToLowerInvariant](https://docs.microsoft.com/en-us/dotnet/api/system.string.tolowerinvariant?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method           | Job      | Runtime  | Mean     | Error    | StdDev   | Gen0   | Allocated |
|----------------- |--------- |--------- |---------:|---------:|---------:|-------:|----------:|
| ToUpperInvariant | .NET 7.0 | .NET 7.0 | 25.55 ns | 0.545 ns | 1.161 ns | 0.0076 |     128 B |
| ToUpperInvariant | .NET 8.0 | .NET 8.0 | 19.28 ns | 0.463 ns | 1.366 ns | 0.0076 |     128 B |
|                  |          |          |          |          |          |        |           |
| ToLower          | .NET 7.0 | .NET 7.0 | 30.91 ns | 0.660 ns | 1.420 ns | 0.0076 |     128 B |
| ToLower          | .NET 8.0 | .NET 8.0 | 19.43 ns | 0.418 ns | 1.002 ns | 0.0076 |     128 B |
|                  |          |          |          |          |          |        |           |
| ToUpper          | .NET 7.0 | .NET 7.0 | 30.04 ns | 0.639 ns | 0.683 ns | 0.0076 |     128 B |
| ToUpper          | .NET 8.0 | .NET 8.0 | 22.03 ns | 0.482 ns | 1.108 ns | 0.0076 |     128 B |
|                  |          |          |          |          |          |        |           |
| ToLowerInvariant | .NET 7.0 | .NET 7.0 | 25.72 ns | 0.550 ns | 1.112 ns | 0.0076 |     128 B |
| ToLowerInvariant | .NET 8.0 | .NET 8.0 | 16.41 ns | 0.372 ns | 0.884 ns | 0.0076 |     128 B |
```

## 🏁 Results

- 🚀 Culuture Invariant-methods are faster, and in most use cases general more correct because invariant casing rules are applied.
- All tests are about 30% faster in .NET 8 than in .NET 7.

## Remarks

- Do not use a specific case by performance first. The use case is more important!
- With `ToUpper` and `ToLower` pay attention to the fact that the respective language and the character set can affect the results!

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
