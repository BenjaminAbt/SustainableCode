# 🌳 Sustainable Code - List vs. HashSet 📊

This small example should show the performance comparison between List and HashSet

Docs:
- [List](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [HashSet](https://docs.microsoft.com/dotnet/api/system.collections.generic.hashset-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method       | Runtime  | Count | Mean        | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|------------- |--------- |------ |------------:|----------:|----------:|-------:|-------:|----------:|
| ListWrite    | .NET 7.0 | 10    |    29.24 ns |  0.512 ns |  0.479 ns | 0.0057 |      - |      96 B |
| ListWrite    | .NET 8.0 | 10    |    17.70 ns |  0.372 ns |  0.348 ns | 0.0057 |      - |      96 B |
|              |          |       |             |           |           |        |        |           |
| HashSetWrite | .NET 7.0 | 10    |    77.12 ns |  1.555 ns |  2.804 ns | 0.0176 |      - |     296 B |
| HashSetWrite | .NET 8.0 | 10    |    68.13 ns |  1.347 ns |  1.843 ns | 0.0176 |      - |     296 B |
|              |          |       |             |           |           |        |        |           |
| ListDistinct | .NET 7.0 | 10    |   199.39 ns |  3.804 ns |  3.736 ns | 0.0353 |      - |     592 B |
| ListDistinct | .NET 8.0 | 10    |   179.22 ns |  3.521 ns |  3.458 ns | 0.0353 |      - |     592 B |
|              |          |       |             |           |           |        |        |           |
| ListWrite    | .NET 7.0 | 100   |   205.82 ns |  3.058 ns |  2.711 ns | 0.0272 |      - |     456 B |
| ListWrite    | .NET 8.0 | 100   |    96.70 ns |  1.599 ns |  1.496 ns | 0.0272 |      - |     456 B |
|              |          |       |             |           |           |        |        |           |
| HashSetWrite | .NET 7.0 | 100   |   642.95 ns |  5.789 ns |  5.132 ns | 0.1087 |      - |    1832 B |
| HashSetWrite | .NET 8.0 | 100   |   526.90 ns |  6.265 ns |  5.861 ns | 0.1087 |      - |    1832 B |
|              |          |       |             |           |           |        |        |           |
| ListDistinct | .NET 7.0 | 100   | 1,466.94 ns | 27.696 ns | 25.907 ns | 0.1698 |      - |    2848 B |
| ListDistinct | .NET 8.0 | 100   | 1,026.49 ns | 11.527 ns | 10.782 ns | 0.1698 |      - |    2848 B |
|              |          |       |             |           |           |        |        |           |
| ListWrite    | .NET 7.0 | 500   |   967.23 ns | 15.021 ns | 14.051 ns | 0.1221 |      - |    2056 B |
| ListWrite    | .NET 8.0 | 500   |   440.48 ns |  6.749 ns |  6.313 ns | 0.1225 |      - |    2056 B |
|              |          |       |             |           |           |        |        |           |
| HashSetWrite | .NET 7.0 | 500   | 2,831.40 ns | 23.379 ns | 19.522 ns | 0.5035 |      - |    8456 B |
| HashSetWrite | .NET 8.0 | 500   | 2,434.14 ns | 37.114 ns | 41.252 ns | 0.5035 |      - |    8456 B |
|              |          |       |             |           |           |        |        |           |
| ListDistinct | .NET 7.0 | 500   | 6,531.18 ns | 64.563 ns | 57.233 ns | 0.7553 | 0.0153 |   12672 B |
| ListDistinct | .NET 8.0 | 500   | 4,712.75 ns | 62.142 ns | 51.891 ns | 0.7553 | 0.0153 |   12672 B |
```



## 🏁 Results

- 🚀 HashSet is a collection that ensures that each element exists only once and thus no manual distinct has to be made. However, this is bought by a performance loss and allocations when inserting the elements!

## Remarks

- The more elements to write, the higher the performance loss
- It must be considered how relevant a distinct is and which quantity should be compared
- Use the HashSet only if you really need to ensure that each element exists only once

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
