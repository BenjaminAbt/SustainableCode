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
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1466 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.200-preview.21617.4
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


|       Method | Iterations |         Mean |     Error |    StdDev | Allocated |
|------------- |----------- |-------------:|----------:|----------:|----------:|
| WithoutLocal |         10 |    17.456 ns | 0.1201 ns | 0.1065 ns |         - |
|    WithLocal |         10 |     6.825 ns | 0.0915 ns | 0.0856 ns |         - |
| WithoutLocal |        100 |   192.719 ns | 0.8559 ns | 0.8006 ns |         - |
|    WithLocal |        100 |    67.583 ns | 0.4015 ns | 0.3559 ns |         - |
| WithoutLocal |       1000 | 1,895.982 ns | 8.0679 ns | 6.7371 ns |         - |
|    WithLocal |       1000 |   637.850 ns | 4.8726 ns | 4.5578 ns |         - |
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

This benchmark runs several minutes (1:37min on my workstation)

---
*Thanks to [gfoidl](https://github.com/gfoidl).*
