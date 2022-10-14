// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
public class Benchmark
{
    [Benchmark]
    public MyClass? Class_NotNull()
    {
        MyClass? myClass = new();
        if (myClass is not null) { return myClass; }
        return null;
    }

    [Benchmark]
    public MyClass? Class_Null()
    {
        MyClass? myClass = null;
        if (myClass is not null) { return myClass; }
        return null;
    }

    [Benchmark]
    public MyStruct? Struct_HasValue_Value()
    {
        MyStruct? myStruct = new();
        if (myStruct.HasValue) { return myStruct; }
        return null;
    }

    [Benchmark]
    public MyStruct? Struct_HasValue_Null()
    {
        MyStruct? myStruct = null;
        if (myStruct.HasValue) { return myStruct; }
        return null;
    }

    [Benchmark]
    public MyStruct? Struct_PatterMatching_Value()
    {
        MyStruct? myStruct = new();
        if (myStruct is MyStruct val) { return val; }
        return null;
    }

    [Benchmark]
    public MyStruct? Struct_PatterMatching_Null()
    {
        MyStruct? myStruct = null;
        if (myStruct is MyStruct val) { return val; }
        return null;
    }
}

public class MyClass { }
public readonly struct MyStruct { }

