# 🌳 Sustainable Code - SHA512 Create 📊

SHA512 is a class whose compute functions cannot in principle be shared across multiple threads: their instances are not thread-safe.

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method   | Job      | Runtime  | Mean     | Error     | StdDev    | Gen0   | Allocated |
|--------- |--------- |--------- |---------:|----------:|----------:|-------:|----------:|
| Create   | .NET 7.0 | .NET 7.0 | 1.836 us | 0.0336 us | 0.0314 us | 0.0172 |     304 B |
| Create   | .NET 8.0 | .NET 8.0 | 1.816 us | 0.0215 us | 0.0201 us | 0.0172 |     304 B |
|          |          |          |          |           |           |        |           |
| Lock     | .NET 7.0 | .NET 7.0 | 1.579 us | 0.0275 us | 0.0244 us | 0.0095 |     176 B |
| Lock     | .NET 8.0 | .NET 8.0 | 1.576 us | 0.0128 us | 0.0120 us | 0.0095 |     176 B |
|          |          |          |          |           |           |        |           |
| HashData | .NET 7.0 | .NET 7.0 | 1.647 us | 0.0208 us | 0.0194 us | 0.0038 |      88 B |
| HashData | .NET 8.0 | .NET 8.0 | 1.618 us | 0.0146 us | 0.0122 us | 0.0038 |      88 B |
```

## 🏁 Results

- 🏎️ Using one instance (with `lock`) is faster than constantly recreating
- 🔋 Using the new static `HashData` is still the most sustainable

## Remarks

- In newer .NET versions, static functions are available and should be used.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
