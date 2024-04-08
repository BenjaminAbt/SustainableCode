// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class Benchmark
{
    // Prepare
    private List<int> _list;

    [Params(100)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _list = Enumerable.Range(0, Count).ToList();
    }

    // Benchmarks
    [Benchmark(Baseline = true)]
    public List<DemoContainer> SelectToList() => _list.Select(Map).ToList();


    [Benchmark]
    public List<DemoContainer> ConvertAll() => _list.ConvertAll(Map);

    // Map
    public readonly record struct DemoContainer(int Data);

    public static DemoContainer Map(int data) => new(data);
}

