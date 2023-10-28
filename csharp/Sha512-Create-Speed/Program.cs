// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Security.Cryptography;
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
    private readonly byte[] _sampleData;

    public Benchmark()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding winEncoding1252 = Encoding.GetEncoding(1252);

        string sample = new('a', 1000);
        _sampleData = winEncoding1252.GetBytes(sample);
    }

    [Benchmark]
    public byte[] Create()
    {
        using SHA512 sha512 = SHA512.Create();
        return sha512.ComputeHash(_sampleData);
    }

    private static readonly SHA512 s_sha512 = SHA512.Create(); // IDisposable
    [Benchmark]
    public byte[] Lock()
    {
        byte[] data;
        lock (s_sha512)
        {
            data = s_sha512.ComputeHash(_sampleData);
        }
        return data;
    }

    [Benchmark]
    public byte[] HashData() => SHA512.HashData(_sampleData);

}

