# 🌳 Sustainable Code - String Cleanup 📊

Validating and cleaning up strings is a common task in software development. This example shows various ways in which strings can be cleaned up using control chars.

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                 | Runtime  | Mean       | Error    | StdDev  | Ratio | RatioSD | Gen0   | Gen1   | Allocated |
|----------------------- |--------- |-----------:|---------:|--------:|------:|--------:|-------:|-------:|----------:|
| StringBuilder_Pool     | .NET 7.0 |   360.8 ns |  1.15 ns | 1.02 ns |  1.26 |    0.01 | 0.0744 |      - |   1.22 KB |
| StringBuilder_Pool     | .NET 8.0 |   395.2 ns |  2.58 ns | 2.41 ns |  1.38 |    0.01 | 0.0744 |      - |   1.22 KB |
| StringBuilder_Pool     | .NET 9.0 |   286.5 ns |  2.36 ns | 2.09 ns |  1.00 |    0.01 | 0.0744 |      - |   1.22 KB |
|                        |          |            |          |         |       |         |        |        |           |
| StringBuilder_Instance | .NET 7.0 |   541.3 ns |  8.65 ns | 8.10 ns |  1.50 |    0.02 | 0.2270 | 0.0019 |   3.71 KB |
| StringBuilder_Instance | .NET 8.0 |   494.8 ns |  7.85 ns | 7.34 ns |  1.37 |    0.02 | 0.2270 | 0.0019 |   3.71 KB |
| StringBuilder_Instance | .NET 9.0 |   361.7 ns |  1.12 ns | 0.93 ns |  1.00 |    0.00 | 0.2270 | 0.0019 |   3.71 KB |
|                        |          |            |          |         |       |         |        |        |           |
| Linq                   | .NET 7.0 | 3,418.9 ns | 10.44 ns | 9.25 ns |  3.45 |    0.02 | 0.0763 |      - |    1.3 KB |
| Linq                   | .NET 8.0 | 1,103.8 ns |  7.93 ns | 7.03 ns |  1.11 |    0.01 | 0.0782 |      - |    1.3 KB |
| Linq                   | .NET 9.0 |   990.3 ns |  4.88 ns | 4.56 ns |  1.00 |    0.01 | 0.0782 |      - |    1.3 KB |
|                        |          |            |          |         |       |         |        |        |           |
| Span                   | .NET 7.0 |   249.8 ns |  4.97 ns | 4.88 ns |  1.03 |    0.02 | 0.0744 |      - |   1.22 KB |
| Span                   | .NET 8.0 |   251.4 ns |  3.61 ns | 3.37 ns |  1.04 |    0.02 | 0.0744 |      - |   1.22 KB |
| Span                   | .NET 9.0 |   241.7 ns |  2.22 ns | 2.07 ns |  1.00 |    0.01 | 0.0744 |      - |   1.22 KB |
|                        |          |            |          |         |       |         |        |        |           |
| Span1                  | .NET 7.0 |   285.0 ns |  2.78 ns | 2.46 ns |  1.10 |    0.01 | 0.0744 |      - |   1.22 KB |
| Span1                  | .NET 8.0 |   286.1 ns |  3.51 ns | 3.11 ns |  1.10 |    0.02 | 0.0744 |      - |   1.22 KB |
| Span1                  | .NET 9.0 |   259.1 ns |  2.66 ns | 2.49 ns |  1.00 |    0.01 | 0.0744 |      - |   1.22 KB |
|                        |          |            |          |         |       |         |        |        |           |
| Span2                  | .NET 7.0 |   290.2 ns |  3.76 ns | 3.52 ns |  1.23 |    0.02 | 0.0744 |      - |   1.22 KB |
| Span2                  | .NET 8.0 |   276.1 ns |  1.10 ns | 1.03 ns |  1.17 |    0.01 | 0.0744 |      - |   1.22 KB |
| Span2                  | .NET 9.0 |   235.2 ns |  1.53 ns | 1.36 ns |  1.00 |    0.01 | 0.0744 |      - |   1.22 KB |
|                        |          |            |          |         |       |         |        |        |           |
| Span2Unsafe            | .NET 7.0 |   232.2 ns |  2.32 ns | 2.17 ns |  1.06 |    0.01 | 0.0744 |      - |   1.22 KB |
| Span2Unsafe            | .NET 8.0 |   279.1 ns |  1.27 ns | 1.06 ns |  1.27 |    0.01 | 0.0744 |      - |   1.22 KB |
| Span2Unsafe            | .NET 9.0 |   219.0 ns |  1.07 ns | 1.00 ns |  1.00 |    0.01 | 0.0744 |      - |   1.22 KB |
```

## 🏁 Results

- 🚀 Linq is by far the slowest option
- 🚀 A StringBuilder is more performant in comparison; performance can be further improved with StringBuilder pooling
- 🚀 All Span variants are faster by far; as expected, an unsafe implementation is the most efficient.

## Remarks

- `ReadOnlySpan<char>` would cause the double allocation when converted to `string` via ToString.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Update
- Additional implementation provided by [@gfoidl](https://gist.github.com/gfoidl)
- 2024/11 - Add .NET 9
