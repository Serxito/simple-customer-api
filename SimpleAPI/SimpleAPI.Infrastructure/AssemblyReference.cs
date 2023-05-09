using System.Reflection;

namespace SimpleAPI.Infrastructure;

/// <summary>
/// Reference for other projects
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}