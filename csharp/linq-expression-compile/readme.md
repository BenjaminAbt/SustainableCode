# 🌳 Sustainable Code - Compiled Expressions 📊

A small example of the performance effects of compiled expressions

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method      | Runtime  | Mean          | StdDev       | Ratio    | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------ |--------- |--------------:|-------------:|---------:|--------:|-------:|-------:|----------:|------------:|
| Ex          | .NET 7.0 | 162,346.11 ns | 1,559.010 ns | 5,838.03 |   56.20 | 0.4883 | 0.2441 |   12081 B |      125.84 |
| Ex          | .NET 8.0 | 145,292.36 ns |   766.132 ns | 5,224.77 |   29.67 | 0.4883 | 0.2441 |   12145 B |      126.51 |
| Ex          | .NET 9.0 | 137,245.31 ns |   733.243 ns | 4,935.40 |   28.33 | 0.4883 | 0.2441 |   12113 B |      126.18 |
| Ex_Compiled | .NET 7.0 |      42.31 ns |     0.098 ns |     1.52 |    0.01 | 0.0057 |      - |      96 B |        1.00 |
| Ex_Compiled | .NET 8.0 |      28.94 ns |     0.140 ns |     1.04 |    0.01 | 0.0057 |      - |      96 B |        1.00 |
| Ex_Compiled | .NET 9.0 |      27.81 ns |     0.072 ns |     1.00 |    0.00 | 0.0057 |      - |      96 B |        1.00 |

```

## 🏁 Results

- 🚀 The runtime of compiled expressions takes only a fraction of time and allocation. The performance benefit is gigantic (>x3000).

## Remarks

- Compiling an expression costs time itself. Therefore, this is not advisable for all cases.
- Furthermore, [Compile()](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1.compile?WT.mc_id=DT-MVP-5001507) is not very fast, but there is a workaround for this with [FastExpressionCompiler](https://github.com/dadhi/FastExpressionCompiler).
- Compiled bits in .NET 8 are way faster than in .NET 7

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
