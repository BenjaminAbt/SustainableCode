# 🌳 Sustainable Code - String Cleanup 📊

Validating and cleaning up strings is a common task in software development. This example shows various ways in which strings can be cleaned up using control chars.

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4412/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100-preview.3.24204.13
  [Host]   : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2

Job=.NET 8.0  Runtime=.NET 8.0

| Method                 | Mean       | Error    | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
| StringBuilder_Pool     |   708.9 ns |  6.48 ns |  5.74 ns |  1.00 |    0.00 | 0.0744 |      - |   1.22 KB |        1.00 |
| StringBuilder_Instance |   811.4 ns | 15.80 ns | 19.41 ns |  1.15 |    0.03 | 0.2270 | 0.0019 |   3.71 KB |        3.04 |
| Linq                   | 1,808.9 ns | 20.57 ns | 19.24 ns |  2.55 |    0.03 | 0.0782 |      - |    1.3 KB |        1.07 |
| Span                   |   403.8 ns |  6.68 ns |  6.25 ns |  0.57 |    0.01 | 0.0744 |      - |   1.22 KB |        1.00 |
```

## 🏁 Results

- 🚀 Linq is by far the slowest option
- 🚀 A StringBuilder is more performant in comparison; performance can be further improved with StringBuilder pooling
- 🚀 `Span<char>` is by far the best performing solution, but would cause more allocations if converted to a string.

## Remarks

- `ReadOnlySpan<char>` would cause the double allocation when converted to `string` via ToString.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```
