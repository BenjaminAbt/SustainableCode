# 🌳 Sustainable Code - Condition vs Exception 📊

Often exceptions are used for program flow, although this is not recommended and violates the [guidelines of exceptions](https://learn.microsoft.com/dotnet/standard/exceptions/best-practices-for-exceptions?WT.mc_id=DT-MVP-5001507) in .NET.

Better alternatives like [Result classes](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.identity.signinresult?view=aspnetcore-7.0&WT.mc_id=DT-MVP-5001507) or [OneOf](https://github.com/mcintyre321/OneOf) are disregarded for convenience.

But what is the impact of using exceptions anyway?

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method    | Runtime  | Mean         | Error     | StdDev    | Ratio    | RatioSD | Gen0   | Allocated |
|---------- |--------- |-------------:|----------:|----------:|---------:|--------:|-------:|----------:|
| Condition | .NET 7.0 |    11.495 ns | 0.0733 ns | 0.0612 ns |    10.30 |    0.06 |      - |         - |
| Condition | .NET 8.0 |     1.299 ns | 0.0041 ns | 0.0034 ns |     1.16 |    0.00 |      - |         - |
| Condition | .NET 9.0 |     1.116 ns | 0.0035 ns | 0.0031 ns |     1.00 |    0.00 |      - |         - |
| Exception | .NET 7.0 | 2,336.653 ns | 9.1249 ns | 8.5355 ns | 2,092.87 |    9.28 | 0.0114 |     232 B |
| Exception | .NET 8.0 | 2,303.988 ns | 2.6537 ns | 2.0719 ns | 2,063.61 |    5.80 | 0.0114 |     232 B |
| Exception | .NET 9.0 | 1,072.247 ns | 8.2707 ns | 7.3317 ns |   960.38 |    6.84 | 0.0134 |     224 B |
```

## 🏁 Results

- 🚀 Controlling logic via exceptions is expensive, inefficient and not smart. Doing away with exceptions is almost 500x faster and more efficient.
- With .NET 8, performance has improved in both cases, but in the case of Condition, .NET 8 is more than 20 times faster than .NET 7.
- With .NET 9, exception handling has become significantly more performant.

## Remarks

- Do not use exceptions to control your logic flow.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
