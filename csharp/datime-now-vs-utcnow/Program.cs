// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job, Column.Median)]
public class Benchmark
{
    [Benchmark(Baseline = true)]
    public DateTimeOffset DateTimeOffset_UtcNow() => DateTimeOffset.UtcNow;
    [Benchmark]
    public DateTimeOffset DateTimeOffset_Now() => DateTimeOffset.Now;


    [Benchmark]
    public DateTimeOffset DateTime_UtcNow() => DateTime.UtcNow;
    [Benchmark]
    public DateTimeOffset DateTime_Now() => DateTime.Now;
}

