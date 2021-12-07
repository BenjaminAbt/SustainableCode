# 🌳 Sustainable Code - Source Generator based Logging Messages 📊

This sample, based on .NET and [Compile-time logging source generation]](https://docs.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator?WT.mc_id=DT-MVP-5001507), shows how to write logs faster and with less to zero allocations.

## 🔥 Benchmark

For better comparability, additional string concat were added.

```
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1348 (21H1/May2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


|              Method |     Mean |   Error |  StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|-------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|
| SourceCodeGenerated | 122.1 ns | 2.09 ns | 1.96 ns |  1.00 |    0.00 |      - |         - |
|              Concat | 216.5 ns | 4.29 ns | 5.73 ns |  1.76 |    0.05 | 0.0286 |     480 B |
|       Interpolation | 212.3 ns | 2.74 ns | 2.43 ns |  1.74 |    0.03 | 0.0286 |     480 B |
```

## 🏁 Results

- 🎿 The Generator is about 50-60% faster in creating the log messages, as expected.
- 🔋 The String Generator does not generate any allocations for shorter strings
- ✨ The benefits are comparable to general zero-allocation string generation.
- 🕳️ This benchmark is very simple. It may be different with real world exceptions and/or more parameters.

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

This benchmark runs 1:19min on my workstation
