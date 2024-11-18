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
[HideColumns(Column.Job)]
public class Benchmark
{
    private List<int> _list;
    private HashSet<int> _hashset;
    private IEnumerable<int> _ienumerable;

    [Params(1000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        IEnumerable<int> range = Enumerable.Range(0, Count);

        _list = new List<int>(range);
        _hashset = new HashSet<int>(range);
        _ienumerable = new List<int>(range);
    }

    [Benchmark(Baseline = true), BenchmarkCategory("List")]
    public int List_Count_Property() => _list.Count;

    [Benchmark, BenchmarkCategory("List")]
    public int List_Count_Method() => _list.Count();

    [Benchmark, BenchmarkCategory("HashSet")]
    public int HashSet_Count_Property() => _hashset.Count;

    [Benchmark, BenchmarkCategory("HashSet")]
    public int HashSet_Count_Method() => _hashset.Count();

    public int IEnumerable_Count_Method() => _ienumerable.Count();
}

