// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    private int _a = 100, _b = 200, _c = 300, _d = 3;

    [Benchmark]
    public int With_InParam() => CalcWithIn(_a, _b, _c, _d);

    [Benchmark(Baseline = true)]
    public int WithOut_InParam() => CalcWithOutIn(_a, _b, _c, _d);


    private int CalcWithIn(in int a, in int b, in int c, in int d) => a * b * c / d;
    private int CalcWithOutIn(int a, int b, int c, int d) => a * b * c / d;
}
