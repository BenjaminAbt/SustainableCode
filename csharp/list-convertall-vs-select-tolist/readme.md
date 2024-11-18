# 🌳 Sustainable Code - ConvertAll() vs Select().ToList() 📊

Roslyn and other style tools often suggest [ConvertTo](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1.convertall?view=net-8.0&WT.mc_id=DT-MVP-5001507) when mapping classes with Select-ToList.
But does this make sense at runtime?

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method       | Runtime  | Count | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated |
|------------- |--------- |------ |---------:|--------:|--------:|------:|--------:|-------:|----------:|
| SelectToList | .NET 7.0 | 100   | 182.8 ns | 2.83 ns | 2.65 ns |  1.32 |    0.03 | 0.0315 |     528 B |
| SelectToList | .NET 8.0 | 100   | 159.5 ns | 0.76 ns | 0.71 ns |  1.15 |    0.02 | 0.0315 |     528 B |
| SelectToList | .NET 9.0 | 100   | 138.7 ns | 2.69 ns | 2.38 ns |  1.00 |    0.02 | 0.0315 |     528 B |
|              |          |       |          |         |         |       |         |        |           |
| ConvertAll   | .NET 7.0 | 100   | 147.6 ns | 1.82 ns | 1.70 ns |  1.01 |    0.01 | 0.0272 |     456 B |
| ConvertAll   | .NET 8.0 | 100   | 147.8 ns | 0.62 ns | 0.55 ns |  1.02 |    0.01 | 0.0272 |     456 B |
| ConvertAll   | .NET 9.0 | 100   | 145.6 ns | 0.62 ns | 0.52 ns |  1.00 |    0.00 | 0.0272 |     456 B |
```

*Some columns of the output were removed*

## 🏁 Results

- In almost all tests, Select-ToList was more efficient.
- ConvertAll costs less allocations.
- However, the difference is only very marginal in reality, so in the end it is developer taste what is used.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
