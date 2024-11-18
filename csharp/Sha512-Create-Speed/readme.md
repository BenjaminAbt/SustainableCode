# 🌳 Sustainable Code - SHA512 Create 📊

SHA512 is a class whose compute functions cannot in principle be shared across multiple threads: their instances are not thread-safe.

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method   | Runtime  | Mean     | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------- |--------- |---------:|----------:|----------:|------:|-------:|----------:|------------:|
| Create   | .NET 7.0 | 1.309 us | 0.0054 us | 0.0051 us |  1.01 | 0.0172 |     304 B |        0.97 |
| Create   | .NET 8.0 | 1.282 us | 0.0062 us | 0.0058 us |  0.99 | 0.0172 |     304 B |        0.97 |
| Create   | .NET 9.0 | 1.301 us | 0.0056 us | 0.0050 us |  1.00 | 0.0172 |     312 B |        1.00 |
|          |          |          |           |           |       |        |           |             |
| Lock     | .NET 7.0 | 1.090 us | 0.0036 us | 0.0032 us |  1.00 | 0.0095 |     176 B |        1.00 |
| Lock     | .NET 8.0 | 1.070 us | 0.0019 us | 0.0016 us |  0.98 | 0.0095 |     176 B |        1.00 |
| Lock     | .NET 9.0 | 1.092 us | 0.0042 us | 0.0039 us |  1.00 | 0.0095 |     176 B |        1.00 |
|          |          |          |           |           |       |        |           |             |
| HashData | .NET 7.0 | 1.123 us | 0.0091 us | 0.0080 us |  0.99 | 0.0038 |      88 B |        1.00 |
| HashData | .NET 8.0 | 1.123 us | 0.0033 us | 0.0028 us |  1.00 | 0.0038 |      88 B |        1.00 |
| HashData | .NET 9.0 | 1.129 us | 0.0080 us | 0.0074 us |  1.00 | 0.0038 |      88 B |        1.00 |
```

## 🏁 Results

- 🏎️ Using one instance (with `lock`) is faster than constantly recreating
- 🔋 Using the new static `HashData` is still the most sustainable
- There is no performance difference between .NET 7 and .NET 8.

## Remarks

- In newer .NET versions, static functions are available and should be used.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
