# 🌳 Sustainable Code - Struct vs Class 📊

This small example should show how struct and class behave in .NET at runtime, and why struct should be used more often - where it makes sense.

Docs:
- [Structures](https://docs.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct?WT.mc_id=DT-MVP-5001507)
- [Classes](https://docs.microsoft.com/dotnet/csharp/fundamentals/types/classes?WT.mc_id=DT-MVP-5001507)
## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1415 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


|       Method |      Mean |    Error |    StdDev |  Gen 0 | Allocated |
|------------- |----------:|---------:|----------:|-------:|----------:|
|  SmallStruct |  39.16 ns | 0.069 ns |  0.061 ns |      - |         - |
| MediumStruct |  39.30 ns | 0.254 ns |  0.238 ns |      - |         - |
|   SmallClass | 215.30 ns | 3.048 ns |  2.994 ns | 0.1433 |   2,400 B |
|  MediumClass | 487.58 ns | 9.713 ns | 15.959 ns | 0.2389 |   4,000 B |
```



## 🏁 Results

- 🔋 Both struct samples produce no allocations!
- 🚀 Struct has a better performance over all!

## Remarks

- The use of struct or class is part of the software architecture.
- Not in all scenarios struct makes sense or can be used (e.g. serialization)!
- The change from class to struct is a breaking change!
- It's all a matter of perspective, whether these are small or large samples here - names are smoke and mirrors.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark runs several minutes (1:47min on my workstation)
