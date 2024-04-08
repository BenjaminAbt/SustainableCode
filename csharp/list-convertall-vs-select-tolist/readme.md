# 🌳 Sustainable Code - ConvertAll() vs Select().ToList() 📊

Roslyn and other style tools often suggest [ConvertTo](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1.convertall?view=net-8.0&WT.mc_id=DT-MVP-5001507) when mapping classes with Select-ToList.
But does this make sense at runtime?

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4170/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100-preview.2.24157.14
  [Host]   : .NET 7.0.17 (7.0.1724.11508), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.12805), X64 RyuJIT AVX2


| Method       | Runtime  | Mean     | Error   | StdDev  | Ratio | RatioSD |
|------------- |--------- |---------:|--------:|--------:|------:|--------:|
| SelectToList | .NET 8.0 | 192.4 ns | 3.63 ns | 8.13 ns |  1.00 |    0.00 |
| ConvertAll   | .NET 8.0 | 209.5 ns | 4.14 ns | 4.61 ns |  1.06 |    0.04 |
|              |          |          |         |         |       |         |
| SelectToList | .NET 9.0 | 183.2 ns | 3.21 ns | 2.68 ns |  1.00 |    0.00 |
| ConvertAll   | .NET 9.0 | 213.0 ns | 4.22 ns | 7.71 ns |  1.17 |    0.04 |
```

*Some columns of the output were removed*

## 🏁 Results

- In almost all cases of my tests, Select-ToList was more efficient
- With .NET 9, the Select variant was consistently more efficient
- However, the difference is only very marginal in reality, so in the end it is developer taste what is used

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

