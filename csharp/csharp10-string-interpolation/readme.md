# 🌳 Sustainable Code - String Interpolation with C# 10 📊

This small example should show the new string interpolation features with .NET 6 and C# 10

Docs:
- [String Interpolation in C# 10 and .NET 6](https://devblogs.microsoft.com/dotnet/string-interpolation-in-c-10-and-net-6/?WT.mc_id=DT-MVP-5001507)
- [stackalloc](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/stackalloc?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method       | Runtime  | Mean      | StdDev    | Ratio | Allocated |
|------------- |--------- |----------:|----------:|------:|----------:|
| String       | .NET 7.0 | 20.872 ns | 0.0560 ns |  1.48 |      56 B |
| String       | .NET 8.0 | 22.092 ns | 0.0347 ns |  1.57 |      56 B |
| String       | .NET 9.0 | 14.058 ns | 0.0338 ns |  1.00 |      56 B |
|              |          |           |           |       |           |
| StringCreate | .NET 7.0 | 11.206 ns | 0.0374 ns |  1.18 |      56 B |
| StringCreate | .NET 8.0 | 10.074 ns | 0.0426 ns |  1.06 |      56 B |
| StringCreate | .NET 9.0 |  9.524 ns | 0.1118 ns |  1.00 |      56 B |
```

## 🏁 Results

- 🔋 Both have the same allocation stats.
- 🚀 The new `string.Create` interpolation feature is way faster.
- With .NET 9 we got a performance boost.

## Remarks

- The `string.Create` API is quite hard to understand
- `stackalloc` is still safe during runtime, but the size must be checked!

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
