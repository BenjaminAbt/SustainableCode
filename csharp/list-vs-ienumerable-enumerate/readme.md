# 🌳 Sustainable Code - Enumerate List vs. IEnumerable  📊

This code example shows the different behavior of runs of IEnumerable and List. 

> This example has no claim to how lists are handled in the very very best case, but **how they are currently handled in everyday life**.

- [List - msdocs](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1?WT.mc_id=DT-MVP-5001507)
- [IEnumeranle - msdocs](https://docs.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1?WT.mc_id=DT-MVP-5001507)

## Background of this test:
There are always the discussions whether to use List or IEnumerable - especially for returns.
So you can find many repository examples in the net, which say that IEnumerable is always the best solution - although for example a materialization is already done.

Whats better
```csharp
// This
public IEnumerable<MyEntity> GetEntities() // return List as IEnumerable
    return _context.MyEntities.ToList();

// or 
public List<MyEntity> GetEntities()
    return _context.MyEntities.ToList(); // return List as List
```

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                  | Runtime  | Mean       | Error    | StdDev   | Ratio | Gen0   | Allocated |
|------------------------ |--------- |-----------:|---------:|---------:|------:|-------:|----------:|
| IEnumerable             | .NET 7.0 | 2,051.0 ns | 10.50 ns |  9.82 ns |  4.18 |      - |      40 B |
| IEnumerable             | .NET 8.0 |   378.1 ns |  0.94 ns |  0.83 ns |  0.77 | 0.0024 |      40 B |
| IEnumerable             | .NET 9.0 |   491.6 ns |  9.80 ns | 24.76 ns |  1.00 | 0.0019 |      40 B |
|                         |          |            |          |          |       |        |           |
| IEnumerable_ToList      | .NET 7.0 |   729.2 ns |  4.05 ns |  3.79 ns |  2.03 | 0.2422 |    4056 B |
| IEnumerable_ToList      | .NET 8.0 |   343.0 ns |  3.64 ns |  3.22 ns |  0.96 | 0.2422 |    4056 B |
| IEnumerable_ToList      | .NET 9.0 |   358.7 ns |  4.86 ns |  4.55 ns |  1.00 | 0.2422 |    4056 B |
|                         |          |            |          |          |       |        |           |
| List                    | .NET 7.0 |   258.4 ns |  4.21 ns |  3.94 ns |  1.22 |      - |         - |
| List                    | .NET 8.0 |   254.6 ns |  4.78 ns |  4.47 ns |  1.20 |      - |         - |
| List                    | .NET 9.0 |   212.1 ns |  2.65 ns |  2.35 ns |  1.00 |      - |         - |
|                         |          |            |          |          |       |        |           |
| List_IEnumerable        | .NET 7.0 | 3,142.4 ns |  8.06 ns |  7.54 ns |  4.24 |      - |      40 B |
| List_IEnumerable        | .NET 8.0 |   692.4 ns |  1.67 ns |  1.30 ns |  0.93 | 0.0019 |      40 B |
| List_IEnumerable        | .NET 9.0 |   741.7 ns |  1.39 ns |  1.09 ns |  1.00 | 0.0019 |      40 B |
|                         |          |            |          |          |       |        |           |
| List_IEnumerable_ToList | .NET 7.0 |   345.6 ns |  3.60 ns |  3.19 ns |  1.05 | 0.2422 |    4056 B |
| List_IEnumerable_ToList | .NET 8.0 |   365.9 ns |  5.52 ns |  4.61 ns |  1.11 | 0.2422 |    4056 B |
| List_IEnumerable_ToList | .NET 9.0 |   328.7 ns |  5.57 ns |  4.94 ns |  1.00 | 0.2422 |    4056 B |
```

## 🏁 Results

- If you iterate through IEnumerable it means that the enumerator must be passed through when looping. This costs time.
- In this example, ToList ensures that materialization takes place before the loop, which is why appropriate allocations are necessary, but the throughput becomes faster.
- By far the fastest way is to treat an already [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) `List`.
- The slowest way is to treat a `List` as IEnumerable, so that the enumerator has to be run again.
- While List behavior has changed virtually nothing in .NET 8, all IEnumerable cases are significantly faster in .NET 8 than in .NET 7.
- Performance has been improved even further with .NET 9.

## Remarks

- Treat a materialized `List` as a `List`
- The return of a `List` should therefore not be `IEnumerable` if possible
- A `ToList` on an already materialized list hiding behind `IEnumerable` creates unnecessary allocations.


> Don't forget: this is not about the best way of `IEnumerable` and `List` in general, but how they are used in everyday life today and what impact this has. `IEnumerable` is good, has its legitimacy and advantages!

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
