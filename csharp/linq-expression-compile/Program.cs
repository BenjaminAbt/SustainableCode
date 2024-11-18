// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    private IQueryable<Person> _persons;

    [GlobalSetup]
    public void Init()
    {
        _persons = new List<Person> { new("Batman", 81), new("Superman", 9998), new("Catwoman", 23) }
                    .AsQueryable(); // This is what EF Core uses;
    }

    private static readonly Expression<Func<Person, bool>> s_ageExpression = e => e.Age == 23;
    private static readonly Func<Person, bool> s_ageExpressionCompiled = s_ageExpression.Compile();

    [Benchmark]
    public Person Ex() => _persons.Where(s_ageExpression).Single();

    [Benchmark(Baseline = true)]
    public Person Ex_Compiled() => _persons.Where(s_ageExpressionCompiled).Single();
}

public record class Person(string Name, int Age);

