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
    private const string _chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

    [Benchmark]
    public string ToUpper()
        => _chars.ToUpper();

    [Benchmark]
    public string ToUpperInvariant()
        => _chars.ToUpperInvariant();

    [Benchmark]
    public string ToLower()
        => _chars.ToLower();

    [Benchmark]
    public string ToLowerInvariant()
        => _chars.ToLowerInvariant();
}

