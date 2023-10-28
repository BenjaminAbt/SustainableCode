# 🌳 Sustainable Code - In Param 📊

This small example should show the effect of in parameter modifier in C#.

Docs:
- [in parameter modifier](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/in-parameter-modifier?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method          | Runtime  | Mean      | Error     | StdDev    | Ratio |
|---------------- |--------- |----------:|----------:|----------:|------:|
| With_InParam    | .NET 7.0 | 0.4264 ns | 0.0247 ns | 0.0219 ns |  2.10 |
| WithOut_InParam | .NET 7.0 | 0.2051 ns | 0.0163 ns | 0.0153 ns |  1.00 |
|                 |          |           |           |           |       |
| With_InParam    | .NET 8.0 | 0.2000 ns | 0.0084 ns | 0.0070 ns |  1.10 |
| WithOut_InParam | .NET 8.0 | 0.1842 ns | 0.0092 ns | 0.0086 ns |  1.00 |
```


## 🏁 Results

- 🚀 In this very simple sample, without `in` is faster

## Remarks

- With `in` the semantic changes and the value is passed by reference.
- This leads to the fact that the runtime must create a copy, which costs time.
- However, there are scenarios, such as large readonly structs, where the in behavior has advantages and should! even be used.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
