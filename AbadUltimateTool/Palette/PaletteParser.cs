using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbadUltimateTool.Palette;



// Find Palette files and parse them for their ID, RGB value, and comment
// Why? Because most ppl can't read RGB values and know what they mean...
internal static class PaletteParser
{
    //   Regual expression that extracts IDX, R, G, B, and the comment written - saves time to not have to do this in Notepad++
    // IDX=0 R=20 G=3 B=30 // comment
    private static readonly Regex Rx = new Regex(
        @"^\s*IDX=(\d+)\s+R=(\d+)\s+G=(\d+)\s+B=(\d+)(?:\s*//\s*(.*))?",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    // establish how many palette files there are in folders and subfolders, because who knows if some handmade stuff has been made 
    public static IEnumerable<string> FindPaletteFiles(string root)
    {
        // use an explicit stack
        var todo = new Stack<string>();
        todo.Push(root);

        while (todo.Count > 0)
        {
            string dir = todo.Pop();

            //   Grab *Palette*.ini files 
            IEnumerable<string> files = Enumerable.Empty<string>();
            try
            {
                files = Directory.EnumerateFiles(dir, "*Palette*.ini", SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException) { } //skip sealed directories 
            catch (PathTooLongException) { }// skip absurd paths
            catch (DirectoryNotFoundException) { }// skip dissappeared files or paths (might be volatile somehow)

            foreach (var f in files)
                yield return f;

            //   Queue sub-directories 
            IEnumerable<string> subDirs = Enumerable.Empty<string>();
            try
            {
                subDirs = Directory.EnumerateDirectories(dir);
            }
            catch (UnauthorizedAccessException) { } //skip sealed directories 
            catch (PathTooLongException) { } // skip absurd paths
            catch (DirectoryNotFoundException) { } // skip dissappeared files or paths (might be volatile somehow)

            foreach (var d in subDirs)
                todo.Push(d);
        }
    }

    public static List<PaletteEntry> Parse(string file)
    {
        var list = new List<PaletteEntry>();

        foreach (string raw in File.ReadLines(file))
        {
            string line = raw.Trim();
            if (line.Length == 0 || line.StartsWith(";")) continue; // skip blank

            var m = Rx.Match(line);
            if (!m.Success) continue;                              // silently ignore garbage

            list.Add(new PaletteEntry( // add values to list to be put into cells
                idx: int.Parse(m.Groups[1].Value),
                r: byte.Parse(m.Groups[2].Value),
                g: byte.Parse(m.Groups[3].Value),
                b: byte.Parse(m.Groups[4].Value),
                comment: m.Groups[5].Value));
        }
        return list;
    }
}
