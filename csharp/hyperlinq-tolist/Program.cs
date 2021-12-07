using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NetFabric.Hyperlinq;
using System.Collections.Generic;
using System.Linq;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    private List<int> _data;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = Enumerable.Range(0, 1000).ToList();
    }


    [Benchmark(Baseline = true)]
    public List<int> SystemLinqWhere()
    {
        return _data
            .Where(x => x > 100)
            .ToList();
    }

    [Benchmark]
    public List<int> HyperLinqWhere()
    {
        return _data
            .AsValueEnumerable()
            .Where(x => x > 100)
            .ToList();
    }
}
