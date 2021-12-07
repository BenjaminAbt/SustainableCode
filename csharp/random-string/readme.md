# 🌳 Sustainable Code - Random String 📊

Random string generation is a very common use case, with large potential for improvement and performance in percentage terms.

The single operation is not very expensive in total, but the amount of calls usually always justifies an optimized implementation in terms of performance and energy.

This sample, based on .NET 6 and [string.Create()](https://docs.microsoft.com/dotnet/api/system.string.create?view=net-6.0&WT.mc_id=DT-MVP-5001507) and [Span<T>](https://docs.microsoft.com/en-us/dotnet/api/system.span-1?view=net-6.0&WT.mc_id=DT-MVP-5001507) compared to solutions found on StackOverflow and Google.

## 🔥 Benchmark

```sh
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1348 (21H1/May2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


|          Method | CharLength |        Mean |      Error |     StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|---------------- |----------- |------------:|-----------:|-----------:|------:|--------:|-------:|----------:|
|    StringCreate |         10 |    43.97 ns |   0.338 ns |   0.300 ns |  1.00 |    0.00 | 0.0029 |      48 B |
| EnumerateRepeat |         10 |   108.19 ns |   2.091 ns |   2.325 ns |  2.47 |    0.04 | 0.0114 |     192 B |
|       CharArray |         10 |    43.49 ns |   0.609 ns |   0.569 ns |  0.99 |    0.01 | 0.0057 |      96 B |
|            Span |         10 |    44.60 ns |   0.737 ns |   0.689 ns |  1.02 |    0.02 | 0.0029 |      48 B |
|                 |            |             |            |            |       |         |        |           |
|    StringCreate |        100 |   388.34 ns |   3.268 ns |   3.056 ns |  1.00 |    0.00 | 0.0134 |     224 B |
| EnumerateRepeat |        100 |   679.51 ns |  13.375 ns |  18.308 ns |  1.74 |    0.05 | 0.0324 |     544 B |
|       CharArray |        100 |   360.21 ns |   2.479 ns |   2.319 ns |  0.93 |    0.01 | 0.0267 |     448 B |
|            Span |        100 |   390.97 ns |   6.416 ns |   6.002 ns |  1.01 |    0.01 | 0.0134 |     224 B |
|                 |            |             |            |            |       |         |        |           |
|    StringCreate |       1000 | 3,778.98 ns |  28.014 ns |  26.204 ns |  1.00 |    0.00 | 0.1144 |   2,024 B |
| EnumerateRepeat |       1000 | 6,155.09 ns |  71.287 ns |  63.194 ns |  1.63 |    0.02 | 0.2441 |   4,144 B |
|       CharArray |       1000 | 3,487.18 ns |  28.482 ns |  26.642 ns |  0.92 |    0.01 | 0.2403 |   4,048 B |
|            Span |       1000 | 3,801.13 ns |  48.570 ns |  45.433 ns |  1.01 |    0.02 | 0.1144 |   2,024 B |
```

## 🏁 Results

- 🔋 string.Create and Span<T> are the most efficient way to create (random) strings
- 🏃‍♀️ The larger the strings, the clearer the performance advantage
- 🏎️ string.Create has the best performance numbers

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

This benchmark runs several minutes (about 4mins on my workstation)
