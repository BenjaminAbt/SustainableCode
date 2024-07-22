# 🌳 Sustainable Code - the cost of returning an empty collection

The return of empty collections is practically part of every code. But which variants cost how much - and wouldn't zero be even better?

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4651/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100-preview.5.24307.3
  [Host]   : .NET 9.0.0 (9.0.24.30607), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.30607), X64 RyuJIT AVX2

| Method      | Mean      | Ratio | Gen0   | Allocated |
|------------ |----------:|------:|-------:|----------:|
| List()      | 2.7975 ns |  1.00 | 0.0019 |      32 B |
| List(0)     | 2.9224 ns |  1.04 | 0.0019 |      32 B |
| List []     | 2.7643 ns |  0.99 | 0.0019 |      32 B |
| Array.Empty | 0.4912 ns |  0.18 |      - |         - |
| Array []    | 0.4835 ns |  0.18 |      - |         - |
| HashSet()   | 4.0250 ns |  1.48 | 0.0038 |      64 B |
| HashSet []  | 4.0798 ns |  1.46 | 0.0038 |      64 B |
```

## 🏁 Remarks

- The new notation `[]` is an alias for all collections in order to create the collection in the most efficient way
- The return of `Array.Empty` is by far the fastest and most efficient way; the array is only created once in the background and then statically retained. This is not possible with other collections.

## Conclusion

It is very useful to follow the current recommendation and use `[]`.

## ⌨️ Run this sample

```shell
dotnet run -c Release
```
