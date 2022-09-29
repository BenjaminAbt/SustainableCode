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
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100-rc.1.22431.12
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2


|                Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|---------------------- |----------:|---------:|---------:|------:|--------:|
| DateTimeOffset_UtcNow |  29.08 ns | 0.084 ns | 0.075 ns |  1.00 |    0.00 |
|    DateTimeOffset_Now |  96.30 ns | 0.712 ns | 0.632 ns |  3.31 |    0.02 |
|       DateTime_UtcNow |  31.26 ns | 0.254 ns | 0.225 ns |  1.07 |    0.01 |
|          DateTime_Now | 165.81 ns | 1.003 ns | 0.889 ns |  5.70 |    0.04 |

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

This benchmark takes ~1min to run on my machine.
