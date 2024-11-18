// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Buffers;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.ObjectPool;

#if DEBUG
Benchmark bench = new();
Console.WriteLine(bench.StringBuilder_Pool());
Console.WriteLine(bench.StringBuilder_Instance());
Console.WriteLine(bench.Linq());
Console.WriteLine(bench.Span());
Console.WriteLine(bench.Span2());
Console.WriteLine(bench.Span2Unsafe());
#else
BenchmarkDotNet.Running.BenchmarkRunner.Run<Benchmark>();
#endif

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job)]
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

    [Benchmark]
    public string StringBuilder_Pool()
        => Cleanups.UsingStringBuilderPool(Input);

    [Benchmark]
    public string StringBuilder_Instance()
        => Cleanups.UsingStringBuilderInstance(Input);

    [Benchmark]
    public string Linq()
        => Cleanups.UsingLinq(Input);

    [Benchmark]
    public string Span()
        => Cleanups.UsingSpan(Input);

    [Benchmark]
    public string Span1()
        => Cleanups.UseSpan1(Input);

    [Benchmark]
    public string Span2()
        => Cleanups.UseSpan2(Input);

    [Benchmark]
    public string Span2Unsafe()
        => Cleanups.UseSpan2Unsafe(Input);
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

    public static string UsingSpan(string source)
    {
        int length = source.Length;
        char[] rentedFromPool = null;

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

    [SkipLocalsInit]
    public static string UseSpan1(ReadOnlySpan<char> source)
    {
        int len = source.Length;
        char[] rentedFromPool = null;

        Span<char> buffer = len <= 512
            ? stackalloc char[512]
            : rentedFromPool = ArrayPool<char>.Shared.Rent(len);

        int idx = 0;
        foreach (char c in source)
        {
            if (!char.IsControl(c))
            {
                buffer[idx++] = c;
            }
        }

        buffer = buffer.Slice(0, idx);

        if (rentedFromPool is not null)
        {
            buffer.Clear();
            ArrayPool<char>.Shared.Return(rentedFromPool);
        }

        return buffer.ToString();
    }

    [SkipLocalsInit]
    public static string UseSpan2(ReadOnlySpan<char> source)
    {
        if (source.Length <= 512)
        {
            (string result, _) = Core(source, stackalloc char[512]);
            return result;
        }

        return UsePool(source);

        static (string, int) Core(ReadOnlySpan<char> source, Span<char> buffer)
        {
            int idx = 0;
            foreach (char c in source)
            {
                if (!char.IsControl(c))
                {
                    buffer[idx++] = c;
                }
            }

            string result = buffer.Slice(0, idx).ToString();
            return (result, idx);
        }

        static string UsePool(ReadOnlySpan<char> source)
        {
            char[] rentedFromPool = ArrayPool<char>.Shared.Rent(source.Length);

            (string result, int length) = Core(source, rentedFromPool);

            rentedFromPool.AsSpan(0, length).Clear();
            ArrayPool<char>.Shared.Return(rentedFromPool);

            return result;
        }
    }

    [SkipLocalsInit]
    public static string UseSpan2Unsafe(ReadOnlySpan<char> source)
    {
        if (source.Length <= 512)
        {
            (string result, _) = Core(source, stackalloc char[512]);
            return result;
        }

        return UsePool(source);

        static (string, int) Core(ReadOnlySpan<char> source, Span<char> buffer)
        {
            Debug.Assert(buffer.Length >= source.Length);

            ref char ptr = ref MemoryMarshal.GetReference(buffer);
            int idx = 0;

            foreach (char c in source)
            {
                if (!char.IsControl(c))
                {
                    Unsafe.Add(ref ptr, (uint)idx) = c;
                    idx++;
                }
            }

            string result = buffer.Slice(0, idx).ToString();
            return (result, idx);
        }

        static string UsePool(ReadOnlySpan<char> source)
        {
            char[] rentedFromPool = ArrayPool<char>.Shared.Rent(source.Length);

            (string result, int length) = Core(source, rentedFromPool);

            rentedFromPool.AsSpan(0, length).Clear();
            ArrayPool<char>.Shared.Return(rentedFromPool);

            return result;
        }
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
