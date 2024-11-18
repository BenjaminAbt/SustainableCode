// Made by Benjamin Abt - https://github.com/BenjaminAbt

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
    private string Hello = "Hello"; // no const for benchmarking
    private string World = "World"; // no const for benchmarking

    [Benchmark]
    public string String() => $"[{Hello}][{World}]";


    [Benchmark]
    public string StringCreate() => string.Create(null, stackalloc char[128], $"[{Hello}][{World}]");
}
