// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public string StringCreate_Case(string a, string b, string c)
    {
        int totalLength = a.Length + 1 + b.Length + 1 + c.Length;

        char[] buffer = new char[totalLength];
        int pos = 0;


        InternalWriteBuffer(ref buffer, ref a, ref pos);
        buffer[pos++] = ' ';

        InternalWriteBuffer(ref buffer, ref b, ref pos);
        buffer[pos++] = ' ';

        InternalWriteBuffer(ref buffer, ref c, ref pos);

        return new string(buffer);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public string StringJoin_Case(string a, string b, string c)
    {

        return string.Join(' ', a, b, c);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public string ValueStringBuilder_Case(string a, string b, string c)
    {
        int totalLength = a.Length + 1 + b.Length + 1 + c.Length;

        ValueStringBuilder sb = new(totalLength);
        sb.Append(a.AsSpan());
        sb.Append(' ');
        sb.Append(b.AsSpan());
        sb.Append(' ');
        sb.Append(c.AsSpan());

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalWriteBuffer(ref char[] buffer, ref string data, ref int pos)
    {
        for (int i = 0; i < data.Length; i++) buffer[pos++] = data[i];
    }

    public IEnumerable<object[]> TestData()
    {
        yield return new object[] { "Mr.", "Benjamin", "Abt" };
    }
}
