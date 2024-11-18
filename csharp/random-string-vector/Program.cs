// Made by Benjamin Abt - https://github.com/BenjaminAbt

// ================================================
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

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
BenchmarkDotNet.Running.BenchmarkRunner.Run<Benchmark>();
#endif

[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
public class Benchmark
{
    [Params(10, 100, 1000)]
    public int CharLength { get; set; } = 100;

    [Benchmark]
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
    private static ReadOnlySpan<byte> AlphNum => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"u8;

    public static string CreateRandomString(int length)
    {
        return string.Create<object?>(length, null, static (buffer, _) => CreateRandomString(buffer));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
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
        ReadOnlySpan<byte> chars = AlphNum;
        int charsLength = chars.Length;

        for (int i = 0; i < buffer.Length; ++i)
        {
            buffer[i] = (char)chars[Random.Shared.Next(charsLength)];
        }
    }

    private static unsafe void CreateRandomStringVectorized(Span<char> buffer, byte* seedChars)
    {
        Vector256<int> seed = Vector256.Create(
            Random.Shared.NextInt64(),
            Random.Shared.NextInt64(),
            Random.Shared.NextInt64(),
            Random.Shared.NextInt64()
        ).AsInt32();

        Debug.Assert(AlphNum.Length == 62);
        PackToBytes(ref MemoryMarshal.GetReference(AlphNum), seedChars);

        Vector256<byte> seedVec0 = Vector256.Load(seedChars);
        Vector256<byte> seedVec1 = Vector256.Load(seedChars + Vector256<byte>.Count);

        Vector256<float> upperForVec = Vector256.Create((float)(Vector256<byte>.Count - 1));
        Vector256<float> one = Vector256.Create(1f);
        Vector256<int> mantissaMask = Vector256.Create(0x7FFFFF);

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
        static void PackToBytes(ref byte chars, byte* seed)
        {
#if DEBUG
            // Clear the seed to aid debugging. We have len 62, so 32 + 16 and the remainder from the end.
            Vector256<byte>.Zero.Store(seed);
            Vector128<byte>.Zero.Store(seed + Vector256<byte>.Count);
            Vector128<byte>.Zero.Store(seed + 62 - Vector128<byte>.Count);

            seed[62] = (byte)'=';
            seed[63] = (byte)'=';
#endif
            // We have 62 chars (given as byte), so read 32.
            // Then 30 chars (given as byte) remain.
            Vector256.LoadUnsafe(ref chars).Store(seed);

            // We read 16 chars (given as byte).
            // Then 14 chars (given as byte) remain.
            Vector128.LoadUnsafe(ref chars, (uint)Vector256<byte>.Count).Store(seed + Vector256<byte>.Count);

            // For the remaining 14 chars we read 16 chars from the end, as the operation is idempotent.
            nuint offset = 62 - (uint)Vector128<byte>.Count;
            Vector128.LoadUnsafe(ref chars, offset).Store(seed + offset);
#if DEBUG
            Debug.Assert(seed[62] == (byte)'=');
            Debug.Assert(seed[63] == (byte)'=');
#endif
            // The 62 Chars are narrowed to 62 bytes, so add another two random bytes (chars)
            // so the whole range of 64 bytes can be used. In regards to entropy it would be
            // better to leave them off, as 2/62 are more likely this way. But for speed it's
            // better.
            seed[62] = Unsafe.Add(ref chars, Random.Shared.Next(62));
            seed[63] = Unsafe.Add(ref chars, Random.Shared.Next(62));
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
