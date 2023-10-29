# 🌳 Sustainable Code - Source Generator based Logging Messages 📊

This sample, based on .NET and [Compile-time logging source generation]](https://docs.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator?WT.mc_id=DT-MVP-5001507), shows how to write logs faster and with less to zero allocations.

## 🔥 Benchmark

For better comparability, additional string concat were added.

```
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method              | Runtime  | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated |
|-------------------- |--------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|
| SourceCodeGenerated | .NET 7.0 | 114.4 ns | 2.30 ns | 2.15 ns |  1.00 |    0.00 |      - |         - |
| SourceCodeGenerated | .NET 8.0 | 109.0 ns | 1.83 ns | 1.71 ns |  1.00 |    0.00 |      - |         - |
|                     |          |          |         |         |       |         |        |           |
| Concat              | .NET 7.0 | 176.2 ns | 3.49 ns | 3.88 ns |  1.54 |    0.05 | 0.0286 |     480 B |
| Concat              | .NET 8.0 | 148.5 ns | 2.94 ns | 3.72 ns |  1.37 |    0.04 | 0.0286 |     480 B |
|                     |          |          |         |         |       |         |        |           |
| Interpolation       | .NET 7.0 | 178.8 ns | 3.52 ns | 3.91 ns |  1.57 |    0.05 | 0.0286 |     480 B |
| Interpolation       | .NET 8.0 | 147.9 ns | 2.54 ns | 4.03 ns |  1.38 |    0.04 | 0.0286 |     480 B |
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
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
