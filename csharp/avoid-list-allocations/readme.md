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
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method          | Runtime  | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |--------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| DefaultList     | .NET 7.0 | 15.216 ns | 1.0117 ns | 2.9831 ns |  1.00 |    0.00 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 7.0 |  9.602 ns | 0.2333 ns | 0.3193 ns |  0.67 |    0.15 | 0.0033 |      56 B |        1.00 |
| ListNull        | .NET 7.0 |  3.622 ns | 0.1116 ns | 0.1044 ns |  0.22 |    0.04 | 0.0014 |      24 B |        0.43 |
|                 |          |           |           |           |       |         |        |           |             |
| DefaultList     | .NET 8.0 |  9.992 ns | 0.4168 ns | 1.2290 ns |  1.00 |    0.00 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 8.0 | 10.199 ns | 0.4187 ns | 1.2344 ns |  1.04 |    0.21 | 0.0033 |      56 B |        1.00 |
| ListNull        | .NET 8.0 |  3.369 ns | 0.0438 ns | 0.0409 ns |  0.36 |    0.04 | 0.0014 |      24 B |        0.43 |
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

## Updates

- 2023/11 - Add .NET 8
