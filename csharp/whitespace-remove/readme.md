# 🌳 Sustainable Code - String Whitespace Remove 📊

There are various ways to remove spaces from text, but which everyday way is the best?

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method        | Runtime  | Mean       | Error    | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|-------------- |--------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
| Regex         | .NET 8.0 |   656.2 ns |  7.17 ns |  6.36 ns |  1.06 |    0.01 | 0.0629 |      - |   1.03 KB |        1.00 |
| Regex         | .NET 9.0 |   617.5 ns |  3.67 ns |  3.44 ns |  1.00 |    0.01 | 0.0629 |      - |   1.03 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| String        | .NET 8.0 | 1,074.5 ns |  9.94 ns |  9.29 ns |  1.21 |    0.01 | 0.0629 |      - |   1.05 KB |        1.00 |
| String        | .NET 9.0 |   887.9 ns |  2.59 ns |  2.29 ns |  1.00 |    0.00 | 0.0639 |      - |   1.05 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| Span          | .NET 8.0 |   288.8 ns |  5.67 ns |  6.31 ns |  1.53 |    0.04 | 0.0639 |      - |   1.05 KB |        1.00 |
| Span          | .NET 9.0 |   188.4 ns |  2.61 ns |  2.45 ns |  1.00 |    0.02 | 0.0639 |      - |   1.05 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| StringBuilder | .NET 8.0 | 1,042.8 ns |  8.32 ns |  7.78 ns |  1.20 |    0.02 | 0.1411 |      - |   2.33 KB |        1.00 |
| StringBuilder | .NET 9.0 |   871.6 ns | 13.50 ns | 12.63 ns |  1.00 |    0.02 | 0.1421 |      - |   2.33 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| JoinSplit     | .NET 8.0 |   688.6 ns |  4.47 ns |  4.18 ns |  1.06 |    0.01 | 0.2422 | 0.0010 |   3.97 KB |        1.00 |
| JoinSplit     | .NET 9.0 |   650.9 ns |  7.26 ns |  6.79 ns |  1.00 |    0.01 | 0.2422 | 0.0010 |   3.97 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| ConcatSplit   | .NET 8.0 |   680.3 ns | 10.91 ns | 10.20 ns |  1.07 |    0.02 | 0.2251 | 0.0010 |   3.68 KB |        1.00 |
| ConcatSplit   | .NET 9.0 |   634.2 ns | 11.93 ns | 11.15 ns |  1.00 |    0.02 | 0.2251 | 0.0010 |   3.68 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| SpanArrayPool | .NET 8.0 |   312.7 ns |  3.35 ns |  3.13 ns |  0.98 |    0.01 | 0.0629 |      - |   1.03 KB |        1.00 |
| SpanArrayPool | .NET 9.0 |   318.8 ns |  3.48 ns |  3.26 ns |  1.00 |    0.01 | 0.0629 |      - |   1.03 KB |        1.00 |
|               |          |            |          |          |       |         |        |        |           |             |
| SpanStackPool | .NET 8.0 |   370.0 ns |  2.87 ns |  2.69 ns |  1.27 |    0.02 | 0.0629 |      - |   1.03 KB |        1.00 |
| SpanStackPool | .NET 9.0 |   291.1 ns |  5.08 ns |  4.75 ns |  1.00 |    0.02 | 0.0629 |      - |   1.03 KB |        1.00 |
```

## 🏁 Results

- 🚀 There are big differences in the performance and allocation of memory
- 🚀 In this rather small example, Span is by far the fastest variant
- 🚀 StringBuilder is the slowest and most expensive variant
- Regex doesn't do badly, but is only in the midfield and slower than all Span variants

## Remarks

- Maximum performance should be possible with vectorization and e.g. SSE2, which I exclude here in the sense of everyday code.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```
