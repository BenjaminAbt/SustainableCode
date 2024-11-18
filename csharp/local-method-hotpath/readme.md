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
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method       | Runtime  | Iterations | Mean         | Error      | StdDev     | Ratio |
|------------- |--------- |----------- |-------------:|-----------:|-----------:|------:|
| WithoutLocal | .NET 7.0 | 10         |    11.076 ns |  0.1264 ns |  0.1121 ns |  3.94 |
| WithoutLocal | .NET 8.0 | 10         |     2.791 ns |  0.0315 ns |  0.0295 ns |  0.99 |
| WithoutLocal | .NET 9.0 | 10         |     2.812 ns |  0.0181 ns |  0.0151 ns |  1.00 |
|              |          |            |              |            |            |       |
| WithLocal    | .NET 7.0 | 10         |     3.494 ns |  0.0859 ns |  0.0990 ns |  1.24 |
| WithLocal    | .NET 8.0 | 10         |     2.807 ns |  0.0147 ns |  0.0130 ns |  0.99 |
| WithLocal    | .NET 9.0 | 10         |     2.827 ns |  0.0277 ns |  0.0246 ns |  1.00 |
|              |          |            |              |            |            |       |
| WithoutLocal | .NET 7.0 | 100        |   119.236 ns |  1.5937 ns |  1.4907 ns |  4.91 |
| WithoutLocal | .NET 8.0 | 100        |    25.734 ns |  0.1918 ns |  0.1794 ns |  1.06 |
| WithoutLocal | .NET 9.0 | 100        |    24.262 ns |  0.2096 ns |  0.1960 ns |  1.00 |
|              |          |            |              |            |            |       |
| WithLocal    | .NET 7.0 | 100        |    40.118 ns |  0.8185 ns |  0.8758 ns |  1.64 |
| WithLocal    | .NET 8.0 | 100        |    25.077 ns |  0.3515 ns |  0.3288 ns |  1.03 |
| WithLocal    | .NET 9.0 | 100        |    24.466 ns |  0.2786 ns |  0.2606 ns |  1.00 |
|              |          |            |              |            |            |       |
| WithoutLocal | .NET 7.0 | 1000       | 1,135.122 ns | 17.3263 ns | 16.2071 ns |  4.74 |
| WithoutLocal | .NET 8.0 | 1000       |   250.022 ns |  1.5989 ns |  1.4174 ns |  1.04 |
| WithoutLocal | .NET 9.0 | 1000       |   239.307 ns |  1.5506 ns |  1.4504 ns |  1.00 |
|              |          |            |              |            |            |       |
| WithLocal    | .NET 7.0 | 1000       |   330.232 ns |  5.3309 ns |  4.9866 ns |  1.37 |
| WithLocal    | .NET 8.0 | 1000       |   241.045 ns |  2.0723 ns |  1.9384 ns |  1.00 |
| WithLocal    | .NET 9.0 | 1000       |   241.217 ns |  1.8586 ns |  1.6476 ns |  1.00 |
```


## 🏁 Results

- 🚀 The local functions are x3 faster

## Remarks

- Local functions do not make sense everywhere and also not with every hot path method
- However, for methods with simple decisions and subsequent actions, this method has proven to be very effective.
- All cases are at least twice, sometimes three times as performant in .NET 8.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9

---
*Thanks to [gfoidl](https://github.com/gfoidl).*
