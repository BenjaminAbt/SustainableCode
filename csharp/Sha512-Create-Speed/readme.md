# 🌳 Sustainable Code - SHA512 Create 📊

SHA512 is a class whose compute functions cannot in principle be shared across multiple threads: their instances are not thread-safe.

## 🔥 Benchmark

```sh
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19045.3208)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=8.0.100-preview.3.23178.7
  [Host]     : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2


|   Method |     Mean |     Error |    StdDev |   Gen0 | Allocated |
|--------- |---------:|----------:|----------:|-------:|----------:|
|   Create | 1.730 us | 0.0115 us | 0.0096 us | 0.0172 |     304 B |
|     Lock | 1.536 us | 0.0214 us | 0.0190 us | 0.0095 |     176 B |
| HashData | 1.600 us | 0.0190 us | 0.0177 us | 0.0038 |      88 B |
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

This benchmark runs 1 minuten on my workstation.
