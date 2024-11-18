// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job, Column.Median)]
public class Benchmark
{
    // Class

    [Benchmark]
    [ArgumentsSource(nameof(Class_Arguments))]
    public MyClass? Class_Condition(MyClass? myClass, string title)
    {
        if (myClass is not null) { return myClass; }
        return null;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Class_Arguments))]
    public MyClass? Class_PatternMatching(MyClass? myClass, string title)
    {
        if (myClass is MyClass val) { return val; }
        return null;
    }

    public IEnumerable<object[]> Class_Arguments()
    {
        yield return new object[] { new MyClass(), "not null" };
        yield return new object[] { null, "null" };
    }

    // Struct

    [Benchmark]
    [ArgumentsSource(nameof(Struct_Arguments))]
    public MyStruct? Struct_HasValue(MyStruct? myStruct, string title)
    {
        if (myStruct.HasValue) { return myStruct; }
        return null;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Struct_Arguments))]
    public MyStruct? Struct_PatterMatching(MyStruct? myStruct, string title)
    {
        if (myStruct is MyStruct val) { return val; }
        return null;
    }

    public IEnumerable<object[]> Struct_Arguments()
    {
        yield return new object[] { new MyStruct(), "not null" };
        yield return new object[] { null, "null" };
    }
}

public class MyClass { }
public readonly struct MyStruct { }

