// File: IpScan/CsvExporter.cs
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AbadUltimateTool.IpParser;

internal static class CsvExporter
{
    // Export the All-IPs grid
    public static void WriteAll(string file, IEnumerable<IpUsage> items)
    {
        var sb = new StringBuilder();
        sb.AppendLine("IP,Type,Name,File");

        foreach (var g in items)
            sb.AppendFormat("{0},{1},{2},{3}\n",
                            g.Ip,
                            g.Type,
                            g.Name.Replace(',', ' '),
                            g.FilePath);
        File.WriteAllText(file, sb.ToString(), Encoding.UTF8);
    }

    // Export unused addresses of a /24 range
    public static void WriteUnused(string file, IEnumerable<string> ips)
    {
        var sb = new StringBuilder();
        sb.AppendLine("IP");
        foreach (var ip in ips)
            sb.AppendLine(ip);
        File.WriteAllText(file, sb.ToString(), Encoding.UTF8);
    }

    public static void WriteUnusedPrompt(string folder, string net24,
                                     IEnumerable<string> unused)
    {
        string path = Path.Combine(folder,
                                   $"UnusedRange_{net24}.csv"); // underscore added
        WriteUnused(path, unused);
    }

}
