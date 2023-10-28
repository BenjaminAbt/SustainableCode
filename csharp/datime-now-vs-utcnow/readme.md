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
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2


| Method                | Runtime  | Mean      | Error    | StdDev   | Ratio | RatioSD |
|---------------------- |--------- |----------:|---------:|---------:|------:|--------:|
| DateTimeOffset_UtcNow | .NET 7.0 |  29.25 ns | 0.256 ns | 0.227 ns |  1.00 |    0.00 |
| DateTimeOffset_Now    | .NET 7.0 |  97.67 ns | 1.710 ns | 1.600 ns |  3.35 |    0.07 |
| DateTime_UtcNow       | .NET 7.0 |  30.24 ns | 0.277 ns | 0.231 ns |  1.03 |    0.01 |
| DateTime_Now          | .NET 7.0 | 148.96 ns | 2.050 ns | 1.917 ns |  5.09 |    0.04 |
|                       |          |           |          |          |       |         |
| DateTimeOffset_UtcNow | .NET 8.0 |  23.90 ns | 0.025 ns | 0.019 ns |  1.00 |    0.00 |
| DateTimeOffset_Now    | .NET 8.0 |  72.49 ns | 1.222 ns | 1.143 ns |  3.04 |    0.05 |
| DateTime_UtcNow       | .NET 8.0 |  27.11 ns | 0.521 ns | 0.487 ns |  1.13 |    0.02 |
| DateTime_Now          | .NET 8.0 | 114.42 ns | 1.317 ns | 1.232 ns |  4.80 |    0.05 |

```

## 🏁 Results

- 🚀 `UtcNow` is way faster than `Now`, because no timezone allignment is necessary
- 🚀 `DateTimeOffset` is faster than `DateTime`

## Remarks

- In an application, time information should always be treated as time zone neutral (i.e. UTC).
- The adjustment to the time zone should be done during the visualization of the time information (e.g. in the UI).
- The explicit way should always be preferred to an implicit one: therefore in .NET - whenever possible - `DateTimeOffset` should be used.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8
