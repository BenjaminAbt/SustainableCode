// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.ObjectPool;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job)]
public class Benchmark
{
    private List<string> _data;

    [Params(100, 500)]
    public int Lines { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = Enumerable.Range(0, Lines)
            .Select(x => new string('a', x)).ToList();

        _sbPool = ObjectPool.Create<StringBuilder>();
    }

    private ObjectPool<StringBuilder> _sbPool;
    [Benchmark(Baseline = true)]
    public string SB_Pooled()
    {
        // retrive from pool
        StringBuilder sb = _sbPool.Get();

        foreach (string entry in _data)
        {
            string e = entry;
            sb.Append(e);
        }

        string s = sb.ToString();

        // cleanup and return to pool
        sb.Clear();
        _sbPool.Return(sb);

        return s;
    }

    [Benchmark]
    public string SB_NoPool()
    {
        StringBuilder sb = new();

        foreach (string entry in _data)
        {
            string e = entry;
            sb.Append(e);
        }

        return sb.ToString();
    }

    [Benchmark]
    public string ConcatLong()
    {
        string s = string.Empty;

        foreach (string entry in _data)
        {
            s = s + entry;
        }

        return s;
    }

    [Benchmark]
    public string ConcatShort()
    {
        string s = string.Empty;

        foreach (string entry in _data)
        {
            s += entry;
        }

        return s;
    }

    [Benchmark]
    public string ConcatList()
    {
        string s = string.Concat(_data);

        return s;
    }
}
