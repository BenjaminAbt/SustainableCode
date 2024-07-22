# 🌳 Sustainable Code - the cost of returning an empty collection

The return of empty collections is practically part of every code. But which variants cost how much - and wouldn't zero be even better?

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4651/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100-preview.5.24307.3
  [Host]   : .NET 9.0.0 (9.0.24.30607), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.30607), X64 RyuJIT AVX2


| Method      | Runtime  | Mean      | Ratio | Gen0   | Allocated |
|------------ |--------- |----------:|------:|-------:|----------:|
| New         | .NET 9.0 | 3.0654 ns |  1.00 | 0.0019 |      32 B |
| New(0)      | .NET 9.0 | 3.1433 ns |  1.02 | 0.0019 |      32 B |
| []          | .NET 9.0 | 3.0596 ns |  1.00 | 0.0019 |      32 B |
| Array.Empty | .NET 9.0 | 0.4346 ns |  0.14 |      - |         - |
```

## 🏁 Remarks

- Array.Empty is by far the most efficient and fastest variant - but is the only method in this example that also returns an array
- All other variants are almost identical, whereby `[]` corresponds to the latest and currently recommended notation.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```
