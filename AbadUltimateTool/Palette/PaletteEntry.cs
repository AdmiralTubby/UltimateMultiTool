using System.Drawing;


// --------------------------------------------------------------------------------------------
//  Plain immutable record for one palette line.
//  I’m keeping it in its own file so parsing logic and UI code can’t mutate it by accident.
// --------------------------------------------------------------------------------------------


namespace AbadUltimateTool.Palette;

public sealed class PaletteEntry
{
    public int Idx { get; }   // numeric colour ID
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public string Comment { get; }   // may be empty

    public PaletteEntry(int idx, byte r, byte g, byte b, string comment)
    {
        Idx = idx;
        R = r;
        G = g;
        B = b;
        Comment = comment;
    }

    /// convert the three channels into a System.Drawing.Color
    public Color ToColor() => Color.FromArgb(R, G, B);
}
