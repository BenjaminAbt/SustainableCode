// Made by Benjamin Abt - https://github.com/BenjaminAbt

using System.Runtime.CompilerServices;
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
    public bool HasUnicode_NonAggressive()
    {
        return StringExtensions.HasUnicode_NonAggressive("Hello 🌍! This is a test: 𝒜𝓁𝓅𝒽𝒶, β, γ, δ, ε, Ω, π, ∞, ❤️");
    }

    [Benchmark]
    public bool HasUnicode_Aggressive()
    {
        return StringExtensions.HasUnicode_Aggressive("Hello 🌍! This is a test: 𝒜𝓁𝓅𝒽𝒶, β, γ, δ, ε, Ω, π, ∞, ❤️");
    }
}

public static class StringExtensions
{
    public static bool HasUnicode_NonAggressive(string source)
    {
        foreach (char c in source)
        {
            if (c > 255)
            {
                return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasUnicode_Aggressive(string source)
    {
        foreach (char c in source)
        {
            if (c > 255)
            {
                return true;
            }
        }

        return false;
    }
}
