# 🌳 Sustainable Code - Struct vs Class with Boxing 📊

This small example should show how struct and class behave in .NET at runtime, and why struct with interfaces are less efficient.

Docs:
- [Structures](https://docs.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct?WT.mc_id=DT-MVP-5001507)
- [Classes](https://docs.microsoft.com/dotnet/csharp/fundamentals/types/classes?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2486/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.200-preview.22628.1
  [Host]     : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2


|              Method |      Mean |     Error |    StdDev |    Median |   Gen0 | Allocated |
|-------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
|               Class | 2.9964 ns | 0.0713 ns | 0.0667 ns | 3.0374 ns | 0.0014 |      24 B |
|  ClassWithInterface | 3.0590 ns | 0.1043 ns | 0.0976 ns | 3.0742 ns | 0.0014 |      24 B |
|              Struct | 0.0018 ns | 0.0030 ns | 0.0023 ns | 0.0012 ns |      - |         - |
| StructWithInterface | 3.1241 ns | 0.0835 ns | 0.0781 ns | 3.1147 ns | 0.0014 |      24 B |
```



## 🏁 Results

- 🚀 structs have by far the best efficiency when declared as readonly and used without an interface.
- 🔋 as soon as an interface is involved, structs have no more advantages due to boxing

## Remarks

- In principle, this behavior is nothing new. Potential solutions are already being worked ([dotnet/csharplang - Proposal: Allow readonly ref structs to implement interfaces, but disallow boxing them #1479](https://github.com/dotnet/csharplang/discussions/1479)) on so that structs can be used boxing-free.

## Info

- [Boxing and Unboxing (C# Programming Guide)](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing?WT.mc_id=DT-MVP-5001507)

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark runs several minutes (1:42min on my workstation)
