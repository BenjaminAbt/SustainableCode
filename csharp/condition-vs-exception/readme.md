# 🌳 Sustainable Code - Condition vs Exception 📊

Often exceptions are used for program flow, although this is not recommended and violates the [guidelines of exceptions](https://learn.microsoft.com/dotnet/standard/exceptions/best-practices-for-exceptions?WT.mc_id=DT-MVP-5001507) in .NET.

Better alternatives like [Result classes](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.identity.signinresult?view=aspnetcore-7.0&WT.mc_id=DT-MVP-5001507) or [OneOf](https://github.com/mcintyre321/OneOf) are disregarded for convenience.

But what is the impact of using exceptions anyway?

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method    | Job      | Runtime  | Mean         | Error      | StdDev     | Gen0   | Allocated | Alloc Ratio |
|---------- |--------- |--------- |-------------:|-----------:|-----------:|-------:|----------:|------------:|
| Condition | .NET 7.0 | .NET 7.0 |    19.482 ns |  0.3892 ns |  0.3641 ns |      - |         - |          NA |
| Condition | .NET 8.0 | .NET 8.0 |     1.612 ns |  0.0315 ns |  0.0295 ns |      - |         - |          NA |
|           |          |          |              |            |            |        |           |             |
| Exception | .NET 7.0 | .NET 7.0 | 3,547.191 ns | 48.9747 ns | 43.4148 ns | 0.0114 |     232 B |          NA |
| Exception | .NET 8.0 | .NET 8.0 | 2,003.474 ns | 44.8129 ns | 41.9180 ns | 0.0076 |     232 B |          NA |
```

## 🏁 Results

- 🚀 Controlling logic via exceptions is expensive, inefficient and not smart. Doing away with exceptions is almost 500x faster and more efficient.
- With .NET 8.0, performance has improved in both cases, but in the case of Condition, .NET 8 is more than 20 times faster than .NET 7.

## Remarks

- Do not use exceptions to control your logic flow.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
