# 🌳 Sustainable Code - String Interpolation with C# 10 📊

This small example should show the new string interpolation features with .NET 6 and C# 10

Docs:
- [String Interpolation in C# 10 and .NET 6](https://devblogs.microsoft.com/dotnet/string-interpolation-in-c-10-and-net-6/?WT.mc_id=DT-MVP-5001507)
- [stackalloc](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/stackalloc?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method       | Job      | Runtime  | Mean     | Error    | StdDev   | Gen0   | Allocated |
|------------- |--------- |--------- |---------:|---------:|---------:|-------:|----------:|
| String       | .NET 7.0 | .NET 7.0 | 37.46 ns | 0.785 ns | 1.020 ns | 0.0033 |      56 B |
| String       | .NET 8.0 | .NET 8.0 | 38.10 ns | 0.725 ns | 0.678 ns | 0.0033 |      56 B |
|              |          |          |          |          |          |        |           |
| StringCreate | .NET 7.0 | .NET 7.0 | 23.99 ns | 0.506 ns | 0.562 ns | 0.0033 |      56 B |
| StringCreate | .NET 8.0 | .NET 8.0 | 21.19 ns | 0.431 ns | 0.403 ns | 0.0033 |      56 B |
```

## 🏁 Results

- 🔋 Both have the same allocation stats
- 🚀 The new `string.Create` interpolation feature is ~30% faster!
- The performance of .NET 7 and .NET 8 has not changed significantly.

## Remarks

- The `string.Create` API is quite hard to understand
- `stackalloc` is still safe during runtime, but the size must be checked!

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
