# 🌳 Sustainable Code - Random String 📊

Random string generation is a very common use case, with large potential for improvement and performance in percentage terms.

The single operation is not very expensive in total, but the amount of calls usually always justifies an optimized implementation in terms of performance and energy.

This sample, based on .NET 6 and [string.Create()](https://docs.microsoft.com/dotnet/api/system.string.create?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Span<T>](https://docs.microsoft.com/en-us/dotnet/api/system.span-1?view=net-6.0&WT.mc_id=DT-MVP-5001507) compared to solutions found on StackOverflow and Google.

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method          | Runtime  | CharLength | Mean        | Error     | StdDev    | Ratio | Allocated |
|---------------- |--------- |----------- |------------:|----------:|----------:|------:|----------:|
| StringCreate    | .NET 7.0 | 10         |    28.13 ns |  0.141 ns |  0.118 ns |  1.88 |      48 B |
| StringCreate    | .NET 8.0 | 10         |    15.81 ns |  0.181 ns |  0.169 ns |  1.05 |      48 B |
| StringCreate    | .NET 9.0 | 10         |    14.99 ns |  0.085 ns |  0.071 ns |  1.00 |      48 B |
|                 |          |            |             |           |           |       |           |
| EnumerateRepeat | .NET 7.0 | 10         |    74.58 ns |  1.077 ns |  0.955 ns |  2.21 |     192 B |
| EnumerateRepeat | .NET 8.0 | 10         |    47.17 ns |  0.337 ns |  0.299 ns |  1.40 |     192 B |
| EnumerateRepeat | .NET 9.0 | 10         |    33.77 ns |  0.320 ns |  0.267 ns |  1.00 |     192 B |
|                 |          |            |             |           |           |       |           |
| CharArray       | .NET 7.0 | 10         |    28.92 ns |  0.168 ns |  0.157 ns |  1.66 |      96 B |
| CharArray       | .NET 8.0 | 10         |    17.58 ns |  0.113 ns |  0.101 ns |  1.01 |      96 B |
| CharArray       | .NET 9.0 | 10         |    17.45 ns |  0.222 ns |  0.207 ns |  1.00 |      96 B |
|                 |          |            |             |           |           |       |           |
| Span            | .NET 7.0 | 10         |    28.20 ns |  0.097 ns |  0.076 ns |  1.85 |      48 B |
| Span            | .NET 8.0 | 10         |    15.30 ns |  0.102 ns |  0.096 ns |  1.00 |      48 B |
| Span            | .NET 9.0 | 10         |    15.24 ns |  0.160 ns |  0.142 ns |  1.00 |      48 B |
|                 |          |            |             |           |           |       |           |
| StringCreate    | .NET 7.0 | 100        |   279.18 ns |  1.074 ns |  0.897 ns |  2.31 |     224 B |
| StringCreate    | .NET 8.0 | 100        |   129.04 ns |  0.879 ns |  0.822 ns |  1.07 |     224 B |
| StringCreate    | .NET 9.0 | 100        |   120.93 ns |  0.638 ns |  0.597 ns |  1.00 |     224 B |
|                 |          |            |             |           |           |       |           |
| EnumerateRepeat | .NET 7.0 | 100        |   525.01 ns |  2.208 ns |  1.724 ns |  3.01 |     544 B |
| EnumerateRepeat | .NET 8.0 | 100        |   311.45 ns |  3.997 ns |  3.739 ns |  1.79 |     544 B |
| EnumerateRepeat | .NET 9.0 | 100        |   174.32 ns |  0.945 ns |  0.838 ns |  1.00 |     544 B |
|                 |          |            |             |           |           |       |           |
| CharArray       | .NET 7.0 | 100        |   270.20 ns |  1.172 ns |  1.096 ns |  2.29 |     448 B |
| CharArray       | .NET 8.0 | 100        |   126.29 ns |  1.023 ns |  0.957 ns |  1.07 |     448 B |
| CharArray       | .NET 9.0 | 100        |   118.22 ns |  0.606 ns |  0.567 ns |  1.00 |     448 B |
|                 |          |            |             |           |           |       |           |
| Span            | .NET 7.0 | 100        |   270.39 ns |  2.522 ns |  2.359 ns |  2.27 |     224 B |
| Span            | .NET 8.0 | 100        |   118.29 ns |  0.368 ns |  0.344 ns |  0.99 |     224 B |
| Span            | .NET 9.0 | 100        |   119.29 ns |  1.066 ns |  0.997 ns |  1.00 |     224 B |
|                 |          |            |             |           |           |       |           |
| StringCreate    | .NET 7.0 | 1000       | 2,625.71 ns | 10.050 ns |  9.401 ns |  2.18 |    2024 B |
| StringCreate    | .NET 8.0 | 1000       | 1,217.06 ns |  4.586 ns |  3.830 ns |  1.01 |    2024 B |
| StringCreate    | .NET 9.0 | 1000       | 1,205.26 ns |  7.794 ns |  6.508 ns |  1.00 |    2024 B |
|                 |          |            |             |           |           |       |           |
| EnumerateRepeat | .NET 7.0 | 1000       | 4,848.13 ns | 13.721 ns | 11.458 ns |  3.05 |    4144 B |
| EnumerateRepeat | .NET 8.0 | 1000       | 2,747.68 ns | 10.834 ns |  9.047 ns |  1.73 |    4144 B |
| EnumerateRepeat | .NET 9.0 | 1000       | 1,587.60 ns | 10.722 ns |  8.371 ns |  1.00 |    4144 B |
|                 |          |            |             |           |           |       |           |
| CharArray       | .NET 7.0 | 1000       | 2,526.60 ns |  8.985 ns |  8.404 ns |  2.19 |    4048 B |
| CharArray       | .NET 8.0 | 1000       | 1,216.09 ns |  3.699 ns |  3.460 ns |  1.05 |    4048 B |
| CharArray       | .NET 9.0 | 1000       | 1,153.79 ns | 11.739 ns |  9.165 ns |  1.00 |    4048 B |
|                 |          |            |             |           |           |       |           |
| Span            | .NET 7.0 | 1000       | 2,592.64 ns | 16.828 ns | 15.741 ns |  2.19 |    2024 B |
| Span            | .NET 8.0 | 1000       | 1,184.21 ns | 10.027 ns |  8.889 ns |  1.00 |    2024 B |
| Span            | .NET 9.0 | 1000       | 1,182.92 ns |  8.253 ns |  7.720 ns |  1.00 |    2024 B |
```

## 🏁 Results

- 🔋 string.Create and Span<T> are the most efficient way to create (random) strings
- 🏃‍♀️ The larger the strings, the clearer the performance advantage
- 🏎️ string.Create has the best performance numbers
- Across all examples, performance is better in .NET 8. Allocations have remained the same.

## Remarks

- This sample is just a sample!
- The double allocation of the char array happens by converting it into the string
- string.Create is underrated, but the most performant way to create strings today
- string.Create is felt by many to be complex, but is worthwhile at runtime. Close behind is the construct via Span
- For strings, there could be more performant solutions with unsafe code.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
