# 🌳 Sustainable Code - Source Generator based Logging Messages 📊

This sample, based on .NET and [Compile-time logging source generation](https://docs.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator?WT.mc_id=DT-MVP-5001507), shows how to write logs faster and with less to zero allocations.

## 🔥 Benchmark

For better comparability, additional string concat were added.

```
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method              | Runtime  | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated |
|-------------------- |--------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|
| SourceCodeGenerated | .NET 7.0 |  83.49 ns | 0.299 ns | 0.279 ns |  1.24 |    0.01 |      - |         - |
| SourceCodeGenerated | .NET 8.0 |  82.12 ns | 0.252 ns | 0.224 ns |  1.22 |    0.01 |      - |         - |
| SourceCodeGenerated | .NET 9.0 |  67.30 ns | 0.365 ns | 0.341 ns |  1.00 |    0.01 |      - |         - |
|                     |          |           |          |          |       |         |        |           |
| Concat              | .NET 7.0 | 112.57 ns | 0.235 ns | 0.208 ns |  1.67 |    0.01 | 0.0286 |     480 B |
| Concat              | .NET 8.0 |  96.94 ns | 1.239 ns | 1.098 ns |  1.44 |    0.02 | 0.0286 |     480 B |
| Concat              | .NET 9.0 |  94.59 ns | 1.136 ns | 1.007 ns |  1.41 |    0.02 | 0.0286 |     480 B |
|                     |          |           |          |          |       |         |        |           |
| Interpolation       | .NET 7.0 | 112.79 ns | 0.444 ns | 0.416 ns |  1.68 |    0.01 | 0.0286 |     480 B |
| Interpolation       | .NET 8.0 |  97.08 ns | 1.208 ns | 1.071 ns |  1.44 |    0.02 | 0.0286 |     480 B |
| Interpolation       | .NET 9.0 |  94.12 ns | 0.620 ns | 0.580 ns |  1.40 |    0.01 | 0.0286 |     480 B |
```

## 🏁 Results

- 🎿 The Generator is about 50-60% faster in creating the log messages, as expected.
- 🔋 The String Generator does not generate any allocations for shorter strings
- ✨ The benefits are comparable to general zero-allocation string generation.
- 🕳️ This benchmark is very simple. It may be different with real world exceptions and/or more parameters.
- Performance in .NET 8 has changed slightly, allocations have remained the same.

## Docs

- [Compile-time logging source generation](https://docs.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator?WT.mc_id=DT-MVP-5001507)
- [Improving logging performance with source generators](https://andrewlock.net/exploring-dotnet-6-part-8-improving-logging-performance-with-source-generators/) by Andrew Lock

## Remarks

- Logging is relatively expensive and should be used overall well placed
- Often it is also worth improving the logging strategy in general to get more performance.
- The Log Message generator improves string generation with less to zero allocations.
- The generation is a helper / wrapper around the ILogger.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
