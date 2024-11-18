# 🌳 Sustainable Code - Any vs Count > 0 📊 

You often see the use of `Any()` to check whether collections contain elements. However, some code analysis tools recommend using the `Count` property for performance reasons. Is this correct?

Yes, lists that are already materialized (HashSet, List..) have the property that already contains how many elements the list has. The enumerator does not have to be triggered, only a comparison is sufficient.
This is many times faster (because O(1)) than `Any()`, which has an additional overhead.

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Runtime  | Items | Mean      | StdDev    | Ratio | RatioSD |
|------- |--------- |------ |----------:|----------:|------:|--------:|
| Any    | .NET 7.0 | 0     | 1.9150 ns | 0.0271 ns | 4.069 |    0.06 |
| Any    | .NET 8.0 | 0     | 1.8727 ns | 0.0139 ns | 3.979 |    0.04 |
| Any    | .NET 9.0 | 0     | 0.4706 ns | 0.0031 ns | 1.000 |    0.01 |
|        |          |       |           |           |       |         |
| Count  | .NET 7.0 | 0     | 0.0034 ns | 0.0017 ns | 0.007 |    0.00 |
| Count  | .NET 8.0 | 0     | 0.0038 ns | 0.0018 ns | 0.008 |    0.00 |
| Count  | .NET 9.0 | 0     | 0.0161 ns | 0.0019 ns | 0.034 |    0.00 |
|        |          |       |           |           |       |         |
| Any    | .NET 7.0 | 1     | 1.9256 ns | 0.0194 ns |  4.06 |    0.05 |
| Any    | .NET 8.0 | 1     | 1.8670 ns | 0.0057 ns |  3.94 |    0.02 |
| Any    | .NET 9.0 | 1     | 0.4740 ns | 0.0027 ns |  1.00 |    0.01 |
|        |          |       |           |           |       |         |
| Count  | .NET 7.0 | 1     | 0.0082 ns | 0.0029 ns |  0.02 |    0.01 |
| Count  | .NET 8.0 | 1     | 0.0062 ns | 0.0013 ns |  0.01 |    0.00 |
| Count  | .NET 9.0 | 1     | 0.0189 ns | 0.0051 ns |  0.04 |    0.01 |
|        |          |       |           |           |       |         |
| Any    | .NET 7.0 | 10    | 1.9292 ns | 0.0199 ns |  4.09 |    0.04 |
| Any    | .NET 8.0 | 10    | 1.8612 ns | 0.0050 ns |  3.95 |    0.02 |
| Any    | .NET 9.0 | 10    | 0.4717 ns | 0.0015 ns |  1.00 |    0.00 |
|        |          |       |           |           |       |         |
| Count  | .NET 7.0 | 10    | 0.0107 ns | 0.0051 ns |  0.02 |    0.01 |
| Count  | .NET 8.0 | 10    | 0.0056 ns | 0.0035 ns |  0.01 |    0.01 |
| Count  | .NET 9.0 | 10    | 0.0159 ns | 0.0026 ns |  0.03 |    0.01 |
|        |          |       |           |           |       |         |
| Any    | .NET 8.0 | 100   | 1.8619 ns | 0.0126 ns |  3.93 |    0.05 |
| Any    | .NET 7.0 | 100   | 1.9241 ns | 0.0252 ns |  4.06 |    0.07 |
| Any    | .NET 9.0 | 100   | 0.4740 ns | 0.0055 ns |  1.00 |    0.02 |
|        |          |       |           |           |       |         |
| Count  | .NET 7.0 | 100   | 0.0102 ns | 0.0029 ns |  0.02 |    0.01 |
| Count  | .NET 8.0 | 100   | 0.0058 ns | 0.0071 ns |  0.01 |    0.01 |
| Count  | .NET 9.0 | 100   | 0.0157 ns | 0.0026 ns |  0.03 |    0.01 |
```

## 🏁 Results

The results are very descriptive and very clear

- Any() is slower in all cases, but faster the more recent the .NET version
- Count>0 is always 10x faster than Any()


## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
