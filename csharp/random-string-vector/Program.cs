// This example was provided by https://github.com/gfoidl to illustrate what is possible with "lower level programming in .NET".
// https://github.com/BenjaminAbt/SustainableCode/tree/main/csharp/random-string

/* RESULTS
 * 
 * BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2130/21H2/November2021Update)
 * AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
 * .NET SDK=7.0.100-rc.2.22477.23
 *   [Host]     : .NET 7.0.0 (7.0.22.47203), X64 RyuJIT AVX2
 *   DefaultJob : .NET 7.0.0 (7.0.22.47203), X64 RyuJIT AVX2
 * 
 * 
 * |       Method | CharLength |        Mean |     Error |    StdDev | Ratio | RatioSD |
 * |------------- |----------- |------------:|----------:|----------:|------:|--------:|
 * | StringCreate |         10 |    83.80 ns |  1.698 ns |  1.668 ns |  1.00 |    0.00 |
 * |   Vectorized |         10 |    82.21 ns |  1.670 ns |  1.481 ns |  0.98 |    0.02 |
 * |              |            |             |           |           |       |         |
 * | StringCreate |        100 |   738.45 ns |  8.001 ns |  7.484 ns |  1.00 |    0.00 |
 * |   Vectorized |        100 |    89.81 ns |  1.724 ns |  1.528 ns |  0.12 |    0.00 |
 * |              |            |             |           |           |       |         |
 * | StringCreate |       1000 | 7,480.91 ns | 89.578 ns | 83.791 ns |  1.00 |    0.00 |
 * |   Vectorized |       1000 |   377.54 ns |  6.273 ns |  6.712 ns |  0.05 |    0.00 |
 * 
 * */


// ================================================
// comment this out to run in DEBUG
#define BENCH 

using System;
using System.Threading;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;

Benchmark bench = new()
{
    CharLength = 82
};

#if DEBUG
Console.WriteLine(bench.StringCreate());
Console.WriteLine();
for (int i = 0; i < 10; ++i)
{
    Console.WriteLine(bench.Vectorized());
}
#else
#if BENCH
BenchmarkDotNet.Running.BenchmarkRunner.Run<Benchmark>();
#else
for (int i = 0; i < 100; ++i)
{
    if (i % 10 == 0) Thread.Sleep(100);

    _ = bench.Vectorized();
}
#endif
#endif


public class Benchmark
{
    [Params(10, 100, 1000)]
    public int CharLength { get; set; } = 100;

    [Benchmark(Baseline = true)]
    public string StringCreate() => StringCreateSample.CreateRandomString(CharLength);

    [Benchmark]
    public string Vectorized() => VectorSample.CreateRandomString(CharLength);
}

public static class SampleConstants
{
    public const string UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string LowerChars = "abcdefghijklmnopqrstuvwxyz";
    public const string Digits = "0123456789";

    public const string AlphNum = UpperChars + LowerChars + Digits;
}

public static class StringCreateSample
{
    private static readonly Random s_random = new(0);
    private static void CreateRandomString(Span<char> buffer)
    {
        const string Chars = SampleConstants.AlphNum;
        int charsLength = Chars.Length;

        for (int i = 0; i < buffer.Length; ++i)
        {
            int cl = charsLength;
            buffer[i] = Chars[s_random.Next(cl)];
        }
    }

    public static string CreateRandomString(int length)
    {
        return string.Create<object?>(length, null, static (buffer, _) => CreateRandomString(buffer));
    }
}

public static class VectorSample
{
    private static readonly Random s_random = new(0);

    public static string CreateRandomString(int length)
    {
        return string.Create<object?>(length, null, static (buffer, _) => CreateRandomString(buffer));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe void CreateRandomString(Span<char> buffer)
    {
        if (Avx2.IsSupported && buffer.Length >= 2 * Vector256<ushort>.Count)
        {
            // For JIT-TC to kick in no stackalloc must occur.
            // Thus it's hoisted to here. And that's why the scalar path is moved
            // into its own method.
            byte* seedChars = stackalloc byte[64];
            CreateRandomStringVectorized(buffer, seedChars);
        }
        else
        {
            CreateRandomStringScalar(buffer);
        }
    }

    private static void CreateRandomStringScalar(Span<char> buffer)
    {
        const string Chars = SampleConstants.AlphNum;
        int charsLength = Chars.Length;

        for (int i = 0; i < buffer.Length; ++i)
        {
            buffer[i] = Chars[s_random.Next(charsLength)];
        }
    }

    [SkipLocalsInit]
    private static unsafe void CreateRandomStringVectorized(Span<char> buffer, byte* seedChars)
    {
        const string Chars = SampleConstants.AlphNum;

        Debug.Assert(Chars.Length == 62);

        // seedChars could also be given as ROS<byte>, depending on use case.
        // Especially with C# 11's UTF-8 literals, e.g. "ABCD..."u8
        ref ushort chars = ref Unsafe.As<char, ushort>(ref Unsafe.AsRef(Chars.GetPinnableReference()));
        PackToBytes(ref chars, seedChars);

        Vector256<byte> seedVec0 = Vector256.Load(seedChars);
        Vector256<byte> seedVec1 = Vector256.Load(seedChars + Vector256<byte>.Count);

        Vector256<float> upperForVec = Vector256.Create((float)(Vector256<byte>.Count - 1));
        Vector256<float> one = Vector256.Create(1f);
        Vector256<int> mantissaMask = Vector256.Create(0x7FFFFF);
        Vector256<int> seed = Vector256.Create(
            Random.Shared.NextInt64(),
            Random.Shared.NextInt64(),
            Random.Shared.NextInt64(),
            Random.Shared.NextInt64()
        ).AsInt32();

        ref ushort dest = ref Unsafe.As<char, ushort>(ref MemoryMarshal.GetReference(buffer));
        ref ushort twoVectorsAwayFromEnd = ref Unsafe.Add(ref dest, (uint)(buffer.Length - 2 * Vector256<ushort>.Count));

        do
        {
            Core(ref dest, seedVec0, seedVec1, ref seed, mantissaMask, one, upperForVec);
            dest = ref Unsafe.Add(ref dest, 2 * Vector256<ushort>.Count);
        }
        while (Unsafe.IsAddressLessThan(ref dest, ref twoVectorsAwayFromEnd));

        Core(ref twoVectorsAwayFromEnd, seedVec0, seedVec1, ref seed, mantissaMask, one, upperForVec);
        //---------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void PackToBytes(ref ushort chars, byte* seed)
        {
            ref short charsAsInt16 = ref Unsafe.As<ushort, short>(ref chars);

            // Clear the seed (len = 32, i.e. Vector256<byte> size)
            Vector256<byte>.Zero.Store(seed);
#if DEBUG
            seed[62] = (byte)'=';
            seed[63] = (byte)'=';
#endif
            // We read 32 chars, pack them to 32 bytes
            // Then 30 chars remain
            //
            // // Use hw-intrinsics as they don't perform additional AND like Vector256.Narrow does
            Vector256<byte> narrowed256 = Avx2.PackUnsignedSaturate(
                Vector256.LoadUnsafe(ref charsAsInt16),
                Vector256.LoadUnsafe(ref charsAsInt16, (uint)Vector256<ushort>.Count));
            narrowed256 = Avx2.Permute4x64(narrowed256.AsInt64(), 0b_11_01_10_00).AsByte();

            narrowed256.Store(seed);

            nuint offset = 2 * (uint)Vector256<ushort>.Count;

            // We read 16 chars, pack them to 16 bytes
            // Then 14 chars remain
            Vector128<byte> narrowed128 = Sse2.PackUnsignedSaturate(
                Vector128.LoadUnsafe(ref charsAsInt16, offset),
                Vector128.LoadUnsafe(ref charsAsInt16, offset + (uint)Vector128<ushort>.Count));

            narrowed128.Store(seed + Vector256<byte>.Count);

            // For the remaining 14 chars we read 16 chars from the end, as the operation is idempotent.
            offset = 62 - 2 * (uint)Vector128<ushort>.Count;

            narrowed128 = Sse2.PackUnsignedSaturate(
                Vector128.LoadUnsafe(ref charsAsInt16, offset),
                Vector128.LoadUnsafe(ref charsAsInt16, offset + (uint)Vector128<ushort>.Count));

            narrowed128.Store(seed + offset);
#if DEBUG
            Debug.Assert(seed[62] == (byte)'=');
            Debug.Assert(seed[63] == (byte)'=');
#endif
            // The 62 Chars are narrowed to 62 bytes, so add another two random bytes (chars)
            // so the whole range of 64 bytes can be used. In regards to entropy it would be
            // better to leave them off, as 2/62 are more likely this way. But for speed it's
            // better.
            seed[62] = (byte)Unsafe.Add(ref chars, Random.Shared.Next(62));
            seed[63] = (byte)Unsafe.Add(ref chars, Random.Shared.Next(62));
        }
        //---------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Core(
            ref ushort dest,
            Vector256<byte> seedVec0,
            Vector256<byte> seedVec1,
            ref Vector256<int> seed,
            Vector256<int> mantissaMask,
            Vector256<float> one,
            Vector256<float> upperForVector)
        {
            Vector256<byte> shuffleMask = NextRandomByteVector(ref seed, mantissaMask, one, upperForVector);

            // seedVec0                             seedVec1
            // ABCDEFGHIJKLMNOP QRSTUVWXYZabcdef    ghijklmnopqrstuv wxyz0123456789<>
            // vec0                                 vec1
            // ANBGJCKHNKIMKLDE ZUVXWRVSUbQWZVZR    gthmpiqntqosqrjk 50132x1y07w2515x
            Vector256<byte> vec0 = Avx2.Shuffle(seedVec0, shuffleMask);
            Vector256<byte> vec1 = Avx2.Shuffle(seedVec1, shuffleMask);

            Vector256<int> permuteMask = NextRandomVector(ref seed, mantissaMask, one, upperForVector);
            //          vec0                                        vec1
            // before:  ANBG JCKH NKIM KLDE ZUVX WRVS UbQW ZVZR     gthm piqn tqos qrjk 5013 2x1y 07w2 515x
            // after:   WRVS ZVZR JCKH KLDE KLDE ZVZR KLDE NKIM     2x1y 515x piqn qrjk qrjk 515x qrjk tqos
            vec0 = Avx2.PermuteVar8x32(vec0.AsInt32(), permuteMask).AsByte();
            vec1 = Avx2.PermuteVar8x32(vec1.AsInt32(), permuteMask).AsByte();

            // after blend: 2RVyZ15RJiqnqLDkKrDE5VZxqLjkNKIM
            Vector256<byte> blendMask = Vector256.Equals(shuffleMask & Vector256.Create((byte)1), Vector256<byte>.Zero);
            Vector256<byte> res = Avx2.BlendVariable(vec0, vec1, blendMask);

            (Vector256<ushort> lower, Vector256<ushort> upper) = Vector256.Widen(res);
            lower.StoreUnsafe(ref dest);
            upper.StoreUnsafe(ref dest, (uint)Vector256<ushort>.Count);
        }
        //---------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector256<byte> NextRandomByteVector(
            ref Vector256<int> seed,
            Vector256<int> mantissaMask,
            Vector256<float> one,
            Vector256<float> upper)
        {
            Vector256<int> rnd0 = NextRandomVector(ref seed, mantissaMask, one, upper);
            Vector256<int> rnd1 = NextRandomVector(ref seed, mantissaMask, one, upper);
            Vector256<int> rnd2 = NextRandomVector(ref seed, mantissaMask, one, upper);
            Vector256<int> rnd3 = NextRandomVector(ref seed, mantissaMask, one, upper);

            rnd1 = Vector256.ShiftLeft(rnd1, 8);
            rnd2 = Vector256.ShiftLeft(rnd2, 16);
            rnd3 = Vector256.ShiftLeft(rnd3, 24);

            Vector256<int> rnd = (rnd0 | rnd1) | (rnd2 | rnd3);
            return rnd.AsByte();
        }
        //---------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector256<int> NextRandomVector(
            ref Vector256<int> seed,
            Vector256<int> mantissaMask,
            Vector256<float> one,
            Vector256<float> upper)
        {
            // Xorshift (cool how easy :-))
            seed ^= Vector256.ShiftLeft(seed, 13);
            seed ^= Vector256.ShiftRightArithmetic(seed, 17);
            seed ^= Vector256.ShiftLeft(seed, 5);

            // Convert random ints to floats out of [1, 2), cf. https://stackoverflow.com/a/70565649/347870
            Vector256<int> mantissa = seed & mantissaMask;
            Vector256<float> val = mantissa.AsSingle() | one;
            val = Fma.MultiplySubtract(val, upper, upper);  // Scale from [1, 2) to [0, upper)
            Vector256<int> rnd = Vector256.ConvertToInt32(val);            // Convert back to int out of [0, upper) by truncation

            return rnd;
        }
    }
}
