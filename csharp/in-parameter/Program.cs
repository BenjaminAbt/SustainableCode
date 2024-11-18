// Made by Benjamin Abt - https://github.com/BenjaminAbt

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job)]
public class Benchmark
{
    private int _a = 100, _b = 200, _c = 300, _d = 3;

    [Benchmark]
    public int With_InParam()
        => CalcWithIn(_a, _b, _c, _d);

    [Benchmark]
    public int WithOut_InParam()
        => CalcWithOutIn(_a, _b, _c, _d);


    private int CalcWithIn(in int a, in int b, in int c, in int d) => a * b * c / d;
    private int CalcWithOutIn(int a, int b, int c, int d) => a * b * c / d;
}
