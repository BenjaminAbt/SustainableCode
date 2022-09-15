// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Benchmark
{
    private List<int> _list;
    private HashSet<int> _hashset;
    private int[] _array;

    [Params(100, 1000, 10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        IEnumerable<int> range = Enumerable.Range(0, Count).ToList();

        _list = range.ToList();
        _hashset = new(range);
        _array = range.ToArray();
    }

    [Benchmark(Baseline = true), BenchmarkCategory("List")]
    public int List_Count_Property() => _list.Count;
    [Benchmark, BenchmarkCategory("List")]
    public int List_Count_Method() => _list.Count();

    [Benchmark(Baseline = true), BenchmarkCategory("HashSet")]
    public int HashSet_Count_Property() => _hashset.Count;
    [Benchmark, BenchmarkCategory("HashSet")]
    public int HashSet_Count_Method() => _hashset.Count();

    [Benchmark(Baseline = true), BenchmarkCategory("Array")]
    public int Array_Length_Property() => _array.Length;
    [Benchmark, BenchmarkCategory("Array")]
    public int Array_Count_Method() => _array.Count();
}

