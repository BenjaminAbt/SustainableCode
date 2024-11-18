# 🌳 Sustainable Code - List vs. HashSet 📊

This small example should show the performance comparison between List and HashSet

Docs:
- [List](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [HashSet](https://docs.microsoft.com/dotnet/api/system.collections.generic.hashset-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method       | Runtime  | Count | Mean        | Gen0   | Gen1   | Allocated |
|------------- |--------- |------ |------------:|-------:|-------:|----------:|
| ListWrite    | .NET 7.0 | 10    |    17.62 ns | 0.0057 |      - |      96 B |
| ListWrite    | .NET 8.0 | 10    |    10.36 ns | 0.0057 |      - |      96 B |
| ListWrite    | .NET 9.0 | 10    |    10.14 ns | 0.0057 |      - |      96 B |
|              |          |       |             |        |        |           |
| HashSetWrite | .NET 7.0 | 10    |    46.45 ns | 0.0176 |      - |     296 B |
| HashSetWrite | .NET 8.0 | 10    |    38.23 ns | 0.0176 |      - |     296 B |
| HashSetWrite | .NET 9.0 | 10    |    36.40 ns | 0.0176 |      - |     296 B |
|              |          |       |             |        |        |           |
| ListDistinct | .NET 7.0 | 10    |   117.86 ns | 0.0353 |      - |     592 B |
| ListDistinct | .NET 8.0 | 10    |    92.83 ns | 0.0353 |      - |     592 B |
| ListDistinct | .NET 9.0 | 10    |    72.39 ns | 0.0353 |      - |     592 B |
|              |          |       |             |        |        |           |
| ListWrite    | .NET 7.0 | 100   |   161.84 ns | 0.0272 |      - |     456 B |
| ListWrite    | .NET 8.0 | 100   |    66.02 ns | 0.0272 |      - |     456 B |
| ListWrite    | .NET 9.0 | 100   |    70.78 ns | 0.0272 |      - |     456 B |
|              |          |       |             |        |        |           |
| HashSetWrite | .NET 7.0 | 100   |   392.54 ns | 0.1092 |      - |    1832 B |
| HashSetWrite | .NET 8.0 | 100   |   310.50 ns | 0.1092 |      - |    1832 B |
| HashSetWrite | .NET 9.0 | 100   |   302.29 ns | 0.1092 |      - |    1832 B |
|              |          |       |             |        |        |           |
| ListDistinct | .NET 7.0 | 100   |   958.11 ns | 0.1698 | 0.0010 |    2848 B |
| ListDistinct | .NET 8.0 | 100   |   654.36 ns | 0.1698 | 0.0010 |    2848 B |
| ListDistinct | .NET 9.0 | 100   |   485.05 ns | 0.1698 | 0.0010 |    2848 B |
|              |          |       |             |        |        |           |
| ListWrite    | .NET 7.0 | 500   |   796.96 ns | 0.1221 |      - |    2056 B |
| ListWrite    | .NET 8.0 | 500   |   316.34 ns | 0.1225 |      - |    2056 B |
| ListWrite    | .NET 9.0 | 500   |   233.99 ns | 0.1228 |      - |    2056 B |
|              |          |       |             |        |        |           |
| HashSetWrite | .NET 7.0 | 500   | 1,861.07 ns | 0.5054 |      - |    8456 B |
| HashSetWrite | .NET 8.0 | 500   | 1,441.61 ns | 0.5054 |      - |    8456 B |
| HashSetWrite | .NET 9.0 | 500   | 1,464.85 ns | 0.5054 |      - |    8456 B |
|              |          |       |             |        |        |           |
| ListDistinct | .NET 7.0 | 500   | 4,421.42 ns | 0.7553 | 0.0153 |   12672 B |
| ListDistinct | .NET 8.0 | 500   | 2,955.31 ns | 0.7553 | 0.0153 |   12672 B |
| ListDistinct | .NET 9.0 | 500   | 2,269.79 ns | 0.7553 | 0.0153 |   12672 B |
```



## 🏁 Results

- 🚀 HashSet is a collection that ensures that each element exists only once and thus no manual distinct has to be made. However, this is bought by a performance loss and allocations when inserting the elements!
- The performance benefits of .NET 8 are almost twice as high as in .NET 7.
- Performance has been improved even further with .NET 9.

## Remarks

- The more elements to write, the higher the performance loss
- It must be considered how relevant a distinct is and which quantity should be compared
- Use the HashSet only if you really need to ensure that each element exists only once

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
