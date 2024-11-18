# 🌳 Sustainable Code - Pattern Matching value performance 📊

This example illustrates the runtime cost of pattern matching with a simple reference/value comparison
[C# Pattern matching](https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/pattern-matching?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                | Runtime  | Mean      | StdDev    | Ratio |
|---------------------- |--------- |----------:|----------:|------:|
| Class_Condition       | .NET 7.0 | 0.0031 ns | 0.0027 ns |     ? |
| Class_Condition       | .NET 8.0 | 0.0027 ns | 0.0045 ns |     ? |
| Class_Condition       | .NET 9.0 | 0.0026 ns | 0.0033 ns |     ? |
|                       |          |           |           |       |
| Class_PatternMatching | .NET 7.0 | 0.0036 ns | 0.0045 ns |     ? |
| Class_PatternMatching | .NET 8.0 | 0.0023 ns | 0.0032 ns |     ? |
| Class_PatternMatching | .NET 9.0 | 0.0087 ns | 0.0076 ns |     ? |
|                       |          |           |           |       |
| Struct_HasValue       | .NET 7.0 | 0.0113 ns | 0.0030 ns |  0.72 |
| Struct_HasValue       | .NET 8.0 | 0.0094 ns | 0.0029 ns |  0.61 |
| Struct_HasValue       | .NET 9.0 | 0.0160 ns | 0.0026 ns |  1.03 |
|                       |          |           |           |       |
| Struct_PatterMatching | .NET 7.0 | 0.0146 ns | 0.0030 ns |  1.46 |
| Struct_PatterMatching | .NET 8.0 | 0.0098 ns | 0.0011 ns |  0.98 |
| Struct_PatterMatching | .NET 9.0 | 0.0108 ns | 0.0031 ns |  1.08 |
|                       |          |           |           |       |
| Class_Condition       | .NET 7.0 | 0.1921 ns | 0.0097 ns |  1.03 |
| Class_Condition       | .NET 8.0 | 0.1891 ns | 0.0054 ns |  1.01 |
| Class_Condition       | .NET 9.0 | 0.1869 ns | 0.0073 ns |  1.00 |
|                       |          |           |           |       |
| Class_PatternMatching | .NET 7.0 | 0.1900 ns | 0.0059 ns |  1.02 |
| Class_PatternMatching | .NET 8.0 | 0.1912 ns | 0.0066 ns |  1.03 |
| Class_PatternMatching | .NET 9.0 | 0.1865 ns | 0.0049 ns |  1.00 |
|                       |          |           |           |       |
| Struct_HasValue       | .NET 7.0 | 0.0098 ns | 0.0028 ns |  0.46 |
| Struct_HasValue       | .NET 8.0 | 0.0124 ns | 0.0037 ns |  0.58 |
| Struct_HasValue       | .NET 9.0 | 0.0215 ns | 0.0019 ns |  1.01 |
|                       |          |           |           |       |
| Struct_PatterMatching | .NET 7.0 | 2.7237 ns | 0.0092 ns |  1.07 |
| Struct_PatterMatching | .NET 8.0 | 2.5338 ns | 0.0064 ns |  1.00 |
| Struct_PatterMatching | .NET 9.0 | 2.5431 ns | 0.0078 ns |  1.00 |
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
- 2024/11 - Add .NET 9
