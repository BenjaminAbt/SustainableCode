# 🌳 Sustainable Code - String.Create vs ValueStringBuilder 📊 

- Internal [ValueStringBuilder](https://github.com/dotnet/runtime/blob/cabb8b089fd3d84fc46446c2079ddc1981b55fd9/src/libraries/Common/src/System/Text/ValueStringBuilder.cs#L11)
- [String.Create](https://learn.microsoft.com/en-us/dotnet/api/system.string.create?view=net-6.0&WT.mc_id=DT-MVP-5001507)

See more here

- https://github.com/dotnet/runtime/issues/25587
- https://github.com/dotnet/runtime/issues/50389
- https://github.com/dotnet/runtime/issues/25587#issuecomment-662076796


## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2251/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100
  [Host]   : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


|                  Method |  Runtime |     Mean |    Error |   StdDev |   Gen0 | Allocated |
|------------------------ |--------- |---------:|---------:|---------:|-------:|----------:|
|       StringCreate_Case | .NET 6.0 | 13.79 ns | 0.157 ns | 0.147 ns | 0.0067 |     112 B |
|         StringJoin_Case | .NET 6.0 | 20.32 ns | 0.197 ns | 0.174 ns | 0.0062 |     104 B |
| ValueStringBuilder_Case | .NET 6.0 | 36.38 ns | 0.170 ns | 0.159 ns | 0.0033 |      56 B |
|       StringCreate_Case | .NET 7.0 | 15.59 ns | 0.133 ns | 0.124 ns | 0.0067 |     112 B |
|         StringJoin_Case | .NET 7.0 | 23.33 ns | 0.275 ns | 0.257 ns | 0.0062 |     104 B |
| ValueStringBuilder_Case | .NET 7.0 | 36.70 ns | 0.393 ns | 0.348 ns | 0.0033 |      56 B |

```


## ℹ Remarks

The ValueStringBuilder is a replacement for the StringBuilder, which as a reference type has corresponding disadvantages in the allocation of memory, especially in scenarios where very many instances are created. The ValueStringBuilder is intended to compensate for this disadvantage.

However, due to its base as a struct, it simply cannot be treated as a reference type, so it remains an internal class until further notice to avoid misapplication.

String.Create remains by far the most performant way to create strings for short strings. The ValueStringBuilder as well as the StringBuilder have advantages for larger strings or certain programming scenarios.


## ⌨️ Run this sample

```shell
dotnet run -c Release
```

As reference, this benchmark takes ~2mins to run.
