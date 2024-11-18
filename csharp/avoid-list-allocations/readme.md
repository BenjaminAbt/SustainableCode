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


| Method          | Runtime  | Mean     | Error     | StdDev    | Ratio | Allocated |
|---------------- |--------- |---------:|----------:|----------:|------:|----------:|
| DefaultList     | .NET 7.0 | 4.562 ns | 0.0575 ns | 0.0538 ns |  1.01 |      56 B |
| DefaultList     | .NET 8.0 | 4.585 ns | 0.0482 ns | 0.0451 ns |  1.01 |      56 B |
| DefaultList     | .NET 9.0 | 4.533 ns | 0.0430 ns | 0.0403 ns |  1.00 |      56 B |
|                 |          |          |           |           |       |           |
| ListZeroEntries | .NET 7.0 | 4.611 ns | 0.0739 ns | 0.0691 ns |  0.99 |      56 B |
| ListZeroEntries | .NET 8.0 | 4.484 ns | 0.0369 ns | 0.0345 ns |  0.97 |      56 B |
| ListZeroEntries | .NET 9.0 | 4.643 ns | 0.0443 ns | 0.0393 ns |  1.00 |      56 B |
|                 |          |          |           |           |       |           |
| ListNull        | .NET 7.0 | 1.683 ns | 0.0139 ns | 0.0123 ns |  1.00 |      24 B |
| ListNull        | .NET 8.0 | 1.650 ns | 0.0170 ns | 0.0159 ns |  0.98 |      24 B |
| ListNull        | .NET 9.0 | 1.681 ns | 0.0447 ns | 0.0396 ns |  1.00 |      24 B |
```

## 🏁 Results

The results are very descriptive and very clear

- It is no surprise that the lazy implementation generates less allocation.
- Therefore, it is also no surprise that in the median the lazy implementation is more performant.
- There are no real differences between `List()` and `List(0)` anymore.
- There are no measurable differences between the various runtime versions.

Optimizing list generation is not only incredibly efficient, but thanks to C#'s syntax sugar, it can be implemented without effort.
Therefore, there is no reason not to use this.


## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
