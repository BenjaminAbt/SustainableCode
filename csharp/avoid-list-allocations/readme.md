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
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

| Method          | Runtime  | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |--------- |---------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| DefaultList     | .NET 7.0 | 4.554 ns | 0.0764 ns | 0.0714 ns |  1.01 |    0.02 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 7.0 | 4.527 ns | 0.0570 ns | 0.0533 ns |  1.00 |    0.02 | 0.0033 |      56 B |        1.00 |
| ListNull        | .NET 7.0 | 1.638 ns | 0.0369 ns | 0.0345 ns |  0.36 |    0.01 | 0.0014 |      24 B |        0.43 |
|                 |          |          |           |           |       |         |        |           |             |
| DefaultList     | .NET 8.0 | 4.484 ns | 0.0569 ns | 0.0533 ns |  0.99 |    0.02 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 8.0 | 4.491 ns | 0.0747 ns | 0.0699 ns |  0.99 |    0.02 | 0.0033 |      56 B |        1.00 |
| ListNull        | .NET 8.0 | 1.575 ns | 0.0137 ns | 0.0122 ns |  0.35 |    0.01 | 0.0014 |      24 B |        0.43 |
|                 |          |          |           |           |       |         |        |           |             |
| DefaultList     | .NET 9.0 | 4.521 ns | 0.0723 ns | 0.0677 ns |  1.00 |    0.02 | 0.0033 |      56 B |        1.00 |
| ListZeroEntries | .NET 9.0 | 4.517 ns | 0.0426 ns | 0.0398 ns |  1.00 |    0.02 | 0.0033 |      56 B |        1.00 |
| ListNull        | .NET 9.0 | 1.643 ns | 0.0630 ns | 0.0590 ns |  0.36 |    0.01 | 0.0014 |      24 B |        0.43 |
```

## 🏁 Results

The results are very descriptive and very clear

- An implementation with the default behavior of List (4 entries) requires by far the most time, performance, memory
- The additional parameter for specifying the number of entries optimizes the performance
- However, the simplification in which the list is only created on demand is by far the best, fastest (~ -70%) and most efficient (~ -67%) solution in all aspects
- With .NET 8, the performance has further improved significantly for the Default, while the allocation remains unchanged

Optimizing list generation is not only incredibly efficient, but thanks to C#'s syntax sugar, it can be implemented without effort.
Therefore, there is no reason not to use this.


## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
