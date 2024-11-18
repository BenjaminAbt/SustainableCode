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
    private IEnumerable<int> _collection;
    private List<int> _list;

    [GlobalSetup]
    public void GlobalSetup()
    {
        const int to = 1000;

        _collection = Enumerable.Range(0, to);
        _list = Enumerable.Range(0, to).ToList();
    }


    [Benchmark]
    public int IEnumerable()
    {
        return RunIEnumerable(_collection);
    }

    [Benchmark]
    public int IEnumerable_ToList()
    {
        return RunList(_collection.ToList());
    }

    [Benchmark]
    public int List()
    {
        return RunList(_list);
    }

    [Benchmark]
    public int List_IEnumerable()
    {
        IEnumerable<int> c = _list;
        return RunIEnumerable(c);
    }

    [Benchmark]
    public int List_IEnumerable_ToList()
    {
        IEnumerable<int> c = _list;
        return RunList(c.ToList());
    }



    private static int RunIEnumerable(IEnumerable<int> enumerable)
    {
        int sum = 0;
        foreach (int item in enumerable)
        {
            sum = sum + item;
        }
        return sum;
    }
    private static int RunList(List<int> list)
    {
        int sum = 0;
        foreach (int item in list)
        {
            sum = sum + item;
        }
        return sum;
    }
}

