// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job)]
public class Benchmark
{
    [Benchmark(Baseline = true)]
    public List<int> List_Ctor() => new();

    [Benchmark]
    public List<int> List_Ctor_0() => new(0);

    [Benchmark]
    public List<int> List_Recommended() => [];

    [Benchmark]
    public int[] Array_Empty() => Array.Empty<int>();

    [Benchmark]
    public int[] Array_Recommended() => [];

    [Benchmark]
    public HashSet<int> HashSet_Ctor() => new();

    [Benchmark]
    public HashSet<int> HashSet_Recommended() => [];
}
