// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;


BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
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
    public string Regex()
    {
        return RegexSample.WhiteSpaceRegex().Replace(Input, "");
    }

    [Benchmark]
    public string String()
    {
        string data = Input;

        return data.Replace(" ", "");
    }

    [Benchmark]
    public string Span()
    {
        ReadOnlySpan<char> inputSpan = Input.AsSpan();
        Span<char> resultSpan = stackalloc char[Input.Length];
        int resultIndex = 0;

        foreach (char c in inputSpan)
        {
            if (c is not ' ')
            {
                resultSpan[resultIndex++] = c;
            }
        }

        return new string(resultSpan.Slice(0, resultIndex));
    }

    [Benchmark]
    public string StringBuilder()
    {
        StringBuilder stringBuilder = new(Input);
        stringBuilder.Replace(" ", "");

        return stringBuilder.ToString();
    }

    [Benchmark]
    public string JoinSplit()
    {
        return string.Join("", Input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
    }

    [Benchmark]
    public string ConcatSplit()
    {
        return string.Concat(Input.Split(null));
    }

    [Benchmark]
    public string SpanArrayPool()
    {
        char[] pooledArray = ArrayPool<char>.Shared.Rent(Input.Length);
        try
        {
            Span<char> destination = pooledArray.AsSpan(0, Input.Length);

            int pos = 0;

            foreach (char c in Input)
            {
                if (!char.IsWhiteSpace(c))
                {
                    destination[pos++] = c;
                }
            }

            return Input.Length == pos ? Input : new string(destination[..pos]);
        }
        finally
        {
            ArrayPool<char>.Shared.Return(pooledArray);
        }
    }

    [Benchmark]
    public string SpanStackPool()
    {
        // this only works when Input <256 to avoid heap allocation
        Span<char> destination = stackalloc char[Input.Length];

        int pos = 0;

        foreach (char c in Input)
        {
            if (!char.IsWhiteSpace(c))
            {
                destination[pos++] = c;
            }
        }

        return Input.Length == pos ? Input : new string(destination[..pos]);
    }
}

public static partial class RegexSample
{
    [GeneratedRegex(@"\s+")]
    public static partial Regex WhiteSpaceRegex();
}
