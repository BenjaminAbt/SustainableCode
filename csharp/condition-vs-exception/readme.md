# 🌳 Sustainable Code - Condition vs Exception 📊

Often exceptions are used for program flow, although this is not recommended and violates the [guidelines of exceptions](https://learn.microsoft.com/dotnet/standard/exceptions/best-practices-for-exceptions?WT.mc_id=DT-MVP-5001507) in .NET.

Better alternatives like [Result classes](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.identity.signinresult?view=aspnetcore-7.0&WT.mc_id=DT-MVP-5001507) or [OneOf](https://github.com/mcintyre321/OneOf) are disregarded for convenience.

But what is the impact of using exceptions anyway?

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.300-preview.23122.5
  [Host]     : .NET 6.0.15 (6.0.1523.11507), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.15 (6.0.1523.11507), X64 RyuJIT AVX2


|    Method |         Mean |      Error |     StdDev |  Ratio |   Gen0 | Allocated |
|---------- |-------------:|-----------:|-----------:|-------:|-------:|----------:|
| Condition |     8.304 ns |  0.1887 ns |  0.1938 ns |   1.00 |      - |         - |
| Exception | 3,639.148 ns | 63.7049 ns | 56.4728 ns | 440.65 | 0.0076 |     208 B |

```

## 🏁 Results

- 🚀 Controlling logic via exceptions is expensive, inefficient and not smart. Doing away with exceptions is almost 500x faster and more efficient.

## Remarks

- Do not use exceptions to control your logic flow.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~0.32mins to run on my machine.
