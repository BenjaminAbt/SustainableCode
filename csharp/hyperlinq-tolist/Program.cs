// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using NetFabric.Hyperlinq;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job)]
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
