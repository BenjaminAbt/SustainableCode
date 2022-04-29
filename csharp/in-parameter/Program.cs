// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    private int _a = 100, _b = 200, _c = 300, _d = 3;

    /**
    BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
    AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
    .NET SDK= 6.0.300-preview.22204.3 - by Benjamin Abt
      [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
      DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


    |          Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Allocated |
    |---------------- |----------:|----------:|----------:|------:|--------:|----------:|
    |    With_InParam | 0.4396 ns | 0.0190 ns | 0.0178 ns |  2.75 |    0.27 |         - |
    | WithOut_InParam | 0.1613 ns | 0.0159 ns | 0.0149 ns |  1.00 |    0.00 |         - |
    **/

    [Benchmark]
    public int With_InParam() => CalcWithIn(_a, _b, _c, _d);

    [Benchmark(Baseline = true)]
    public int WithOut_InParam() => CalcWithOutIn(_a, _b, _c, _d);


    private int CalcWithIn(in int a, in int b, in int c, in int d) => a * b * c / d;
    private int CalcWithOutIn(int a, int b, int c, int d) => a * b * c / d;
}

