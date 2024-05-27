// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Buffers;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.ObjectPool;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    public const string Input = @"""
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        Hello\u0001World Hello\u0001World Hello\u0001World Hello\u0001World
        """;

    [Benchmark(Baseline = true)]
    public string StringBuilder_Pool() => Cleanups.UsingStringBuilderPool(Input);

    [Benchmark]
    public string StringBuilder_Instance() => Cleanups.UsingStringBuilderInstance(Input);

    [Benchmark]
    public string Linq() => Cleanups.UsingLinq(Input);

    [Benchmark]
    public ReadOnlySpan<char> Span() => Cleanups.UsingSpan(Input);
}

public static class Cleanups
{
    public static string UsingStringBuilderPool(string source)
    {
        StringBuilder sb = StringBuilderPool.Get();
        try
        {
            foreach (char c in source)
            {
                if (char.IsControl(c) is false)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
        finally
        {
            StringBuilderPool.ReturnDirty(sb);
        }
    }

    public static string UsingStringBuilderInstance(string source)
    {
        StringBuilder sb = new();

        foreach (char c in source)
        {
            if (char.IsControl(c) is false)
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }


    public static string UsingLinq(string source)
        => string.Concat(source.Where(c => char.IsControl(c) is false));

    public static ReadOnlySpan<char> UsingSpan(string source)
    {
        int length = source.Length;
        char[]? rentedFromPool = null;

        // allocate
        Span<char> buffer = length > 512 ?
            (rentedFromPool = ArrayPool<char>.Shared.Rent(length)) : (stackalloc char[512]);

        // filter
        int index = 0;
        foreach (char c in source)
        {
            if (char.IsControl(c) is false)
            {
                buffer[index] = c;
                index++;
            }
        }

        // only return the data that was written
        string data = buffer.Slice(0, index).ToString();

        // cleanup
        if (rentedFromPool is not null)
        {
            ArrayPool<char>.Shared.Return(rentedFromPool, clearArray: true);
        }

        return data;
    }
}


public static class StringBuilderPool
{
    private static readonly ObjectPool<StringBuilder> s_pool
        = ObjectPool.Create<StringBuilder>();

    public static StringBuilder Get()
        => s_pool.Get();

    public static void Return(StringBuilder stringBuilder)
        => s_pool.Return(stringBuilder);

    public static void ReturnDirty(StringBuilder stringBuilder)
    {
        stringBuilder.Clear();
        Return(stringBuilder);
    }
}
