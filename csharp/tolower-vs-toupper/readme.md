# 🌳 Sustainable Code - String ToUpper vs. ToLower 📊

A small example of string case methods.

Docs:
- [ToUpper](https://docs.microsoft.com/en-us/dotnet/api/system.string.toupper?WT.mc_id=DT-MVP-5001507)
- [ToUpperInvariant](https://docs.microsoft.com/en-us/dotnet/api/system.string.toupperinvariant?WT.mc_id=DT-MVP-5001507)
- [ToLower](https://docs.microsoft.com/en-us/dotnet/api/system.string.tolower?view=net-6.0?WT.mc_id=DT-MVP-5001507)
- [ToLowerInvariant](https://docs.microsoft.com/en-us/dotnet/api/system.string.tolowerinvariant?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method           | Runtime  | Mean      | StdDev    | Ratio | Gen0   | Allocated |
|----------------- |--------- |----------:|----------:|------:|-------:|----------:|
| ToUpper          | .NET 7.0 | 19.333 ns | 0.0588 ns |  1.58 | 0.0076 |     128 B |
| ToUpper          | .NET 8.0 | 12.585 ns | 0.0661 ns |  1.03 | 0.0076 |     128 B |
| ToUpper          | .NET 9.0 | 12.199 ns | 0.0653 ns |  1.00 | 0.0076 |     128 B |
|                  |          |           |           |       |        |           |
| ToUpperInvariant | .NET 7.0 | 15.922 ns | 0.0472 ns |  1.60 | 0.0076 |     128 B |
| ToUpperInvariant | .NET 8.0 | 10.102 ns | 0.0559 ns |  1.01 | 0.0076 |     128 B |
| ToUpperInvariant | .NET 9.0 |  9.984 ns | 0.1966 ns |  1.00 | 0.0076 |     128 B |
|                  |          |           |           |       |        |           |
| ToLower          | .NET 7.0 | 19.161 ns | 0.0412 ns |  1.54 | 0.0076 |     128 B |
| ToLower          | .NET 8.0 | 12.688 ns | 0.0639 ns |  1.02 | 0.0076 |     128 B |
| ToLower          | .NET 9.0 | 12.473 ns | 0.2151 ns |  1.00 | 0.0076 |     128 B |
|                  |          |           |           |       |        |           |
| ToLowerInvariant | .NET 7.0 | 16.046 ns | 0.0986 ns |  1.58 | 0.0076 |     128 B |
| ToLowerInvariant | .NET 8.0 | 10.173 ns | 0.0555 ns |  1.00 | 0.0076 |     128 B |
| ToLowerInvariant | .NET 9.0 | 10.142 ns | 0.1757 ns |  1.00 | 0.0076 |     128 B |
```

## 🏁 Results

- 🚀 Culuture Invariant-methods are faster, and in most use cases general more correct because invariant casing rules are applied.
- All tests are about 30% faster in .NET 8 than in .NET 7.

## Remarks

- Do not use a specific case by performance first. The use case is more important!
- With `ToUpper` and `ToLower` pay attention to the fact that the respective language and the character set can affect the results!

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
