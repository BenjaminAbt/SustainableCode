# 🌳 Sustainable Code - Random String 📊

Random string generation is a very common use case, with large potential for improvement and performance in percentage terms.

The single operation is not very expensive in total, but the amount of calls usually always justifies an optimized implementation in terms of performance and energy.

This sample, based on .NET 6 and [string.Create()](https://docs.microsoft.com/dotnet/api/system.string.create?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Span<T>](https://docs.microsoft.com/en-us/dotnet/api/system.span-1?view=net-6.0&WT.mc_id=DT-MVP-5001507) compared to solutions found on StackOverflow and Google.

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method          | Runtime  | CharLength | Mean        | Error      | StdDev     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |--------- |----------- |------------:|-----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| StringCreate    | .NET 7.0 | 10         |    35.39 ns |   0.727 ns |   0.808 ns |  1.00 |    0.00 | 0.0029 |      48 B |        1.00 |
| EnumerateRepeat | .NET 7.0 | 10         |   110.11 ns |   1.442 ns |   1.349 ns |  3.11 |    0.08 | 0.0114 |     192 B |        4.00 |
| CharArray       | .NET 7.0 | 10         |    35.78 ns |   0.581 ns |   0.543 ns |  1.01 |    0.03 | 0.0057 |      96 B |        2.00 |
| Span            | .NET 7.0 | 10         |    34.56 ns |   0.454 ns |   0.425 ns |  0.98 |    0.03 | 0.0029 |      48 B |        1.00 |
|                 |          |            |             |            |            |       |         |        |           |             |
| StringCreate    | .NET 8.0 | 10         |    23.50 ns |   0.421 ns |   0.394 ns |  1.00 |    0.00 | 0.0029 |      48 B |        1.00 |
| EnumerateRepeat | .NET 8.0 | 10         |    74.25 ns |   0.613 ns |   0.544 ns |  3.16 |    0.05 | 0.0114 |     192 B |        4.00 |
| CharArray       | .NET 8.0 | 10         |    26.19 ns |   0.543 ns |   0.581 ns |  1.11 |    0.04 | 0.0057 |      96 B |        2.00 |
| Span            | .NET 8.0 | 10         |    22.87 ns |   0.408 ns |   0.362 ns |  0.97 |    0.02 | 0.0029 |      48 B |        1.00 |
|                 |          |            |             |            |            |       |         |        |           |             |
| StringCreate    | .NET 7.0 | 100        |   297.16 ns |   3.904 ns |   3.652 ns |  1.00 |    0.00 | 0.0134 |     224 B |        1.00 |
| EnumerateRepeat | .NET 7.0 | 100        |   707.81 ns |  13.308 ns |  12.448 ns |  2.38 |    0.05 | 0.0324 |     544 B |        2.43 |
| CharArray       | .NET 7.0 | 100        |   295.44 ns |   5.884 ns |   5.216 ns |  0.99 |    0.02 | 0.0267 |     448 B |        2.00 |
| Span            | .NET 7.0 | 100        |   293.80 ns |   3.910 ns |   3.658 ns |  0.99 |    0.02 | 0.0134 |     224 B |        1.00 |
|                 |          |            |             |            |            |       |         |        |           |             |
| StringCreate    | .NET 8.0 | 100        |   191.18 ns |   1.864 ns |   1.744 ns |  1.00 |    0.00 | 0.0134 |     224 B |        1.00 |
| EnumerateRepeat | .NET 8.0 | 100        |   420.08 ns |   8.006 ns |   7.489 ns |  2.20 |    0.04 | 0.0324 |     544 B |        2.43 |
| CharArray       | .NET 8.0 | 100        |   203.43 ns |   4.105 ns |   4.216 ns |  1.07 |    0.03 | 0.0267 |     448 B |        2.00 |
| Span            | .NET 8.0 | 100        |   181.78 ns |   3.416 ns |   3.195 ns |  0.95 |    0.02 | 0.0134 |     224 B |        1.00 |
|                 |          |            |             |            |            |       |         |        |           |             |
| StringCreate    | .NET 7.0 | 1000       | 3,000.02 ns |  46.516 ns |  43.511 ns |  1.00 |    0.00 | 0.1183 |    2024 B |        1.00 |
| EnumerateRepeat | .NET 7.0 | 1000       | 6,633.78 ns | 102.422 ns |  95.806 ns |  2.21 |    0.04 | 0.2441 |    4144 B |        2.05 |
| CharArray       | .NET 7.0 | 1000       | 2,919.56 ns |  57.803 ns |  51.241 ns |  0.97 |    0.02 | 0.2403 |    4048 B |        2.00 |
| Span            | .NET 7.0 | 1000       | 2,745.92 ns |  33.841 ns |  31.654 ns |  0.92 |    0.02 | 0.1183 |    2024 B |        1.00 |
|                 |          |            |             |            |            |       |         |        |           |             |
| StringCreate    | .NET 8.0 | 1000       | 1,908.84 ns |  23.776 ns |  22.240 ns |  1.00 |    0.00 | 0.1183 |    2024 B |        1.00 |
| EnumerateRepeat | .NET 8.0 | 1000       | 3,822.60 ns |  18.622 ns |  16.508 ns |  2.00 |    0.03 | 0.2441 |    4144 B |        2.05 |
| CharArray       | .NET 8.0 | 1000       | 2,045.09 ns |  42.378 ns | 120.218 ns |  1.04 |    0.06 | 0.2403 |    4048 B |        2.00 |
| Span            | .NET 8.0 | 1000       | 1,709.99 ns |  19.047 ns |  16.885 ns |  0.90 |    0.01 | 0.1202 |    2024 B |        1.00 |
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
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
