// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    private string _sampleString = new('a', 1000);
    private int _a = 1;
    private int _b = 10;

    private int rows = 100;

    [Benchmark]
    public int SmallStruct()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            SmallStruct test = new(_a, _b);
            sum = sum + Calc(test);
        }
        return sum;
    }

    [Benchmark]
    public int MediumStruct()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            MediumStruct test = new(_a, _b, _sampleString, _sampleString);
            sum = sum + Calc(test);
        }
        return sum;
    }

    [Benchmark]
    public int SmallClass()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            SmallClass test = new(_a, _b);
            sum = sum + Calc(test);
        }
        return sum;
    }

    [Benchmark]
    public int MediumClass()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            MediumClass test = new(_a, _b, _sampleString, _sampleString);
            sum = sum + Calc(test);
        }
        return sum;
    }

    private int Calc(SmallStruct input) => input.A + input.B;
    private int Calc(MediumStruct input) => input.A + input.B;
    private int Calc(SmallClass input) => input.A + input.B;
    private int Calc(MediumClass input) => input.A + input.B;
}

public readonly record struct SmallStruct(int A, int B);
public readonly record struct MediumStruct(int A, int B, string X, string Y);

public record class SmallClass(int A, int B);
public record class MediumClass(int A, int B, string X, string Y);
