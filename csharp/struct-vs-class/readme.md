# 🌳 Sustainable Code - Struct vs Class 📊

This small example should show how struct and class behave in .NET at runtime, and why struct should be used more often - where it makes sense.

Docs:
- [Structures](https://docs.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct?WT.mc_id=DT-MVP-5001507)
- [Classes](https://docs.microsoft.com/dotnet/csharp/fundamentals/types/classes?WT.mc_id=DT-MVP-5001507)
## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method       | Job      | Runtime  | Mean      | Error    | StdDev    | Gen0   | Allocated |
|------------- |--------- |--------- |----------:|---------:|----------:|-------:|----------:|
| SmallStruct  | .NET 7.0 | .NET 7.0 |  39.14 ns | 0.326 ns |  0.272 ns |      - |         - |
| SmallStruct  | .NET 8.0 | .NET 8.0 |  39.00 ns | 0.310 ns |  0.259 ns |      - |         - |
|              |          |          |           |          |           |        |           |
| MediumStruct | .NET 7.0 | .NET 7.0 |  38.83 ns | 0.290 ns |  0.226 ns |      - |         - |
| MediumStruct | .NET 8.0 | .NET 8.0 |  38.75 ns | 0.276 ns |  0.245 ns |      - |         - |
|              |          |          |           |          |           |        |           |
| SmallClass   | .NET 7.0 | .NET 7.0 | 240.87 ns | 4.264 ns |  6.639 ns | 0.1433 |    2400 B |
| SmallClass   | .NET 8.0 | .NET 8.0 | 228.85 ns | 4.586 ns | 12.632 ns | 0.1433 |    2400 B |
|              |          |          |           |          |           |        |           |
| MediumClass  | .NET 7.0 | .NET 7.0 | 428.15 ns | 8.471 ns | 11.595 ns | 0.2389 |    4000 B |
| MediumClass  | .NET 8.0 | .NET 8.0 | 425.85 ns | 8.487 ns | 18.270 ns | 0.2389 |    4000 B |
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
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
