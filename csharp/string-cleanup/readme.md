# 🌳 Sustainable Code - String Cleanup 📊

Validating and cleaning up strings is a common task in software development. This example shows various ways in which strings can be cleaned up using control chars.

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4412/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100-preview.3.24204.13
  [Host]   : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.17209), X64 RyuJIT AVX2


| Method                 | Runtime  | Mean       | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------- |--------- |-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| StringBuilder_Pool     | .NET 8.0 |   706.2 ns |  1.49 |    0.02 | 0.0744 |      - |   1.22 KB |        1.00 |
| StringBuilder_Instance | .NET 8.0 |   860.0 ns |  1.81 |    0.02 | 0.2270 | 0.0019 |   3.71 KB |        3.04 |
| Linq                   | .NET 8.0 | 1,646.5 ns |  3.47 |    0.03 | 0.0782 |      - |    1.3 KB |        1.07 |
| Span                   | .NET 8.0 |   394.4 ns |  0.83 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
| Span1                  | .NET 8.0 |   474.7 ns |  1.00 |    0.00 | 0.0744 |      - |   1.22 KB |        1.00 |
| Span2                  | .NET 8.0 |   411.8 ns |  0.87 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
| Span2Unsafe            | .NET 8.0 |   375.1 ns |  0.79 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
|                        |          |            |       |         |        |        |           |             |
| StringBuilder_Pool     | .NET 9.0 |   616.3 ns |  1.19 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
| StringBuilder_Instance | .NET 9.0 |   715.9 ns |  1.39 |    0.01 | 0.2270 | 0.0019 |   3.71 KB |        3.04 |
| Linq                   | .NET 9.0 | 1,663.5 ns |  3.22 |    0.02 | 0.0782 |      - |    1.3 KB |        1.07 |
| Span                   | .NET 9.0 |   404.9 ns |  0.78 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
| Span1                  | .NET 9.0 |   516.1 ns |  1.00 |    0.00 | 0.0744 |      - |   1.22 KB |        1.00 |
| Span2                  | .NET 9.0 |   398.9 ns |  0.77 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
| Span2Unsafe            | .NET 9.0 |   389.4 ns |  0.75 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
```

## 🏁 Results

- 🚀 Linq is by far the slowest option
- 🚀 A StringBuilder is more performant in comparison; performance can be further improved with StringBuilder pooling
- 🚀 All Span variants are faster by far; as expected, an unsafe implementation is the most efficient.

## Remarks

- `ReadOnlySpan<char>` would cause the double allocation when converted to `string` via ToString.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Update
- Additional implementation provided by [@gfoidl](https://gist.github.com/gfoidl)
