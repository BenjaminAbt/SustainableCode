// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    private const string _chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

    [Benchmark]
    public string ToUpper() => _chars.ToUpper();
    [Benchmark]
    public string ToUpperInvariant() => _chars.ToUpperInvariant();
    [Benchmark]
    public string ToLower() => _chars.ToLower();
    [Benchmark]
    public string ToLowerInvariant() => _chars.ToLowerInvariant();
}

