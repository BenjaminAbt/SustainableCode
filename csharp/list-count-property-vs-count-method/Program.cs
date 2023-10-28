// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Benchmark
{
    private List<int> _list;
    private HashSet<int> _hashset;
    private IEnumerable<int> _ienumerable;

    [Params(1000, 10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        IEnumerable<int> range = Enumerable.Range(0, Count);

        _list = range.ToList();
        _hashset = new(range);
        _ienumerable = range;
    }

    [Benchmark(Baseline = true), BenchmarkCategory("List")]
    public int List_Count_Property() => _list.Count;
    [Benchmark, BenchmarkCategory("List")]
    public int List_Count_Method() => _list.Count();

    [Benchmark(Baseline = true), BenchmarkCategory("HashSet")]
    public int HashSet_Count_Property() => _hashset.Count;
    [Benchmark, BenchmarkCategory("HashSet")]
    public int HashSet_Count_Method() => _hashset.Count();

    public int IEnumerable_Count_Method() => _ienumerable.Count();
}

