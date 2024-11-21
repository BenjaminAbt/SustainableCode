# 🌳 Sustainable Code - String Builder Pooled Performance Comparison 📊

This sample, based on the [Object Pool Pattern](https://docs.microsoft.com/dotnet/api/microsoft.extensions.objectpool.objectpool-1?view=dotnet-plat-ext-5.0&WT.mc_id=DT-MVP-5001507), shows very clearly how the reuse of object instances can massively increase performance while reducing memory consumption.

Sample with ASP.NET Core: [Object reuse with ObjectPool in ASP.NET Core](https://docs.microsoft.com/aspnet/core/performance/objectpool?view=aspnetcore-5.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

For better comparability, additional string concat were added.

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method      | Runtime  | Lines | Mean           | StdDev       | Ratio | Gen0       | Gen1       | Gen2       |
|------------ |--------- |------ |---------------:|-------------:|------:|-----------:|-----------:|-----------:|
| SB_NoPool   | .NET 7.0 | 100   |       863.6 ns |     10.97 ns |  2.22 |     1.6174 |     0.1011 |          - |
| SB_NoPool   | .NET 8.0 | 100   |       764.9 ns |      9.05 ns |  1.96 |     1.6174 |     0.1001 |          - |
| SB_NoPool   | .NET 9.0 | 100   |       750.5 ns |      6.98 ns |  1.93 |     1.6174 |     0.1001 |          - |
|             |          |       |                |              |       |            |            |            |
| SB_Pooled   | .NET 7.0 | 100   |       501.2 ns |      4.79 ns |  1.29 |     0.5922 |          - |          - |
| SB_Pooled   | .NET 8.0 | 100   |       399.0 ns |      5.24 ns |  1.02 |     0.5927 |          - |          - |
| SB_Pooled   | .NET 9.0 | 100   |       389.3 ns |      3.55 ns |  1.00 |     0.5927 |          - |          - |
|             |          |       |                |              |       |            |            |            |
| ConcatShort | .NET 7.0 | 100   |     6,487.4 ns |    115.96 ns | 16.67 |    20.0729 |     0.3586 |          - |
| ConcatShort | .NET 8.0 | 100   |     6,454.2 ns |     92.85 ns | 16.58 |    20.0729 |     0.4425 |          - |
| ConcatShort | .NET 9.0 | 100   |     6,316.7 ns |     93.53 ns | 16.23 |    20.0729 |     0.4425 |          - |
|             |          |       |                |              |       |            |            |            |
| ConcatLong  | .NET 7.0 | 100   |     6,405.4 ns |     73.79 ns | 16.45 |    20.0729 |     0.3586 |          - |
| ConcatLong  | .NET 8.0 | 100   |     6,464.1 ns |    106.38 ns | 16.61 |    20.0729 |     0.4425 |          - |
| ConcatLong  | .NET 9.0 | 100   |     6,298.1 ns |     85.69 ns | 16.18 |    20.0729 |     0.4425 |          - |
|             |          |       |                |              |       |            |            |            |
| ConcatList  | .NET 7.0 | 100   |     1,094.5 ns |      7.94 ns |  2.81 |     0.5951 |          - |          - |
| ConcatList  | .NET 8.0 | 100   |       723.2 ns |      5.26 ns |  1.86 |     0.5960 |          - |          - |
| ConcatList  | .NET 9.0 | 100   |       656.8 ns |      7.05 ns |  1.69 |     0.5960 |          - |          - |
|             |          |       |                |              |       |            |            |            |
|-------------|----------|-------|----------------|--------------|-------|------------|------------|------------|
|             |          |       |                |              |       |            |            |            |
| SB_NoPool   | .NET 7.0 | 500   |    66,533.0 ns |  1,439.03 ns |  1.25 |    76.9043 |    76.9043 |    76.9043 |
| SB_NoPool   | .NET 9.0 | 500   |    61,729.7 ns |    846.96 ns |  1.16 |    76.9043 |    76.9043 |    76.9043 |
| SB_NoPool   | .NET 8.0 | 500   |    64,277.6 ns |  1,435.26 ns |  1.20 |    76.9043 |    76.9043 |    76.9043 |
|             |          |       |                |              |       |            |            |            |
| SB_Pooled   | .NET 7.0 | 500   |    56,434.1 ns |  1,616.89 ns |  1.06 |    76.9043 |    76.9043 |    76.9043 |
| SB_Pooled   | .NET 8.0 | 500   |    54,723.3 ns |  1,105.22 ns |  1.03 |    76.9043 |    76.9043 |    76.9043 |
| SB_Pooled   | .NET 9.0 | 500   |    53,360.1 ns |    421.15 ns |  1.00 |    76.9043 |    76.9043 |    76.9043 |
|             |          |       |                |              |       |            |            |            |
| ConcatShort | .NET 7.0 | 500   | 1,142,275.1 ns | 25,572.21 ns | 21.41 | 10332.0313 | 10332.0313 | 10332.0313 |
| ConcatShort | .NET 8.0 | 500   | 1,028,992.2 ns | 11,862.41 ns | 19.29 | 10332.0313 | 10332.0313 | 10332.0313 |
| ConcatShort | .NET 9.0 | 500   | 1,019,582.2 ns | 14,081.10 ns | 19.11 | 10332.0313 | 10332.0313 | 10332.0313 |
|             |          |       |                |              |       |            |            |            |
| ConcatLong  | .NET 7.0 | 500   | 1,166,315.0 ns | 29,884.12 ns | 21.86 | 10332.0313 | 10332.0313 | 10332.0313 |
| ConcatLong  | .NET 8.0 | 500   | 1,027,497.5 ns | 16,457.49 ns | 19.26 | 10332.0313 | 10332.0313 | 10332.0313 |
| ConcatLong  | .NET 9.0 | 500   | 1,016,028.4 ns | 14,626.49 ns | 19.04 | 10332.0313 | 10332.0313 | 10332.0313 |
|             |          |       |                |              |       |            |            |            |
| ConcatList  | .NET 7.0 | 500   |    64,373.8 ns |  1,292.36 ns |  1.21 |    76.9043 |    76.9043 |    76.9043 |
| ConcatList  | .NET 8.0 | 500   |    60,955.1 ns |    624.88 ns |  1.14 |    76.9043 |    76.9043 |    76.9043 |
| ConcatList  | .NET 9.0 | 500   |    60,583.6 ns |    847.82 ns |  1.14 |    76.9043 |    76.9043 |    76.9043 |
```

## 🏁 Results

- 🔋 StringBuilder itself has the best performance and lowest allocations
- 🐏 No additional memory allocations (only if strings are huge) for the string builder instance
- 🏃‍♀️ The larger the strings, the clearer the performance advantage for pooling.
- 🏎️ Pooling is a performance boost
- 🚀 The ratio clearly shows: without pool up always slower and huge allocations (incl. expensive Gen2) for this sample
- 🎒 ConcatList uses string.concat(IEnumable), which uses the internal type [ValueStringBuilder](https://github.com/dotnet/runtime/blob/46a3bfeffec2fb6b33bfd152d33f33b544e401c9/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs#L193) under the hood, why the allocation is almost the same

## Remarks

- This sample is about pooling, not about string operations in general.
- Pooling is great and powerful
- For strings, there are also more performant solutions with unsafe code.
- If you know the total string size, use string.create to benefit from pre-allocations.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
