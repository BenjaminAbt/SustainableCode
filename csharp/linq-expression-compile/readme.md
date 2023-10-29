# 🌳 Sustainable Code - Compiled Expressions 📊

A small example of the performance effects of compiled expressions

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method      | Runtime  | Mean          | Error        | StdDev       | Ratio    | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------ |--------- |--------------:|-------------:|-------------:|---------:|-------:|-------:|----------:|------------:|
| Ex          | .NET 7.0 | 234,255.64 ns | 3,023.677 ns | 2,828.350 ns | 3,091.10 | 0.4883 | 0.2441 |   12081 B |      125.84 |
| Ex          | .NET 8.0 | 213,176.49 ns | 1,785.515 ns | 1,582.812 ns | 4,648.00 | 0.4883 | 0.2441 |   12145 B |      126.51 |
|             |          |               |              |              |          |        |        |           |             |
| Ex_Compiled | .NET 7.0 |      75.84 ns |     0.512 ns |     0.454 ns |     1.00 | 0.0057 |      - |      96 B |        1.00 |
| Ex_Compiled | .NET 8.0 |      45.72 ns |     0.910 ns |     0.973 ns |     1.00 | 0.0057 |      - |      96 B |        1.00 |

```

## 🏁 Results

- 🚀 The runtime of compiled expressions takes only a fraction of time and allocation. The performance benefit is gigantic (>x3000).

## Remarks

- Compiling an expression costs time itself. Therefore, this is not advisable for all cases.
- Furthermore, [Compile()](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1.compile?view=net-6.0&WT.mc_id=DT-MVP-5001507) is not very fast, but there is a workaround for this with [FastExpressionCompiler](https://github.com/dadhi/FastExpressionCompiler).
- Compiled bits in .NET 8 are tremendously much faster than in .NET 7

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
