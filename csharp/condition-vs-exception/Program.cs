// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
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
    private const string _test = "this-is-no-guid";

    [Benchmark(Baseline = true)]
    public bool Condition()
    {
        if (IsValidGuid(_test))
        {
            return true;
        }

        return false;
    }

    [Benchmark]
    public bool Exception()
    {
        try
        {
            ThrowIfInvalid(_test);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidGuid(string input)
    {
        return Guid.TryParse(input, out Guid result);
    }

    private static void ThrowIfInvalid(string input)
    {
        if (IsValidGuid(input) is false)
        {
            throw new ArgumentException();

        }
    }
}


