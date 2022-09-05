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
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT


|                  Method |       Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------------ |-----------:|---------:|---------:|-------:|-------:|----------:|
|             IEnumerable | 2,936.9 ns | 57.07 ns | 88.85 ns |      - |      - |      40 B |
|      IEnumerable_ToList | 1,453.0 ns |  6.90 ns |  5.77 ns | 0.2422 | 0.0019 |   4,056 B |
|                    List |   655.7 ns |  5.16 ns |  4.83 ns |      - |      - |         - |
|        List_IEnumerable | 4,287.2 ns |  8.81 ns |  7.35 ns |      - |      - |      40 B |
| List_IEnumerable_ToList |   820.2 ns | 10.92 ns |  9.68 ns | 0.2422 | 0.0029 |   4,056 B |
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

This benchmark takes ~2mins to run on my machine.
