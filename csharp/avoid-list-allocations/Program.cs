// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
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
    public List<string> MyList { get; set; } = new(); // 4 entries by default
}

public class ListZeroEntriesClass
{
    public List<string> MyList { get; set; } = new(0);
}

public class ListNullClass
{
    private List<string>? _myList = null;
    public List<string> MyList
    {
        get => _myList ??= new();
        set => _myList = value;
    }
}

