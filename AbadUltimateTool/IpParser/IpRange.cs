using System.Collections.Generic;
using System.Linq;

namespace AbadUltimateTool.IpParser;

internal static class IpRange
{
    // "192.168.1.34"  →  "192.168.1"
    public static string Network24(string ip)
    {
        int lastDot = ip.LastIndexOf('.');
        return lastDot > 0 ? ip.Substring(0, lastDot) : ip;
    }

    // Returns the unused addresses 1-254 in that /24
    public static List<string> Unused(string net24, IEnumerable<string> usedIps)
    {
        var used = new HashSet<int>(
            usedIps.Where(s => s.StartsWith(net24 + "."))
                   .Select(s => int.Parse(s.Substring(net24.Length + 1))));

        var list = new List<string>();
        for (int i = 1; i <= 254; i++)
            if (!used.Contains(i))
                list.Add(net24 + "." + i);
        return list;
    }
}
