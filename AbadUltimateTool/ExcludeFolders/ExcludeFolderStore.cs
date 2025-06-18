using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AbadUltimateTool;

// loads and saves the list of folder names that must be skipped while indexing
// stored in UltimateMultiTool.user.config in the same folder as the .exe
internal static class ExcludeFolderStore
{
    private const string FileName = "UltimateMultiTool.user.config";

    private static readonly string _filePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

    //memory set (case-insensitive)
    public static HashSet<string> Names { get; private set; }
        = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    // load on program start, creates file with default entry if missing
    public static void Load()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Names = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Composer"      // default exclusion
                };
                Save();           // create the file for next run
                return;
            }

            string json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<List<string>>(json);
            Names = new HashSet<string>(list ?? Enumerable.Empty<string>(),
                                        StringComparer.OrdinalIgnoreCase);
        }
        catch
        {
            // fall back to default on any parse error
            Names = new HashSet<string>(new[] { "Composer" },
                                        StringComparer.OrdinalIgnoreCase);
        }
    }

    public static void Save()
    {
        var json = JsonSerializer.Serialize(Names.ToList(), new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_filePath, json);
    }

    // keeps names from being changed from the outside
    public static void ReplaceAndSave(IEnumerable<string> newNames)
    {
        Names = new HashSet<string>(newNames, StringComparer.OrdinalIgnoreCase);
        Save();
    }

}
