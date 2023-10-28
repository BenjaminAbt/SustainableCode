# 🌳 Sustainable Code - Small Hotpath Methods 📊

This example shows how to improve the performance of methods with the help of [local functions](https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/local-functions?WT.mc_id=DT-MVP-5001507).
Inlining is an optimization

In this case, the contents of the local functions are detached from the actual method ("AnyHotPathMethod") and moved into a compiler-generated method.
Thus the "HotPathMethod" is smaller and the compiler has the possibility to inline.

```csharp

// Generated code without local function
public void AnyHotPathMethod()
{
    if (_b1)
    {
        _sb.Append("aaa");
    }
    if (_b2)
    {
        _sb.Append("bbb");
    }
}

// Generated code with local function
public void AnyHotPathMethodWithFunction()
{
    if (_b1)
    {
        <AnyHotPathMethodWithFunction>g__Core|4_0();
    }
    if (_b2)
    {
        <AnyHotPathMethodWithFunction>g__Core|4_1();
    }
}

[CompilerGenerated]
private void <AnyHotPathMethodWithFunction>g__Core|4_0()
{
    _sb.Append("aaa");
}

[CompilerGenerated]
private void <AnyHotPathMethodWithFunction>g__Core|4_1()
{
    _sb.Append("bbb");
}

```

## 🔥 Benchmark

```shell
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]   : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

| Method       | Runtime  | Iterations | Mean         | Error      | StdDev     | Allocated |
|------------- |--------- |----------- |-------------:|-----------:|-----------:|----------:|
| WithoutLocal | .NET 7.0 | 10         |    15.991 ns |  0.1620 ns |  0.1436 ns |         - |
| WithoutLocal | .NET 8.0 | 10         |     4.089 ns |  0.0171 ns |  0.0160 ns |         - |
|              |          |            |              |            |            |           |
| WithLocal    | .NET 7.0 | 10         |     6.928 ns |  0.1270 ns |  0.1126 ns |         - |
| WithLocal    | .NET 8.0 | 10         |     4.894 ns |  0.0300 ns |  0.0266 ns |         - |
|              |          |            |              |            |            |           |
| WithoutLocal | .NET 7.0 | 100        |   152.300 ns |  0.7867 ns |  0.6569 ns |         - |
| WithoutLocal | .NET 8.0 | 100        |    47.345 ns |  0.6740 ns |  0.6304 ns |         - |
|              |          |            |              |            |            |           |
| WithLocal    | .NET 7.0 | 100        |    67.067 ns |  0.3281 ns |  0.3069 ns |         - |
| WithLocal    | .NET 8.0 | 100        |    47.956 ns |  0.6337 ns |  0.5928 ns |         - |
|              |          |            |              |            |            |           |
| WithoutLocal | .NET 7.0 | 1000       | 1,504.512 ns | 29.8548 ns | 34.3809 ns |         - |
| WithoutLocal | .NET 8.0 | 1000       |   426.727 ns |  4.8606 ns |  4.5467 ns |         - |
|              |          |            |              |            |            |           |
| WithLocal    | .NET 7.0 | 1000       |   644.801 ns | 12.6167 ns | 11.8016 ns |         - |
| WithLocal    | .NET 8.0 | 1000       |   430.170 ns |  5.7188 ns |  5.3494 ns |         - |
```


## 🏁 Results

- 🚀 The local functions are x3 faster

## Remarks

- Local functions do not make sense everywhere and also not with every hot path method
- However, for methods with simple decisions and subsequent actions, this method has proven to be very effective.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```

## Updates

- 2023/11 - Add .NET 8

---
*Thanks to [gfoidl](https://github.com/gfoidl).*
