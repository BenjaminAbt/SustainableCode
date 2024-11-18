# 🌳 Sustainable Code - less allocations (and better performance) with NetFabric.Hyperlinq 📊 

[NetFabric.Hyperlinq](https://github.com/NetFabric/NetFabric.Hyperlinq) is a high performance LINQ implementation with minimal heap allocations. It supports enumerables, async enumerables, arrays and Span<T>.

Visit https://github.com/NetFabric/NetFabric.Hyperlinq

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method          | Runtime  | Mean       | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------- |--------- |-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| SystemLinqWhere | .NET 7.0 | 1,837.1 ns |  3.03 |    0.04 | 0.5074 | 0.0057 |    8.3 KB |        2.28 |
| SystemLinqWhere | .NET 8.0 | 1,099.0 ns |  1.82 |    0.02 | 0.5074 | 0.0057 |    8.3 KB |        2.28 |
| SystemLinqWhere | .NET 9.0 |   605.4 ns |  1.00 |    0.01 | 0.2222 |      - |   3.64 KB |        1.00 |
|                 |          |            |       |         |        |        |           |             |
| HyperLinqWhere  | .NET 7.0 | 1,775.3 ns |  2.93 |    0.03 | 0.2174 | 0.0019 |   3.57 KB |        0.98 |
| HyperLinqWhere  | .NET 8.0 |   850.6 ns |  1.41 |    0.02 | 0.2174 | 0.0029 |   3.57 KB |        0.98 |
| HyperLinqWhere  | .NET 9.0 |         NA |     ? |       ? |     NA |     NA |        NA |           ? |
```

## 🏁 Results

- ~20% faster code
- Gen 0 is only one third (-68%)
- Gen 1 is only one fourth (-76%)
- allocations have halved

## ℹ Remarks

- Performance has been greatly improved with .NET 9. The comparison between .NET 9 and HyperLinq with .NET 8 shows that the .NET 9 system is now faster.
- The development of HyperLinq seems to be inactive and does not exist for .NET 9 so far.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
