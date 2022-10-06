# 🌳 Sustainable Code - Compiled Expressions 📊

A small example of the performance effects of compiled expressions

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100-rc.1.22431.12
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2


|      Method |          Mean |        Error |       StdDev |    Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|------------ |--------------:|-------------:|-------------:|---------:|--------:|-------:|-------:|----------:|------------:|
|          Ex | 228,941.16 ns | 4,508.268 ns | 4,823.797 ns | 3,464.82 |  104.18 | 0.7324 | 0.2441 |   14817 B |      154.34 |
| Ex_Compiled |      66.10 ns |     1.258 ns |     1.346 ns |     1.00 |    0.00 | 0.0057 |      - |      96 B |        1.00 |

```

## 🏁 Results

- 🚀 The runtime of compiled expressions takes only a fraction of time and allocation. The performance benefit is gigantic (>x3000).

## Remarks

- Compiling an expression costs time itself. Therefore, this is not advisable for all cases.
- Furthermore, [Compile()](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1.compile?view=net-6.0&WT.mc_id=DT-MVP-5001507) is not very fast, but there is a workaround for this with [FastExpressionCompiler](https://github.com/dadhi/FastExpressionCompiler).

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~30secs to run on my machine.
