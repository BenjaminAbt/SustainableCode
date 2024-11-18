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
    [Benchmark]
    public MyClass Class()
        => new("Batman");

    [Benchmark]
    public IName ClassWithInterface()
        => new MyClassWithInterface("Batman");

    [Benchmark]
    public MyStruct Struct()
        => new("Batman");

    [Benchmark]
    public IName StructWithInterface()
        => new MyStructWithInterface("Batman");
}

public readonly record struct MyStruct(string Name);
public readonly record struct MyStructWithInterface(string Name) : IName;
public record class MyClass(string Name);
public record class MyClassWithInterface(string Name) : IName;


public interface IName { string Name { get; } }
