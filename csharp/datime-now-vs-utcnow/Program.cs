// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
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

