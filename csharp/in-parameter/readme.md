# 🌳 Sustainable Code - In Param 📊

This small example should show the effect of in parameter modifier in C#.

Docs:
- [in parameter modifier](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/in-parameter-modifier?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.300-preview.22204.3
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


|          Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Allocated |
|---------------- |----------:|----------:|----------:|------:|--------:|----------:|
|    With_InParam | 0.5647 ns | 0.0139 ns | 0.0123 ns |  3.50 |    0.28 |         - |
| WithOut_InParam | 0.1610 ns | 0.0124 ns | 0.0116 ns |  1.00 |    0.00 |         - |

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

This benchmark runs 1:02min on my workstation.
