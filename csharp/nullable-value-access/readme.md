# 🌳 Sustainable Code - Pattern Matching value performance 📊

This example illustrates the runtime cost of pattern matching with a simple reference/value comparison
[C# Pattern matching](https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/pattern-matching?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2130/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100-rc.2.22477.23
  [Host]   : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.0 (7.0.22.47203), X64 RyuJIT AVX2


=== .NET 6
|                Method |    Input |      Mean |
|---------------------- |--------- |----------:|
|       Class_Condition |     null | 0.2111 ns |
| Class_PatternMatching |     null | 0.2133 ns | => Equivalent

|       Class_Condition | not null | 0.0133 ns |
| Class_PatternMatching | not null | 0.0127 ns | => Equivalent

|       Struct_HasValue |     null | 0.2189 ns |
| Struct_PatterMatching |     null | 0.2388 ns | => Equivalent

|       Struct_HasValue | not null | 0.2534 ns |
| Struct_PatterMatching | not null | 3.6606 ns | => Performance significantly worse

=== .NET 7
|       Class_Condition |     null | 0.1978 ns |
| Class_PatternMatching |     null | 0.2122 ns | => Equivalent

|       Class_Condition | not null | 0.6484 ns |
| Class_PatternMatching | not null | 0.6228 ns | => Equivalent

|       Struct_HasValue |     null | 0.2834 ns |
| Struct_PatterMatching |     null | 0.2395 ns | => Equivalent

|       Struct_HasValue | not null | 0.0210 ns |
| Struct_PatterMatching | not null | 5.0965 ns | => Performance significantly worse
```

## 🏁 Results

- 🔋 Pattern Matching has a similar performance as a classic reference match.
- 🏎️ But when dealing with (nullable) structs, a significant loss of performance is measurable, sometimes by a factor of 250.


## Remarks

- Pattern matching is a great construct for writing code in a simplified way - but it doesn't come for free.

Thanks to [gfoidl](https://github.com/gfoidl) to validate this benchmark.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark runs ~8:30min on my workstation.
