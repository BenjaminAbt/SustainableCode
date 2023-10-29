# 🌳 Sustainable Code - Pattern Matching value performance 📊

This example illustrates the runtime cost of pattern matching with a simple reference/value comparison
[C# Pattern matching](https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/pattern-matching?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method                | Runtime  | Mean      |
|---------------------- |--------- |----------:|
| Class_Condition       | .NET 7.0 | 0.5471 ns |
| Class_Condition       | .NET 8.0 | 0.6678 ns |
|                       |          |           |
| Class_Condition       | .NET 7.0 | 0.2580 ns |
| Class_Condition       | .NET 8.0 | 0.1590 ns |
|                       |          |           |
| Class_PatternMatching | .NET 7.0 | 0.2214 ns |
| Class_PatternMatching | .NET 8.0 | 0.0464 ns |
|                       |          |           |
| Class_PatternMatching | .NET 7.0 | 0.6593 ns |
| Class_PatternMatching | .NET 8.0 | 0.7481 ns |
|                       |          |           |
| Struct_HasValue       | .NET 7.0 | 0.2067 ns |
| Struct_HasValue       | .NET 8.0 | 0.0358 ns |
|                       |          |           |
| Struct_HasValue       | .NET 7.0 | 0.0202 ns |
| Struct_HasValue       | .NET 8.0 | 0.0259 ns |
|                       |          |           |
| Struct_PatterMatching | .NET 7.0 | 0.2331 ns |
| Struct_PatterMatching | .NET 8.0 | 0.0398 ns | 
|                       |          |           |
| Struct_PatterMatching | .NET 7.0 | 5.0464 ns | => Performance significantly worse
| Struct_PatterMatching | .NET 8.0 | 4.8592 ns | => Performance significantly worse
```

## 🏁 Results

- 🔋 Pattern Matching has a similar performance as a classic reference match.
- 🏎️ But when dealing with (nullable) structs, a significant loss of performance is measurable, sometimes by a factor of 250.
- All cases in .NET 8 have continued to improve significantly in terms of performance.


## Remarks

- Pattern matching is a great construct for writing code in a simplified way - but it doesn't come for free.

Thanks to [gfoidl](https://github.com/gfoidl) to validate this benchmark.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
