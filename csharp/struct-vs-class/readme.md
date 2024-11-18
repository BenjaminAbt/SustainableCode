# 🌳 Sustainable Code - Struct vs Class 📊

This small example should show how struct and class behave in .NET at runtime, and why struct should be used more often - where it makes sense.

Docs:
- [Structures](https://docs.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct?WT.mc_id=DT-MVP-5001507)
- [Classes](https://docs.microsoft.com/dotnet/csharp/fundamentals/types/classes?WT.mc_id=DT-MVP-5001507)
## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method       | Runtime  | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated |
|------------- |--------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|
| SmallStruct  | .NET 7.0 |  26.13 ns | 0.073 ns | 0.069 ns |  1.04 |    0.01 |      - |         - |
| SmallStruct  | .NET 8.0 |  26.17 ns | 0.054 ns | 0.048 ns |  1.05 |    0.01 |      - |         - |
| SmallStruct  | .NET 9.0 |  25.03 ns | 0.239 ns | 0.212 ns |  1.00 |    0.01 |      - |         - |
|              |          |           |          |          |       |         |        |           |
| MediumStruct | .NET 7.0 |  26.32 ns | 0.129 ns | 0.120 ns |  1.03 |    0.01 |      - |         - |
| MediumStruct | .NET 8.0 |  27.34 ns | 0.163 ns | 0.153 ns |  1.06 |    0.01 |      - |         - |
| MediumStruct | .NET 9.0 |  25.68 ns | 0.315 ns | 0.279 ns |  1.00 |    0.01 |      - |         - |
|              |          |           |          |          |       |         |        |           |
| SmallClass   | .NET 7.0 | 129.75 ns | 1.158 ns | 1.083 ns |  1.00 |    0.02 | 0.1433 |    2400 B |
| SmallClass   | .NET 8.0 | 128.01 ns | 0.599 ns | 0.531 ns |  0.98 |    0.02 | 0.1433 |    2400 B |
| SmallClass   | .NET 9.0 | 130.45 ns | 2.612 ns | 3.008 ns |  1.00 |    0.03 | 0.1433 |    2400 B |
|              |          |           |          |          |       |         |        |           |
| MediumClass  | .NET 7.0 | 292.62 ns | 1.292 ns | 1.209 ns |  1.00 |    0.01 | 0.2389 |    4000 B |
| MediumClass  | .NET 8.0 | 287.63 ns | 1.091 ns | 0.968 ns |  0.98 |    0.01 | 0.2389 |    4000 B |
| MediumClass  | .NET 9.0 | 292.07 ns | 4.250 ns | 3.768 ns |  1.00 |    0.02 | 0.2389 |    4000 B |
```

## 🏁 Results

- 🔋 Both struct samples produce no allocations!
- 🚀 Struct has a better performance over all!
- There is almost no performance difference between .NET 7 and .NET 8.

## Remarks

- The use of struct or class is part of the software architecture.
- Not in all scenarios struct makes sense or can be used (e.g. serialization)!
- The change from class to struct is a breaking change!
- It's all a matter of perspective, whether these are small or large samples here - names are smoke and mirrors.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
