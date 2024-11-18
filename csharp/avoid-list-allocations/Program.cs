// Made by Benjamin Abt - https://github.com/BenjaminAbt

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
[HideColumns(Column.Job, Column.Median)]
public class Benchmark
{
    [Benchmark(Baseline = true)]
    public DefaultListClass DefaultList() => new();

    [Benchmark]
    public ListZeroEntriesClass ListZeroEntries() => new();

    [Benchmark]
    public ListNullClass ListNull() => new();
}

public class DefaultListClass
{
    public List<string> MyList { get; set; } = []; // 4 entries by default
}

public class ListZeroEntriesClass
{
    public List<string> MyList { get; set; } = [];
}

public class ListNullClass
{
    private List<string> _myList;

    public List<string> MyList
    {
        get => _myList ??= [];
        set => _myList = value;
    }
}

