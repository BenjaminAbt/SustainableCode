# 🌳 Sustainable Code - String.Create vs ValueStringBuilder 📊 

- Internal [ValueStringBuilder](https://github.com/dotnet/runtime/blob/cabb8b089fd3d84fc46446c2079ddc1981b55fd9/src/libraries/Common/src/System/Text/ValueStringBuilder.cs#L11)
- [String.Create](https://learn.microsoft.com/en-us/dotnet/api/system.string.create?WT.mc_id=DT-MVP-5001507)

See more here

- https://github.com/dotnet/runtime/issues/25587
- https://github.com/dotnet/runtime/issues/50389
- https://github.com/dotnet/runtime/issues/25587#issuecomment-662076796


## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                  | Runtime  | Mean      | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
|------------------------ |--------- |----------:|----------:|----------:|------:|----------:|------------:|
| StringCreate_Case       | .NET 7.0 | 10.116 ns | 0.1257 ns | 0.1114 ns |  1.04 |     112 B |        1.00 |
| StringCreate_Case       | .NET 8.0 |  9.652 ns | 0.0441 ns | 0.0391 ns |  0.99 |     112 B |        1.00 |
| StringCreate_Case       | .NET 9.0 |  9.746 ns | 0.0674 ns | 0.0631 ns |  1.00 |     112 B |        1.00 |
|                         |          |           |           |           |       |           |             |
| StringJoin_Case         | .NET 7.0 | 14.972 ns | 0.1940 ns | 0.1720 ns |  1.46 |     104 B |        1.86 |
| StringJoin_Case         | .NET 8.0 | 12.909 ns | 0.0862 ns | 0.0806 ns |  1.26 |     104 B |        1.86 |
| StringJoin_Case         | .NET 9.0 | 10.236 ns | 0.0691 ns | 0.0612 ns |  1.00 |      56 B |        1.00 |
|                         |          |           |           |           |       |           |             |
| ValueStringBuilder_Case | .NET 7.0 | 21.547 ns | 0.1208 ns | 0.1071 ns |  1.40 |      56 B |        1.00 |
| ValueStringBuilder_Case | .NET 8.0 | 23.029 ns | 0.0900 ns | 0.0841 ns |  1.49 |      56 B |        1.00 |
| ValueStringBuilder_Case | .NET 9.0 | 15.417 ns | 0.0803 ns | 0.0712 ns |  1.00 |      56 B |        1.00 |
```


## ℹ Remarks

The ValueStringBuilder is a replacement for the StringBuilder, which as a reference type has corresponding disadvantages in the allocation of memory, especially in scenarios where very many instances are created. The ValueStringBuilder is intended to compensate for this disadvantage.

However, due to its base as a struct, it simply cannot be treated as a reference type, so it remains an internal class until further notice to avoid misapplication.

String.Create remains by far the most performant way to create strings for short strings. The ValueStringBuilder as well as the StringBuilder have advantages for larger strings or certain programming scenarios.

The performance difference has not changed significantly between .NET 7 and .NET 8.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
