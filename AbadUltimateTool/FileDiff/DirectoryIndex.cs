using System;
using System.Collections.Generic;
using System.IO;

namespace AbadUltimateTool.FileDiff;

// snapshot of a directory: file name to last-write-UTC
internal sealed class DirectoryIndex
{
    public string Root { get; }

    // file name  ->  (mtime-UTC , fullPath)
    private readonly Dictionary<string, (DateTime stamp, string path)> _map;

    private DirectoryIndex(string root,
                           Dictionary<string, (DateTime, string)> map)
    {
        Root = root;
        _map = map;
    }

    public static DirectoryIndex Build(string root)
    {
        var map = new Dictionary<string, (DateTime stamp, string path)>(
                      StringComparer.OrdinalIgnoreCase);

        // manual stack so  “Composer” folders are ignored and told to go away
        var stack = new Stack<string>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            string dir = stack.Pop();

            // files in the current dir 
            foreach (var file in Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly))
            {
                string name = Path.GetFileName(file);
                DateTime ts = File.GetLastWriteTimeUtc(file);

                // keep newer duplicate
                if (!map.TryAdd(name, (ts, file)) && ts > map[name].stamp)
                    map[name] = (ts, file);
            }

            //  push sub-dirs except any sub-dirs that contain strings specified in the user config file 
            foreach (var subDir in Directory.EnumerateDirectories(dir, "*", SearchOption.TopDirectoryOnly))
            {
                string leaf = Path.GetFileName(subDir);

                // skip if the leaf matches any exclude name
                if (ExcludeFolderStore.Names.Contains(leaf))
                    continue;

                stack.Push(subDir);
            }
        }

        return new DirectoryIndex(root, map);
    }

    public bool TryGet(string fileName, out DateTime mtimeUtc)
    {
        if (_map.TryGetValue(fileName, out var tuple))
        {
            mtimeUtc = tuple.stamp;
            return true;
        }
        mtimeUtc = default;
        return false;
    }

    public string GetFullPath(string fileName) =>
        _map.TryGetValue(fileName, out var tup) ? tup.path : null;

    public IEnumerable<string> FileNames => _map.Keys;
}

internal sealed class DiffRow
{
    public string File { get; set; }
    public string Status { get; set; }
    public DateTime? SourceMTime { get; set; }
    public DateTime? TargetMTime { get; set; }
    public string SourcePath { get; set; }
    public string TargetPath { get; set; }
    // convenience true when source is newer than target or target missing
    public bool NeedsCopy =>
        Status == "Older" || Status == "Missing in target";
}
