using PKHeX.Core;
using System.Reflection;

namespace owoow.Core;

public static class Utils
{
    private static readonly Assembly thisAssembly;
    private static readonly Dictionary<string, string> resourceNameMap;

    static Utils()
    {
        thisAssembly = Assembly.GetExecutingAssembly();
        resourceNameMap = BuildLookup(thisAssembly.GetManifestResourceNames());
    }

    private static Dictionary<string, string> BuildLookup(IReadOnlyCollection<string> names)
    {
        var res = new Dictionary<string, string>(names.Count);
        foreach (var name in names)
        {
            var fname = GetFileName(name);
            res.TryAdd(fname, name);
        }
        return res;
    }

    private static string GetFileName(string name)
    {
        var period = name.LastIndexOf('.', name.Length - 6);
        var start = period + 1;
        System.Diagnostics.Debug.Assert(start != 0);

        // text file fetch excludes ".txt" (mixed case...); other extensions are used (all lowercase).
        return name.EndsWith(".txt", StringComparison.Ordinal)
            ? name[start..^4].ToLowerInvariant()
            : name[start..];
    }

    public static string? GetStringResource(string name)
    {
        if (!resourceNameMap.TryGetValue(name.ToLowerInvariant(), out var resourceName))
            return null;

        using var resource = thisAssembly.GetManifestResourceStream(resourceName);
        if (resource is null)
            return null;

        using var reader = new StreamReader(resource);
        return reader.ReadToEnd();
    }

    public static bool HasMark(IRibbonIndex pk, out RibbonIndex result)
    {
        result = default;
        for (var mark = RibbonIndex.MarkLunchtime; mark <= RibbonIndex.MarkSlump; mark++)
        {
            if (pk.GetRibbon((int)mark))
            {
                result = mark;
                return true;
            }
        }
        return false;
    }
}
