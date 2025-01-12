# 🌳 Sustainable Code - Aggressive Inlining 📊

[Aggressive inlining](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.methodimploptions?view=net-8.0&WT.mc_id=DT-MVP-5001507) is an optimization technique that the compiler can use in C# to improve the performance of a program.\
With inlining, the code of a method is inserted directly at the point where it is called instead of executing the method call. This saves time as the overhead of a method call is eliminated.

This example uses a simple Unicode method to show how incredibly efficient aggressive inlining can be in terms of performance.

## 🔥 Benchmark

```shell
// * Summary *

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5247/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.200-preview.0.24575.35
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 9.0  Runtime=.NET 9.0

| Method                   | Mean      | Error     | StdDev    | Ratio | RatioSD |
|------------------------- |----------:|----------:|----------:|------:|--------:|
| HasUnicode_NonAggressive | 0.1933 ns | 0.0069 ns | 0.0065 ns |  1.00 |    0.05 |
|                          |           |           |           |       |         |
| HasUnicode_Aggressive    | 0.0073 ns | 0.0040 ns | 0.0037 ns |  1.65 |    2.19 |

```

## 🏁 Results

- Aggressive inlining offers a performance boost
- Execution is approx. 25 times faster

- The simpler and more efficient the content of a method (here Unicode check), the higher the overhead on performance and the more efficient the result of Aggressive Inlining

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```
