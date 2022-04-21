// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    private string Hello = "Hello"; // no const for benchmarking
    private string World = "World"; // no const for benchmarking

    [Benchmark]
    public string String() => $"[{Hello}][{World}]";


    [Benchmark]
    public string StringCreate() => string.Create(null, stackalloc char[128], $"[{Hello}][{World}]");
}
