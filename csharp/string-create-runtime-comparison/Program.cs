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
[HideColumns(Column.Job, Column.Median)]
public class Benchmark
{
    public const string A = "Batman";
    public const string B = " and ";
    public const string C = "Robin ";

    [Benchmark]
    public string String_Concat()
        => A + B + C;


    [Benchmark]
    public string String_Interpolate()
        => $"{A}{B}{C}";


    [Benchmark]
    public string String_Create()
    {
        int length = A.Length + B.Length + C.Length;

        string myString = string.Create(length, (A, B, C), (chars, state) =>
        {
            int position = 0;

            // a
            state.A.AsSpan().CopyTo(chars);
            position += state.A.Length;

            // b
            state.B.AsSpan().CopyTo(chars.Slice(position));
            position += state.B.Length; // update the position

            // c
            state.C.AsSpan().CopyTo(chars.Slice(position));
            position += state.C.Length; // update the position

        });

        return myString;
    }

}

