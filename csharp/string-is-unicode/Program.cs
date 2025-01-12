// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;


BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HideColumns(Column.Job)]
public class Benchmark
{
    [Benchmark]
    public bool HasUnicodeCharCheck()
    {
        return StringExtensions.HasUnicodeCharCheck("Hello 🌍! This is a test: 𝒜𝓁𝓅𝒽𝒶, β, γ, δ, ε, Ω, π, ∞, ❤️");
    }

    [Benchmark]
    public bool HasUnicodeVector()
    {
        return StringExtensions.HasUnicodeVector("Hello 🌍! This is a test: 𝒜𝓁𝓅𝒽𝒶, β, γ, δ, ε, Ω, π, ∞, ❤️");
    }
}

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasUnicodeCharCheck(string source)
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<char>())
        {
            foreach (char c in source)
            {
                if (c > 255)
                {
                    return true;
                }
            }
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasUnicodeVector(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        ReadOnlySpan<char> span = source;
        ref char ptr = ref MemoryMarshal.GetReference(span);

        if (Vector512.IsHardwareAccelerated && span.Length >= Vector512<ushort>.Count)
        {
            return HasUnicodeVector512(ref ptr, span.Length);
        }
        else if (Vector256.IsHardwareAccelerated && span.Length >= Vector256<ushort>.Count)
        {
            return HasUnicodeVector256(ref ptr, span.Length);
        }
        else if (Vector128.IsHardwareAccelerated && span.Length >= Vector128<ushort>.Count)
        {
            return HasUnicodeVector128(ref ptr, span.Length);
        }

        return HasUnicodeScalar(ref ptr, span.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool HasUnicodeVector512(ref char ptr, int length)
    {
        int vectorSize = Vector512<ushort>.Count;
        int vectorCount = length / vectorSize;
        Vector512<ushort> threshold = Vector512.Create((ushort)255);

        for (int i = 0; i < vectorCount; i++)
        {
            Vector512<ushort> vector = Vector512.LoadUnsafe(ref Unsafe.As<char, ushort>(ref Unsafe.Add(ref ptr, i * vectorSize)));
            if (Vector512.GreaterThanAny(vector, threshold))
            {
                return true;
            }
        }

        int processed = vectorCount * vectorSize;
        return HasUnicodeScalar(ref Unsafe.Add(ref ptr, processed), length - processed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool HasUnicodeVector256(ref char ptr, int length)
    {
        int vectorSize = Vector256<ushort>.Count;
        int vectorCount = length / vectorSize;
        Vector256<ushort> threshold = Vector256.Create((ushort)255);

        for (int i = 0; i < vectorCount; i++)
        {
            Vector256<ushort> vector = Vector256.LoadUnsafe(ref Unsafe.As<char, ushort>(ref Unsafe.Add(ref ptr, i * vectorSize)));
            if (Vector256.GreaterThanAny(vector, threshold))
            {
                return true;
            }
        }

        int processed = vectorCount * vectorSize;
        return HasUnicodeScalar(ref Unsafe.Add(ref ptr, processed), length - processed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool HasUnicodeVector128(ref char ptr, int length)
    {
        int vectorSize = Vector128<ushort>.Count;
        int vectorCount = length / vectorSize;
        Vector128<ushort> threshold = Vector128.Create((ushort)255);

        for (int i = 0; i < vectorCount; i++)
        {
            Vector128<ushort> vector = Vector128.LoadUnsafe(ref Unsafe.As<char, ushort>(ref Unsafe.Add(ref ptr, i * vectorSize)));
            if (Vector128.GreaterThanAny(vector, threshold))
            {
                return true;
            }
        }

        int processed = vectorCount * vectorSize;
        return HasUnicodeScalar(ref Unsafe.Add(ref ptr, processed), length - processed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool HasUnicodeScalar(ref char ptr, int length)
    {
        for (int i = 0; i < length; i++)
        {
            if (Unsafe.Add(ref ptr, i) > 255)
            {
                return true;
            }
        }
        return false;
    }
}
