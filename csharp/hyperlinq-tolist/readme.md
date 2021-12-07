# 🌳 Sustainable Code - less allocations (and better performance) with NetFabric.Hyperlinq 📊 

[NetFabric.Hyperlinq](https://github.com/NetFabric/NetFabric.Hyperlinq) is a high performance LINQ implementation with minimal heap allocations. It supports enumerables, async enumerables, arrays and Span<T>.

Visit https://github.com/NetFabric/NetFabric.Hyperlinq

## 🔥 Benchmark

```sh

	.NET SDK=6.0.100-rc.2.21505.57
	  [Host]     : .NET 6.0.0 (6.0.21.48005), X64 RyuJIT
	  DefaultJob : .NET 6.0.0 (6.0.21.48005), X64 RyuJIT


	|    Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Allocated |
	|---------- |---------:|----------:|----------:|---------:|------:|--------:|-------:|-------:|----------:|
	|      Linq | 2.840 us | 0.0568 us | 0.1507 us | 2.898 us |  1.00 |    0.00 | 0.5074 | 0.0153 |      8 KB |
	| HyperLinq | 2.170 us | 0.0345 us | 0.0322 us | 2.178 us |  0.79 |    0.06 | 0.2174 | 0.0038 |      4 KB |

```

## 🏁 Results

- ~20% faster code
- Gen 0 is only one third (-68%)
- Gen 1 is only one fourth (-76%)
- allocations have halved

## ℹ Remarks

I already use HyperLinq in many of my projects and have been able to massively reduce memory consumption while increasing performance.
This has led to us using smaller Azure instances in a variety of projects and services, reducing costs and CO2 consumption.

However, as in any bechnmark, this must be applied reasonably to the individual case in order to achieve improvement.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

As reference, this benchmark takes ~2mins to run (82secs on my machine).