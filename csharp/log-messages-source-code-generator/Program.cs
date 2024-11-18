// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)] // PGO enabled by default
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job, Column.Median)]
public class Benchmark
{
    private const int _lines = 10;

    private ILogger _log;
    private string _message;
    private Exception _ex;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _log = new ThrowAwayLogger();
        _message = "Message";
        _ex = new Exception("ExMessage");
    }

    [Benchmark]
    public int SourceCodeGenerated()
    {
        for (int i = 0; i < _lines; i++)
        {
            SourceCodeGeneratedLog.Sample(_log, _ex, _message);
        }

        return _lines;
    }

    [Benchmark]
    public int Concat()
    {
        for (int i = 0; i < _lines; i++)
        {
            _log.Log(LogLevel.Information, _ex, "Test " + _message);
        }

        return _lines;
    }

    [Benchmark]
    public int Interpolation()
    {
        for (int i = 0; i < _lines; i++)
        {
            _log.Log(LogLevel.Information, _ex, $"Test {_message}");
        }

        return _lines;
    }
}

public static partial class SourceCodeGeneratedLog
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Test {message}")]
    public static partial void Sample(ILogger logger, Exception ex, string message);
}

// https://github.com/BenjaminAbt/ThrowAwayLogger
public class ThrowAwayLogger : ILogger, IDisposable
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { }
    public bool IsEnabled(LogLevel logLevel) => true;
    public IDisposable BeginScope<TState>(TState state) => this;
    public void Dispose() { }
}
