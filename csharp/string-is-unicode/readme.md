# 🌳 Sustainable Code - String Whitespace Remove 📊

Performance boost through [vectorizing](https://learn.microsoft.com/dotnet/api/system.numerics.vector?view=net-8.0&WT.mc_id=DT-MVP-5001507).

The content check of strings - i.e. the individual chars; not length checks such as NullOrEmpty - benefit immensely from vectorization; a rather advanced technique that is rarely easy to understand.

But vectorization brings an enormous performance boost, provided the hardware provides the appropriate features.\
This example - whether a string contains Unicode chars or not - shows this very clearly.

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5247/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.200-preview.0.24575.35
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 9.0  Runtime=.NET 9.0

| Method              | Mean      | Error     | StdDev    | Median    |
|-------------------- |----------:|----------:|----------:|----------:|
| HasUnicodeCharCheck | 0.0156 ns | 0.0036 ns | 0.0032 ns | 0.0145 ns |
|                     |           |           |           |           |
| HasUnicodeVector    | 0.0012 ns | 0.0020 ns | 0.0017 ns | 0.0007 ns |
```

## 🏁 Results

- Aggressive inlining offers a performance boost in itself
- Vectorization offers an enormous boost compared to the individual char comparison

The vector variant is approx. 10 times faster, although this can vary slightly depending on the hardware, but is never slower.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```
