# 🌳 Sustainable Code - the cost of returning an empty collection

The return of empty collections is practically part of every code. But which variants cost how much - and wouldn't zero be even better?

## 🔥 Benchmark

```sh
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
AMD Ryzen 9 9950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method              | Runtime  | Mean      | StdDev    | Gen0   |
|-------------|--------- |----------:|----------:|-------:|
| List()      | .NET 7.0 | 2.6872 ns | 0.0125 ns | 0.0019 |
| List()      | .NET 8.0 | 1.7082 ns | 0.0100 ns | 0.0019 |
| List()      | .NET 9.0 | 1.6796 ns | 0.0065 ns | 0.0019 |
|             |          |           |           |        |
| List(0)     | .NET 7.0 | 2.6740 ns | 0.0157 ns | 0.0019 |
| List(0)     | .NET 8.0 | 1.6931 ns | 0.0095 ns | 0.0019 |
| List(0)     | .NET 9.0 | 1.6590 ns | 0.0066 ns | 0.0019 |
|             |          |           |           |        |
| List []     | .NET 7.0 | 2.6605 ns | 0.0096 ns | 0.0019 |
| List []     | .NET 8.0 | 1.7156 ns | 0.0118 ns | 0.0019 |
| List []     | .NET 9.0 | 1.7171 ns | 0.0088 ns | 0.0019 |
|             |          |           |           |        |
| Array.Empty | .NET 7.0 | 0.1821 ns | 0.0086 ns |      - |
| Array.Empty | .NET 8.0 | 0.1678 ns | 0.0040 ns |      - |
| Array.Empty | .NET 9.0 | 0.1686 ns | 0.0040 ns |      - |
|             |          |           |           |        |
| Array []    | .NET 7.0 | 0.1776 ns | 0.0060 ns |      - |
| Array []    | .NET 8.0 | 0.1733 ns | 0.0055 ns |      - |
| Array []    | .NET 9.0 | 0.1735 ns | 0.0052 ns |      - |
|             |          |           |           |        |
| HashSet()   | .NET 7.0 | 2.1399 ns | 0.0215 ns | 0.0038 |
| HashSet()   | .NET 8.0 | 2.0574 ns | 0.0273 ns | 0.0038 |
| HashSet()   | .NET 9.0 | 2.0175 ns | 0.0180 ns | 0.0038 |
|             |          |           |           |        |
| HashSet []  | .NET 7.0 | 2.1432 ns | 0.0156 ns | 0.0038 |
| HashSet []  | .NET 8.0 | 2.0484 ns | 0.0248 ns | 0.0038 |
| HashSet []  | .NET 9.0 | 1.9895 ns | 0.0223 ns | 0.0038 |
```

## 🏁 Remarks

- The new notation `[]` is an alias for all collections in order to create the collection in the most efficient way
- The return of `Array.Empty` is by far the fastest and most efficient way; the array is only created once in the background and then statically retained. This is not possible with other collections.

## Conclusion

It is very useful to follow the current recommendation and use `[]`.

## ⌨️ Run this sample

```shell
dotnet run -c Release --framework net9.0
```

## Updates

- 2023/11 - Add .NET 8
- 2024/11 - Add .NET 9
