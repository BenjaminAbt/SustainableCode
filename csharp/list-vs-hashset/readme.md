# 🌳 Sustainable Code - List vs. HashSet 📊

This small example should show the performance comparison between List and HashSet

Docs:
- [List](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [HashSet](https://docs.microsoft.com/dotnet/api/system.collections.generic.hashset-1?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.300-preview.22204.3
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


|       Method | Count |        Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------- |------ |------------:|----------:|----------:|-------:|-------:|----------:|
|    ListWrite |    10 |    27.95 ns |  0.533 ns |  0.499 ns | 0.0057 |      - |      96 B |
| HashSetWrite |    10 |    65.16 ns |  1.283 ns |  1.798 ns | 0.0176 |      - |     296 B |
| ListDistinct |    10 |   183.01 ns |  3.544 ns |  4.081 ns | 0.0353 |      - |     592 B |
|    ListWrite |   100 |   195.04 ns |  3.795 ns |  5.066 ns | 0.0272 |      - |     456 B |
| HashSetWrite |   100 |   565.76 ns |  5.673 ns |  5.307 ns | 0.1087 |      - |   1,832 B |
| ListDistinct |   100 | 1,305.11 ns | 19.237 ns | 17.994 ns | 0.1698 |      - |   2,848 B |
|    ListWrite |   500 | 1,030.15 ns | 20.278 ns | 27.070 ns | 0.1221 |      - |   2,056 B |
| HashSetWrite |   500 | 2,718.36 ns | 21.541 ns | 17.987 ns | 0.5035 | 0.0153 |   8,456 B |
| ListDistinct |   500 | 6,173.36 ns | 38.981 ns | 36.463 ns | 0.7553 | 0.0229 |  12,672 B |
```



## 🏁 Results

- 🚀 HashSet is a collection that ensures that each element exists only once and thus no manual distinct has to be made. However, this is bought by a performance loss and allocations when inserting the elements!

## Remarks

- The more elements to write, the higher the performance loss
- It must be considered how relevant a distinct is and which quantity should be compared
- Use the HashSet only if you really need to ensure that each element exists only once

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

This benchmark takes ~3.5mins to run on my machine.
