# 🌳 Sustainable Code - Avoid List Allocations 📊 

Often you see class defintions that always and with every creation a list is initialized by default, although its actual use is not clear, e.g. with APIs, databases, models...

```csharp
public class DefaultListClass
{
    public List<string> MyList { get; set; } = new List<string>();
}
```

The problem here is that in the background of a list always an array is allocated, by default with 4 entries. If these entries are not enough then the list takes care of it internally and copies the array back and forth so that new entries can be added. This is an expensive runtime operation.
The same effect happens when lists are created but not actually used. Therefore this process can be optimized with great effects on performance and thus energy consumption.

## 🔥 Benchmark

```sh

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2251/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100
  [Host]   : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


|          Method |  Runtime |     Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------- |--------- |---------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|     DefaultList | .NET 6.0 | 6.817 ns | 0.1631 ns | 0.1526 ns |  1.00 |    0.00 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 6.0 | 6.501 ns | 0.1510 ns | 0.1338 ns |  0.95 |    0.03 | 0.0033 |      56 B |        1.00 |
|        ListNull | .NET 6.0 | 1.856 ns | 0.0838 ns | 0.0743 ns |  0.27 |    0.01 | 0.0014 |      24 B |        0.43 |
|                 |          |          |           |           |       |         |        |           |             |
|     DefaultList | .NET 7.0 | 7.758 ns | 0.0971 ns | 0.0908 ns |  1.00 |    0.00 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 7.0 | 7.384 ns | 0.0574 ns | 0.0536 ns |  0.95 |    0.01 | 0.0033 |      56 B |        1.00 |
|        ListNull | .NET 7.0 | 2.645 ns | 0.0950 ns | 0.0889 ns |  0.34 |    0.01 | 0.0014 |      24 B |        0.43 |

```

## 🏁 Results

The results are very descriptive and very clear

- An implementation with the default behavior of List (4 entries) requires by far the most time, performance, memory
- The additional parameter for specifying the number of entries optimizes the performance
- However, the simplification in which the list is only created on demand is by far the best, fastest (~ -70%) and most efficient (~ -67%) solution in all aspects


Optimizing list generation is not only incredibly efficient, but thanks to C#'s syntax sugar, it can be implemented without effort. 
Therefore, there is no reason not to use this.


## ⌨️ Run this sample

```shell
dotnet run -c Release
```

As reference, this benchmark takes ~2mins to run on my machine..
