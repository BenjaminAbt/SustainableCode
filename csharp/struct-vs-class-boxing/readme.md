# 🌳 Sustainable Code - Struct vs Class with Boxing 📊

This small example should show how struct and class behave in .NET at runtime, and why struct with interfaces are less efficient.

Docs:
- [Structures](https://docs.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct?WT.mc_id=DT-MVP-5001507)
- [Classes](https://docs.microsoft.com/dotnet/csharp/fundamentals/types/classes?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method              | Job      | Runtime  | Mean      | Error     | StdDev    | Median    | Gen0   | Allocated |
|-------------------- |--------- |--------- |----------:|----------:|----------:|----------:|-------:|----------:|
| Class               | .NET 7.0 | .NET 7.0 | 3.9827 ns | 0.1245 ns | 0.1332 ns | 3.9282 ns | 0.0014 |      24 B |
| Class               | .NET 8.0 | .NET 8.0 | 3.0353 ns | 0.1014 ns | 0.1041 ns | 3.0008 ns | 0.0014 |      24 B |
|                     |          |          |           |           |           |           |        |           |
| Struct              | .NET 7.0 | .NET 7.0 | 0.0165 ns | 0.0200 ns | 0.0187 ns | 0.0059 ns |      - |         - |
| Struct              | .NET 8.0 | .NET 8.0 | 0.0096 ns | 0.0147 ns | 0.0130 ns | 0.0032 ns |      - |         - |
|                     |          |          |           |           |           |           |        |           |
| ClassWithInterface  | .NET 7.0 | .NET 7.0 | 4.0388 ns | 0.1193 ns | 0.1276 ns | 3.9751 ns | 0.0014 |      24 B |
| ClassWithInterface  | .NET 8.0 | .NET 8.0 | 3.2790 ns | 0.1087 ns | 0.1787 ns | 3.2270 ns | 0.0014 |      24 B |
|                     |          |          |           |           |           |           |        |           |
| StructWithInterface | .NET 7.0 | .NET 7.0 | 4.0907 ns | 0.1203 ns | 0.1338 ns | 4.0992 ns | 0.0014 |      24 B |
| StructWithInterface | .NET 8.0 | .NET 8.0 | 3.3699 ns | 0.1103 ns | 0.1651 ns | 3.3568 ns | 0.0014 |      24 B |
```

## 🏁 Results

- 🚀 structs have by far the best efficiency when declared as readonly and used without an interface.
- 🔋 as soon as an interface is involved, structs have no more advantages due to boxing
- All tests are about 30% faster in .NET 8 than in .NET 7.

## Remarks

- In principle, this behavior is nothing new. Potential solutions are already being worked ([dotnet/csharplang - Proposal: Allow readonly ref structs to implement interfaces, but disallow boxing them #1479](https://github.com/dotnet/csharplang/discussions/1479)) on so that structs can be used boxing-free.

## Info

- [Boxing and Unboxing (C# Programming Guide)](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing?WT.mc_id=DT-MVP-5001507)

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
