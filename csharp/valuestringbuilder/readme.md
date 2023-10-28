# 🌳 Sustainable Code - String.Create vs ValueStringBuilder 📊 

- Internal [ValueStringBuilder](https://github.com/dotnet/runtime/blob/cabb8b089fd3d84fc46446c2079ddc1981b55fd9/src/libraries/Common/src/System/Text/ValueStringBuilder.cs#L11)
- [String.Create](https://learn.microsoft.com/en-us/dotnet/api/system.string.create?view=net-6.0&WT.mc_id=DT-MVP-5001507)

See more here

- https://github.com/dotnet/runtime/issues/25587
- https://github.com/dotnet/runtime/issues/50389
- https://github.com/dotnet/runtime/issues/25587#issuecomment-662076796


## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method                  | Runtime  | Error    | StdDev   | Gen0   | Allocated |
|------------------------ |--------- |---------:|---------:|-------:|----------:|
| StringCreate_Case       | .NET 7.0 | 0.368 ns | 0.492 ns | 0.0067 |     112 B |
| StringCreate_Case       | .NET 8.0 | 0.257 ns | 0.241 ns | 0.0067 |     112 B |
| StringJoin_Case         | .NET 7.0 | 0.466 ns | 0.572 ns | 0.0062 |     104 B |
| StringJoin_Case         | .NET 8.0 | 0.465 ns | 0.620 ns | 0.0062 |     104 B |
|                         |          |          |          |        |           |
| ValueStringBuilder_Case | .NET 7.0 | 0.749 ns | 0.701 ns | 0.0033 |      56 B |
| ValueStringBuilder_Case | .NET 8.0 | 0.714 ns | 0.668 ns | 0.0033 |      56 B |
```


## ℹ Remarks

The ValueStringBuilder is a replacement for the StringBuilder, which as a reference type has corresponding disadvantages in the allocation of memory, especially in scenarios where very many instances are created. The ValueStringBuilder is intended to compensate for this disadvantage.

However, due to its base as a struct, it simply cannot be treated as a reference type, so it remains an internal class until further notice to avoid misapplication.

String.Create remains by far the most performant way to create strings for short strings. The ValueStringBuilder as well as the StringBuilder have advantages for larger strings or certain programming scenarios.


## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
