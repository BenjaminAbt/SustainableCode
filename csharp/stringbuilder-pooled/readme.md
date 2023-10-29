# 🌳 Sustainable Code - String Builder Pooled Performance Comparison 📊

This sample, based on the [Object Pool Pattern](https://docs.microsoft.com/dotnet/api/microsoft.extensions.objectpool.objectpool-1?view=dotnet-plat-ext-5.0&WT.mc_id=DT-MVP-5001507), shows very clearly how the reuse of object instances can massively increase performance while reducing memory consumption.

Sample with ASP.NET Core: [Object reuse with ObjectPool in ASP.NET Core](https://docs.microsoft.com/aspnet/core/performance/objectpool?view=aspnetcore-5.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

For better comparability, additional string concat were added.

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method      | Runtime  | Lines | Mean           | Ratio | Gen0       | Gen1       | Gen2       | Allocated   |
|------------ |--------- |------ |---------------:|------:|-----------:|-----------:|-----------:|------------:|
| SB_Pooled   | .NET 7.0 | 100   |       867.4 ns |  1.00 |     0.5913 |          - |          - |      9.7 KB |
| SB_NoPool   | .NET 7.0 | 100   |     1,431.0 ns |  1.69 |     1.6174 |     0.1011 |          - |     26.4 KB |
| ConcatLong  | .NET 7.0 | 100   |    12,328.6 ns | 14.28 |    20.0653 |     0.3510 |          - |   327.88 KB |
| ConcatShort | .NET 7.0 | 100   |    10,770.4 ns | 12.72 |    20.0653 |     0.3510 |          - |   327.88 KB |
| ConcatList  | .NET 7.0 | 100   |     1,644.9 ns |  2.00 |     0.5951 |          - |          - |     9.73 KB |
|             |          |       |                |       |            |            |            |             |
| SB_Pooled   | .NET 8.0 | 100   |       654.6 ns |  1.00 |     0.5922 |          - |          - |      9.7 KB |
| SB_NoPool   | .NET 8.0 | 100   |     1,198.5 ns |  1.92 |     1.6174 |     0.1011 |          - |     26.4 KB |
| ConcatLong  | .NET 8.0 | 100   |     9,919.4 ns | 15.11 |    20.0653 |     0.4425 |          - |   327.88 KB |
| ConcatShort | .NET 8.0 | 100   |     9,753.1 ns | 15.26 |    20.0653 |     0.4425 |          - |   327.88 KB |
| ConcatList  | .NET 8.0 | 100   |     1,262.2 ns |  1.95 |     0.5951 |          - |          - |     9.73 KB |
|             |          |       |                |       |            |            |            |             |
| SB_Pooled   | .NET 7.0 | 500   |    75,360.1 ns |  1.00 |    76.9043 |    76.9043 |    76.9043 |   243.71 KB |
| SB_NoPool   | .NET 7.0 | 500   |    92,728.5 ns |  1.23 |    76.9043 |    76.9043 |    76.9043 |   495.84 KB |
| ConcatLong  | .NET 7.0 | 500   | 2,297,847.4 ns | 31.02 | 10332.0313 | 10332.0313 | 10332.0313 | 40705.49 KB |
| ConcatShort | .NET 7.0 | 500   | 2,118,874.2 ns | 28.65 | 10332.0313 | 10332.0313 | 10332.0313 | 40705.49 KB |
| ConcatList  | .NET 7.0 | 500   |    98,613.0 ns |  1.31 |    76.9043 |    76.9043 |    76.9043 |   243.74 KB |
|             |          |       |                |       |            |            |            |             |
| SB_Pooled   | .NET 8.0 | 500   |    81,122.2 ns |  1.00 |    76.9043 |    76.9043 |    76.9043 |   243.71 KB |
| SB_NoPool   | .NET 8.0 | 500   |    95,604.5 ns |  1.21 |    76.9043 |    76.9043 |    76.9043 |   495.84 KB |
| ConcatLong  | .NET 8.0 | 500   | 2,044,008.7 ns | 25.37 | 10332.0313 | 10332.0313 | 10332.0313 | 40705.49 KB |
| ConcatShort | .NET 8.0 | 500   | 1,990,827.4 ns | 24.98 | 10332.0313 | 10332.0313 | 10332.0313 | 40705.49 KB |
| ConcatList  | .NET 8.0 | 500   |    97,124.3 ns |  1.23 |    76.9043 |    76.9043 |    76.9043 |   243.74 KB |
```

## 🏁 Results

- 🔋 StringBuilder itself has the best performance and lowest allocations
- 🐏 No additional memory allocations (only if strings are huge) for the string builder instance
- 🏃‍♀️ The larger the strings, the clearer the performance advantage for pooling.
- 🏎️ Pooling is a performance boost
- 🚀 The ratio clearly shows: without pool up always slower and huge allocations (incl. expensive Gen2) for this sample
- 🎒 ConcatList uses string.concat(IEnumable), which uses the internal type [ValueStringBuilder](https://github.com/dotnet/runtime/blob/46a3bfeffec2fb6b33bfd152d33f33b544e401c9/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs#L193) under the hood, why the allocation is almost the same
- The differences between .NET 7 and .NET 8 are in the measurement tolerances.

## Remarks

- This sample is about pooling, not about string operations in general.
- Pooling is great and powerful
- For strings, there are also more performant solutions with unsafe code.
- If you know the total string size, use string.create to benefit from pre-allocations.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
