// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    private int[] _data;

    [Params(10, 100, 500)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = Enumerable.Range(0, Count).ToArray();
    }

    [Benchmark]
    public int ListWrite()
    {
        List<int> list = new(Count);
        Fill(list);

        return list.Count;
    }

    [Benchmark]
    public int HashSetWrite()
    {
        HashSet<int> hashSet = new(Count);
        Fill(hashSet);

        return hashSet.Count;
    }

    [Benchmark]
    public int ListDistinct()
    {
        List<int> list = new(Count);
        Fill(list);
        list.Distinct().ToList();

        return list.Count;
    }

    public void Fill(ICollection<int> source)
    {
        foreach (int i in _data) { source.Add(i); }
    }
}
