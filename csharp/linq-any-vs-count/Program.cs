// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
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
    private List<int> _list;

    [Params(0, 1, 10, 100)]
    public int Items { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        IEnumerable<int> range = Enumerable.Range(0, Items);

        _list = range.ToList();
    }

    [Benchmark(Baseline = true)]
    public bool Any() => _list.Any();

    [Benchmark]
    public bool Count() => _list.Count > 0;
}
