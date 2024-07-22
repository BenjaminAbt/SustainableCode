// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class Benchmark
{
    [Benchmark(Baseline = true)]
    public List<int> New_Ctor() => new();

    [Benchmark]
    public List<int> New_0() => new(0);

    [Benchmark]
    public List<int> New() => [];

    [Benchmark]
    public int[] Array_Empty() => Array.Empty<int>();

    [Benchmark]
    public int[] Array_Empty_2() => [];
}
