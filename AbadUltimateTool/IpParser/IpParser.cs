using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace AbadUltimateTool.IpParser;

internal static class IpParser
{
    // good enough
    private static readonly Regex _ipRx =
        new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b", RegexOptions.Compiled);


    // Crawl the folder tree and yield IpUsage objects for every hit
    public static IEnumerable<IpUsage> ScanFolder(string root)
    {
        foreach (var file in SafeEnumerateFiles(root, "*.*"))
        {
            // .ini files are NOT part of the IP scan
            if (file.EndsWith(".ini", StringComparison.OrdinalIgnoreCase))
                continue;

            foreach (var hit in ParseFile(file))
                yield return hit;
        }
    }


    //       buffer each COM_CHANNEL block (BEGIN … END)
    //      pull the lines that logically belong to that block (ProtocolManager, Base_IO, etc.) until I see the next channel
    //      classify + spit out the IPs
    private static IEnumerable<IpUsage> ParseFile(string file)
    {
        // read the whole file once
        var lines = SafeReadLines(file).ToList();

        int i = 0;
        while (i < lines.Count)
        {
            string line = lines[i].Trim();
            i++;

            // skip comments or empty lines early
            if (line.Length == 0 || line.StartsWith("//") || line.StartsWith(";"))
                continue;

            // Start of a new block: COM_CHANNEL  or  TEMPLATE

            bool isChannel = false;

            if (line.StartsWith("BEGIN COM_CHANNEL", StringComparison.OrdinalIgnoreCase))
                isChannel = true;                 // old path – keep as-is
            else if (!line.StartsWith("BEGIN TEMPLATE", StringComparison.OrdinalIgnoreCase))
                continue;                         // skip anything else

            // buffer the COM_CHANNEL or TEMPLATE section itself
            var block = new List<string> { line };
            while (i < lines.Count &&
                   !lines[i].TrimStart().StartsWith("END", StringComparison.OrdinalIgnoreCase))
            {
                block.Add(lines[i]);
                i++;
            }
            if (i < lines.Count) block.Add(lines[i]);   // add the END line

            // outer loop already moved one past END 

            //  (ProtM, Base_IO, etc.) 
            int j = i;
            while (j < lines.Count &&
                   !lines[j].TrimStart().StartsWith("BEGIN COM_CHANNEL", StringComparison.OrdinalIgnoreCase))
            {
                string t = lines[j].TrimStart();

                // hit a TEMPLATE?  stop – that’s a different context
                if (t.StartsWith("BEGIN TEMPLATE", StringComparison.OrdinalIgnoreCase))
                    break;

                block.Add(lines[j]);
                j++;
            }

            // gather ID + IPs
            string id = null;
            var ips = new List<string>();

            foreach (var b in block)
            {
                string t = b.Trim();

                if (isChannel)
                {
                    // COM_CHANNEL
                    if (t.StartsWith("ID=", StringComparison.OrdinalIgnoreCase))
                    {
                        id = ValueAfterEqual(t);
                        int cut = id.IndexOf("_ComC", StringComparison.OrdinalIgnoreCase);
                        if (cut > 0) id = id.Substring(0, cut);
                    }
                    if (t.StartsWith("ADDRESS=", StringComparison.OrdinalIgnoreCase))
                    {
                        var ip = IpFromLine(t);
                        if (ip != null) ips.Add(ip);
                    }
                }
                else
                {
                    //TEMPLATE
                    if (t.StartsWith("<REPLACE_NAME", StringComparison.OrdinalIgnoreCase))
                        id = ValueAfterEqual(t);

                    if (t.StartsWith("<REPLACE_IP_", StringComparison.OrdinalIgnoreCase) ||
                        t.StartsWith("<REPLACE_ADDRESS>=", StringComparison.OrdinalIgnoreCase))
                    {
                        var ip = IpFromLine(t);
                        if (ip != null) ips.Add(ip);
                    }
                }
            }

            // simulator channels are noise – do not record them
            if (id == null || id.EndsWith("SimCom", StringComparison.OrdinalIgnoreCase))
                continue;

            // classify *after* we appended ProtM / Base_IO lines, so CCR etc.
            // are actually detectable
            string type = DetectTypeFromLines(block);

            foreach (var ip in ips)
                yield return new IpUsage(ip, type, id, file);
        }
    }


    //  flips booleans if it runs across well-known substrings; order
    //  of the final ifs sets precedence (CCR beats Controller beats UPS etc.)
    private static string DetectTypeFromLines(IEnumerable<string> lines)
    {
        bool ccr = false, ctrl = false, ups = false, plc = false, sw = false;

        foreach (var raw in lines)
        {
            string s = raw.Trim();
            string[] switchTokens = { "SWITCH", "WESTERMO", "CISCO" };

            // “UPS” / “PLC” in ID is already enough
            if (s.IndexOf("UPS", StringComparison.OrdinalIgnoreCase) >= 0) ups = true;
            if (s.IndexOf("PLC", StringComparison.OrdinalIgnoreCase) >= 0) plc = true;
            if (switchTokens.Any(tok => // there's so many types of switches that this is the easiest way to not fill this file with \more\ crap
                s.IndexOf(tok, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                sw = true;
            }

            // look for namespace clues
            if (s.IndexOf(".Ups.", StringComparison.OrdinalIgnoreCase) >= 0) ups = true;
            if (s.IndexOf(".Plc.", StringComparison.OrdinalIgnoreCase) >= 0 ||
                s.IndexOf(".S7.", StringComparison.OrdinalIgnoreCase) >= 0) plc = true;
            if (s.IndexOf(".Ccr.", StringComparison.OrdinalIgnoreCase) >= 0) ccr = true;
            if (s.IndexOf(".Controller.", StringComparison.OrdinalIgnoreCase) >= 0) ctrl = true;
            //if (s.IndexOf(".Westermo.", StringComparison.OrdinalIgnoreCase) >= 0) sw = true;
        }

        if (ccr) return "CCR";
        if (ctrl) return "Controller";
        if (ups) return "UPS";
        if (plc) return "PLC";
        if (sw) return "Switch";
        return "COM_CHANNEL";   // fallback no clue about type found
    }

    //  helper
    //  scans a single text line for the first IP address and hands it back.
    //  if the regex fails, return null so the caller can ignore it
    private static string IpFromLine(string line)
    {
        var m = _ipRx.Match(line);          // _ipRx = 1-liner “d.d.d.d” regex
        return m.Success ? m.Value : null;  // null = no IP on that line
    }

    // helper 
    //  “key=value” splitter
    //  finds the first "="
    //  returns everything after it, trimmed and without quotes
    //  If there is no "=" we yield an empty string rather than throw
    private static string ValueAfterEqual(string line)
    {
        int eq = line.IndexOf('=');                       // first '='
        return eq >= 0
            ? line.Substring(eq + 1).Trim().Trim('"')     //    value-part
            : string.Empty;                               //    no '=' found
    }

    // helper
    //  recursive directory crawler that never throws on unauthorized access, reparse points etc. 
	// f.eks. failing to access target and loop back into parent folder (infinite loop of DOOOOOM)
    //  uses our own stack so we stay away from the recursion limit basically avoid having a super 
	// deep tree and always be one frame deep no matter how many subfolders there are
    private static IEnumerable<string> SafeEnumerateFiles(string root, string pattern)
    {
        var todo = new Stack<string>();
        todo.Push(root);

        while (todo.Count > 0)
        {
            string dir = todo.Pop();

            // collect files (ignore “access denied”)
            string[] files = null;
            try { files = Directory.GetFiles(dir, pattern); } catch { } // skip
            if (files != null)
                foreach (var f in files)
                    yield return f;

            // push sub-folders for next loop
            string[] sub = null;
            try { sub = Directory.GetDirectories(dir); } catch { } // skip
            if (sub != null)
                foreach (var d in sub)
                    todo.Push(d);
        }
    }

    // helper 
    //  stream-based ReadAllLines that tolerates giant files and keeps RAM down
    //  Meaning that it reads one line at a time, instead of loading mega files into memory and crashing + burning
    //  caller gets an IEnumerable<string> so it can foreach without buffering and thus it can tolerate big fucking files
    private static IEnumerable<string> SafeReadLines(string file)
    {
        using var sr = new StreamReader(file);
        string line;
        while ((line = sr.ReadLine()) != null)
            yield return line;      // yield one line at a time
    }

}
