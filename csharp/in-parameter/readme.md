# 🌳 Sustainable Code - In Param 📊

This small example should show the effect of in parameter modifier in C#.

Docs:
- [in parameter modifier](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/in-parameter-modifier?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method          | Runtime  | Mean      | Error     | StdDev    | Ratio |
|---------------- |--------- |----------:|----------:|----------:|------:|
| With_InParam    | .NET 7.0 | 0.1816 ns | 0.0043 ns | 0.0040 ns |  0.88 |
| With_InParam    | .NET 8.0 | 0.2057 ns | 0.0011 ns | 0.0008 ns |  0.99 |
| With_InParam    | .NET 9.0 | 0.2069 ns | 0.0023 ns | 0.0018 ns |  1.00 |
|                 |          |           |           |           |       |
| WithOut_InParam | .NET 7.0 | 0.2052 ns | 0.0014 ns | 0.0012 ns |  0.95 |
| WithOut_InParam | .NET 8.0 | 0.2223 ns | 0.0070 ns | 0.0062 ns |  1.03 |
| WithOut_InParam | .NET 9.0 | 0.2152 ns | 0.0011 ns | 0.0009 ns |  1.00 |
```


## 🏁 Results

- With `in` the semantic changes and the value is passed by reference.
- This leads to the fact that the runtime must create a copy, which costs time.
- However, there are scenarios, such as large readonly structs, where the in behavior has advantages and should! even be used.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
