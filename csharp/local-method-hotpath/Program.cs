// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Text;
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
    [Params(10, 100, 1000)]
    public int Iterations { get; set; }

    private readonly TestClass _testClass = new();

    [Benchmark]
    public void WithoutLocal()
    {
        for (int i = 0; i < Iterations; i++)
        {
            _testClass.AnyHotPathMethod();
        }
    }

    [Benchmark]
    public void WithLocal()
    {
        for (int i = 0; i < Iterations; i++)
        {
            _testClass.AnyHotPathMethodWithFunction();
        }
    }
}

public class TestClass
{
    private readonly StringBuilder _sb = new();
    private readonly bool _b1;
    private readonly bool _b2;


    public void AnyHotPathMethod()
    {
        if (_b1)
        {
            // Do the things that need to be done
            _sb.Append("aaa");
        }

        if (_b2)
        {

            // Do the things that need to be done
            _sb.Append("bbb");
        }
    }

    public void AnyHotPathMethodWithFunction()
    {
        if (_b1)
        {
            Core();
            void Core()
            {
                // Do the things that need to be done
                _sb.Append("aaa");
            }
        }

        if (_b2)
        {
            Core();
            void Core()
            {
                // Do the things that need to be done
                _sb.Append("bbb");
            }
        }
    }
}
