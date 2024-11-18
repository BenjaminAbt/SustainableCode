# 🌳 Sustainable Code - Struct vs Class with Boxing 📊

This small example should show how struct and class behave in .NET at runtime, and why struct with interfaces are less efficient.

Docs:
- [Structures](https://docs.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct?WT.mc_id=DT-MVP-5001507)
- [Classes](https://docs.microsoft.com/dotnet/csharp/fundamentals/types/classes?WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method              | Runtime  | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated |
|-------------------- |--------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|
| Class               | .NET 7.0 | 2.5567 ns | 0.0362 ns | 0.0339 ns |  1.63 |    0.02 | 0.0014 |      24 B |
| Class               | .NET 8.0 | 1.6313 ns | 0.0331 ns | 0.0276 ns |  1.04 |    0.02 | 0.0014 |      24 B |
| Class               | .NET 9.0 | 1.5723 ns | 0.0120 ns | 0.0100 ns |  1.00 |    0.01 | 0.0014 |      24 B |
|                     |          |           |           |           |       |         |        |           |
| ClassWithInterface  | .NET 7.0 | 2.5173 ns | 0.0331 ns | 0.0294 ns |  1.59 |    0.02 | 0.0014 |      24 B |
| ClassWithInterface  | .NET 8.0 | 1.6194 ns | 0.0389 ns | 0.0364 ns |  1.03 |    0.02 | 0.0014 |      24 B |
| ClassWithInterface  | .NET 9.0 | 1.5783 ns | 0.0123 ns | 0.0115 ns |  1.00 |    0.01 | 0.0014 |      24 B |
|                     |          |           |           |           |       |         |        |           |
| Struct              | .NET 7.0 | 0.0102 ns | 0.0065 ns | 0.0055 ns |     ? |       ? |      - |         - |
| Struct              | .NET 8.0 | 0.0009 ns | 0.0007 ns | 0.0005 ns |     ? |       ? |      - |         - |
| Struct              | .NET 9.0 | 0.0013 ns | 0.0018 ns | 0.0016 ns |     ? |       ? |      - |         - |
|                     |          |           |           |           |       |         |        |           |
| StructWithInterface | .NET 7.0 | 2.6001 ns | 0.0371 ns | 0.0347 ns |  1.63 |    0.03 | 0.0014 |      24 B |
| StructWithInterface | .NET 8.0 | 1.6315 ns | 0.0241 ns | 0.0226 ns |  1.02 |    0.02 | 0.0014 |      24 B |
| StructWithInterface | .NET 9.0 | 1.5944 ns | 0.0167 ns | 0.0140 ns |  1.00 |    0.01 | 0.0014 |      24 B |
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
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
