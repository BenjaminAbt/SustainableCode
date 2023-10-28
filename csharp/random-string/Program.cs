// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    [Params(10, 100, 1000)]
    public int CharLength { get; set; }

    [Benchmark(Baseline = true)]
    public string StringCreate() => StringCreateSample.CreateRandomString(CharLength);

    [Benchmark]
    public string EnumerateRepeat() => EnumerateRepeatSample.CreateRandomString(CharLength);

    [Benchmark]
    public string CharArray() => CharArraySample.CreateRandomString(CharLength);

    [Benchmark]
    public string Span() => SpanSample.CreateRandomString(CharLength);
}

public static class SampleConstants
{
    public const string UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string LowerChars = "abcdefghijklmnopqrstuvwxyz";
    public const string Digits = "0123456789";

    public const string AlphNum = UpperChars + LowerChars + Digits;
}

public static class StringCreateSample
{
    private static readonly Random _random = new();
    private static void CreateRandomString(Span<char> buffer)
    {
        const string chars = SampleConstants.AlphNum;
        int charsLength = chars.Length;

        for (int i = 0; i < buffer.Length; ++i)
        {
            int cl = charsLength;
            buffer[i] = chars[_random.Next(cl)];
        }
    }

    public static string CreateRandomString(int length)
    {
        return string.Create<object>(length, null, static (buffer, _) => CreateRandomString(buffer));
    }
}

public static class SpanSample
{
    private static readonly Random _random = new();
    private static void CreateRandomString(Span<char> buffer)
    {
        const string chars = SampleConstants.AlphNum;
        ReadOnlySpan<char> charsSpan = chars.AsSpan();
        int charsLength = chars.Length;

        for (int i = 0; i < buffer.Length; ++i)
        {
            int cl = charsLength;

            buffer[i] = charsSpan[_random.Next(cl)];
        }
    }

    public static string CreateRandomString(int length)
    {
        return string.Create<object>(length, null, static (buffer, _) => CreateRandomString(buffer));
    }
}

public static class EnumerateRepeatSample
{
    private static readonly Random _random = new();
    public static string CreateRandomString(int length)
    {
        const string chars = SampleConstants.AlphNum;
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}

public static class CharArraySample
{
    private static readonly Random _random = new();
    public static string CreateRandomString(int length)
    {
        const string chars = SampleConstants.AlphNum;
        int charsLength = chars.Length;

        char[] charArray = new char[length];
        for (int i = 0; i < charArray.Length; i++)
        {
            int cl = charsLength;
            charArray[i] = chars[_random.Next(cl)];
        }

        return new(charArray);
    }
}
