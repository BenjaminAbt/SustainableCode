# 🌳 Sustainable Code - Compiled Expressions 📊

A small example of the performance effects of compiled expressions

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100-rc.1.22431.12
  [Host] : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

Job=InProcess  Toolchain=InProcessEmitToolchain

|              Method |          Mean |        Error |       StdDev |    Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|-------------------- |--------------:|-------------:|-------------:|---------:|--------:|-------:|-------:|----------:|------------:|
|          Expression | 222,655.75 ns | 1,875.419 ns | 1,566.059 ns | 3,461.21 |   37.88 | 0.7324 | 0.2441 |   15144 B |      157.75 |
| Expression_Compiled |      64.37 ns |     0.517 ns |     0.483 ns |     1.00 |    0.00 | 0.0057 |      - |      96 B |        1.00 |

```

## 🏁 Results

- 🚀 The runtime of compiled expressions takes only a fraction of time and allocation. The performance benefit is gigantic (>x3000).

## Remarks

- Compiling an expression costs time itself. Therefore, this is not advisable for all cases.
- Furthermore, `Compile()` is not very fast, but there is a workaround for this with [FastExpressionCompiler](https://github.com/dadhi/FastExpressionCompiler).

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~30secs to run on my machine.
