// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    [Benchmark]
    public MyClass Class() => new MyClass("Batman");

    [Benchmark]
    public IName ClassWithInterface() => new MyClassWithInterface("Batman");

    [Benchmark]
    public MyStruct Struct() => new MyStruct("Batman");

    [Benchmark]
    public IName StructWithInterface() => new MyStructWithInterface("Batman");
}

public readonly record struct MyStruct(string Name);
public readonly record struct MyStructWithInterface(string Name) : IName;
public record class MyClass(string Name);
public record class MyClassWithInterface(string Name) : IName;


public interface IName { string Name { get; } }
