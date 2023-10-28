# 🌳 Sustainable Code - less allocations (and better performance) with NetFabric.Hyperlinq 📊 

[NetFabric.Hyperlinq](https://github.com/NetFabric/NetFabric.Hyperlinq) is a high performance LINQ implementation with minimal heap allocations. It supports enumerables, async enumerables, arrays and Span<T>.

Visit https://github.com/NetFabric/NetFabric.Hyperlinq

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method          | Runtime  | Mean     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------- |--------- |---------:|------:|--------:|-------:|-------:|----------:|------------:|
| SystemLinqWhere | .NET 7.0 | 3.237 us |  1.00 |    0.00 | 0.5074 | 0.0038 |    8.3 KB |        1.00 |
| HyperLinqWhere  | .NET 7.0 | 2.146 us |  0.66 |    0.05 | 0.2174 |      - |   3.57 KB |        0.43 |
|                 |          |          |       |         |        |        |           |             |
| SystemLinqWhere | .NET 8.0 | 2.106 us |  1.00 |    0.00 | 0.5074 | 0.0038 |    8.3 KB |        1.00 |
| HyperLinqWhere  | .NET 8.0 | 1.471 us |  0.69 |    0.02 | 0.2174 | 0.0019 |   3.57 KB |        0.43 |
```

## 🏁 Results

- ~20% faster code
- Gen 0 is only one third (-68%)
- Gen 1 is only one fourth (-76%)
- allocations have halved

## ℹ Remarks

I already use HyperLinq in many of my projects and have been able to massively reduce memory consumption while increasing performance.
This has led to us using smaller Azure instances in a variety of projects and services, reducing costs and CO2 consumption.

However, as in any bechnmark, this must be applied reasonably to the individual case in order to achieve improvement.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
