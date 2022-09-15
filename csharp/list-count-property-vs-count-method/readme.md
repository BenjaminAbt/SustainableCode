# 🌳 Sustainable Code - Count() vs Count 📊

These snippets show the basic behavior of Count () and Count.
```

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.1889/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2


|                 Method | Categories | Count |      Mean |     Error |    StdDev |    Median |  Ratio |
|----------------------- |----------- |------ |----------:|----------:|----------:|----------:|-------:|
|  Array_Length_Property |      Array |   100 | 0.0001 ns | 0.0003 ns | 0.0002 ns | 0.0000 ns |   1.00 |
|     Array_Count_Method |      Array |   100 | 5.1354 ns | 0.1115 ns | 0.1043 ns | 5.1693 ns |  51354 |
|                        |            |       |           |           |           |           |        |
|  Array_Length_Property |      Array |  1000 | 0.0334 ns | 0.0161 ns | 0.0150 ns | 0.0355 ns |   1.00 |
|     Array_Count_Method |      Array |  1000 | 5.1734 ns | 0.1075 ns | 0.1005 ns | 5.1928 ns | 221.03 |
|                        |            |       |           |           |           |           |        |
|  Array_Length_Property |      Array | 10000 | 0.0173 ns | 0.0110 ns | 0.0097 ns | 0.0125 ns |   1.00 |
|     Array_Count_Method |      Array | 10000 | 5.1840 ns | 0.1229 ns | 0.1207 ns | 5.1682 ns | 366.07 |
|                        |            |       |           |           |           |           |        |
| HashSet_Count_Property |    HashSet |   100 | 0.0398 ns | 0.0232 ns | 0.0285 ns | 0.0256 ns |   1.00 |
|   HashSet_Count_Method |    HashSet |   100 | 2.4635 ns | 0.0677 ns | 0.1352 ns | 2.4315 ns | 114.00 |
|                        |            |       |           |           |           |           |        |
| HashSet_Count_Property |    HashSet |  1000 | 0.0129 ns | 0.0157 ns | 0.0147 ns | 0.0077 ns |   1.00 |
|   HashSet_Count_Method |    HashSet |  1000 | 2.3950 ns | 0.0514 ns | 0.0455 ns | 2.3900 ns | 185,66 |
|                        |            |       |           |           |           |           |        |
| HashSet_Count_Property |    HashSet | 10000 | 0.0260 ns | 0.0168 ns | 0.0158 ns | 0.0190 ns |   1.00 |
|   HashSet_Count_Method |    HashSet | 10000 | 2.3823 ns | 0.0537 ns | 0.0502 ns | 2.3519 ns | 127.52 |
|                        |            |       |           |           |           |           |        |
|    List_Count_Property |       List |   100 | 0.0211 ns | 0.0143 ns | 0.0134 ns | 0.0126 ns |   1.00 |
|      List_Count_Method |       List |   100 | 2.3773 ns | 0.0479 ns | 0.0448 ns | 2.3836 ns | 166.68 |
|                        |            |       |           |           |           |           |        |
|    List_Count_Property |       List |  1000 | 0.0324 ns | 0.0171 ns | 0.0151 ns | 0.0295 ns |   1.00 |
|      List_Count_Method |       List |  1000 | 2.3827 ns | 0.0570 ns | 0.0533 ns | 2.3451 ns | 102.50 |
|                        |            |       |           |           |           |           |        |
|    List_Count_Property |       List | 10000 | 0.0036 ns | 0.0061 ns | 0.0057 ns | 0.0000 ns |   1.00 |
|      List_Count_Method |       List | 10000 | 2.3807 ns | 0.0556 ns | 0.0520 ns | 2.3887 ns | 661.31 |
```



## 🏁 Results

- The difference is clear: accessing the `Count` property (or `Length` in the case of the array) is much faster - and does not change even if the list contains more entries, more than x300!!!
- Thus, when a [materialized](https://docs.microsoft.com/dotnet/standard/linq/intermediate-materialization?WT.mc_id=DT-MVP-5001507) state exists, the property is always faster.

## Remarks

- While the property is information that is available immediately, `Count()` causes the enumerator to go over all the elements and count - every time.
- The general use of `Count()` is therefore not advisable.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~10mins to run on my machine.
