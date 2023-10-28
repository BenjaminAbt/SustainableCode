// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    private readonly Consumer consumer = new();

    [Benchmark]
    public void List() => new List<string>().Consume(consumer);

    [Benchmark]
    public void Array() => new string[] { }.Consume(consumer);

    [Benchmark]
    public void ArrayEmpty() => System.Array.Empty<string>().Consume(consumer);

    [Benchmark]
    public void EnumerableEmpty() => Enumerable.Empty<string>().Consume(consumer);
}
