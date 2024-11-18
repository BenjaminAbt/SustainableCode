# 🌳 Sustainable Code - DateTimeOffset vs. DateTime  📊

Difference of DateTimeOffset vs. DateTime

The big lack of `DateTime`, which was also recognized early in .NET 1.0, is that it is not clear from the DateTime information which time zone the time information represents. Therefore `DateTime` is also called `implicit representation of time information`, whose "hope" is that the time information is always in relation to UTC-0. `DateTime` cannot guarantee this, which is why errors often occur in combination with time zones and `DateTime`.

Since the problems of `DateTime` were recognized early, there was a much better alternative in the form of `DateTimeOffset`, which has been the recommended variant since .NET 1.1. Probably because `DateTime` appears in IntelliSense earlier than `DateTimeOffset`, this is often used despite the massive deficits and the large error potential of `DateTime`.

Docs:
- [DateTimeOffset.UtcNow](https://learn.microsoft.com/de-de/dotnet/api/system.datetimeoffset.utcnow?view=net-6.0view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [DateTimeOffset.Now](https://learn.microsoft.com/de-de/dotnet/api/system.datetimeoffset.now?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [DateTime.UtcNow](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.utcnow?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)
- [DateTime.Now](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.utcnow?view=net-6.0?view=net-6.0&WT.mc_id=DT-MVP-5001507)

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                | Runtime  | Mean     | Error    | StdDev   | Ratio | RatioSD |
|---------------------- |--------- |---------:|---------:|---------:|------:|--------:|
| DateTimeOffset_UtcNow | .NET 7.0 | 22.68 ns | 0.045 ns | 0.040 ns |  1.13 |    0.00 |
| DateTimeOffset_Now    | .NET 7.0 | 62.67 ns | 0.189 ns | 0.168 ns |  3.13 |    0.01 |
| DateTime_UtcNow       | .NET 7.0 | 24.50 ns | 0.145 ns | 0.136 ns |  1.22 |    0.01 |
| DateTime_Now          | .NET 7.0 | 97.97 ns | 0.588 ns | 0.550 ns |  4.90 |    0.03 |
|                       |          |          |          |          |       |         |
| DateTimeOffset_UtcNow | .NET 8.0 | 19.95 ns | 0.037 ns | 0.035 ns |  1.00 |    0.00 |
| DateTimeOffset_Now    | .NET 8.0 | 50.25 ns | 0.171 ns | 0.160 ns |  2.51 |    0.01 |
| DateTime_UtcNow       | .NET 8.0 | 22.14 ns | 0.060 ns | 0.053 ns |  1.11 |    0.00 |
| DateTime_Now          | .NET 8.0 | 74.43 ns | 0.150 ns | 0.140 ns |  3.72 |    0.01 |
|                       |          |          |          |          |       |         |
| DateTimeOffset_UtcNow | .NET 9.0 | 20.00 ns | 0.066 ns | 0.062 ns |  1.00 |    0.00 |
| DateTimeOffset_Now    | .NET 9.0 | 51.31 ns | 0.113 ns | 0.106 ns |  2.57 |    0.01 |
| DateTime_UtcNow       | .NET 9.0 | 22.08 ns | 0.041 ns | 0.037 ns |  1.10 |    0.00 |
| DateTime_Now          | .NET 9.0 | 79.21 ns | 0.227 ns | 0.190 ns |  3.96 |    0.02 |

```

## 🏁 Results

- 🚀 `UtcNow` is way faster than `Now`, because no timezone allignment is necessary
- 🚀 `DateTimeOffset` is faster than `DateTime`
- The performance is roughly the same across all versions.

## Remarks

- In an application, time information should always be treated as time zone neutral (i.e. UTC).
- The adjustment to the time zone should be done during the visualization of the time information (e.g. in the UI).
- The explicit way should always be preferred to an implicit one: therefore in .NET - whenever possible - `DateTimeOffset` should be used.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
