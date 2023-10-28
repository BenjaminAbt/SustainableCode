# 🌳 Sustainable Code - Enumerate List vs. IEnumerable  📊

This code example shows the different behavior of runs of IEnumerable and List. 

> This example has no claim to how lists are handled in the very very best case, but **how they are currently handled in everyday life**.

- [List - msdocs](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [IEnumeranle - msdocs](https://docs.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)

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
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method                  | Job      | Runtime  | Mean       | Error    | StdDev   | Gen0   | Allocated |
|------------------------ |--------- |--------- |-----------:|---------:|---------:|-------:|----------:|
| List                    | .NET 7.0 | .NET 7.0 |   438.3 ns |  8.39 ns |  8.24 ns |      - |         - |
| List                    | .NET 8.0 | .NET 8.0 |   436.8 ns |  8.75 ns |  9.36 ns |      - |         - |
| List_IEnumerable_ToList | .NET 7.0 | .NET 7.0 |   636.6 ns | 12.54 ns | 14.93 ns | 0.2422 |    4056 B |
| List_IEnumerable_ToList | .NET 8.0 | .NET 8.0 |   599.2 ns | 11.94 ns | 22.73 ns | 0.2422 |    4056 B |
| IEnumerable_ToList      | .NET 7.0 | .NET 7.0 | 1,447.5 ns | 27.83 ns | 29.78 ns | 0.2422 |    4056 B |
| IEnumerable_ToList      | .NET 8.0 | .NET 8.0 |   611.3 ns | 12.08 ns | 26.01 ns | 0.2422 |    4056 B |
| IEnumerable             | .NET 7.0 | .NET 7.0 | 2,645.1 ns | 48.93 ns | 48.05 ns |      - |      40 B |
| IEnumerable             | .NET 8.0 | .NET 8.0 |   532.0 ns | 10.43 ns |  9.76 ns | 0.0019 |      40 B |
| List_IEnumerable        | .NET 7.0 | .NET 7.0 | 4,232.0 ns | 83.24 ns | 89.07 ns |      - |      40 B |
| List_IEnumerable        | .NET 8.0 | .NET 8.0 | 1,071.4 ns | 20.83 ns | 20.45 ns | 0.0019 |      40 B |
```

## 🏁 Results

- If you iterate through IEnumerable it means that the enumerator must be passed through when looping. This costs time.
- In this example, ToList ensures that materialization takes place before the loop, which is why appropriate allocations are necessary, but the throughput becomes faster.
- By far the fastest way is to treat an already [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) `List`.
- The slowest way is to treat a `List` as IEnumerable, so that the enumerator has to be run again.

## Remarks

- Treat a materialized `List` as a `List`
- The return of a `List` should therefore not be `IEnumerable` if possible
- A `ToList` on an already materialized list hiding behind `IEnumerable` creates unnecessary allocations.


> Don't forget: this is not about the best way of `IEnumerable` and `List` in general, but how they are used in everyday life today and what impact this has. `IEnumerable` is good, has its legitimacy and advantages!

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
