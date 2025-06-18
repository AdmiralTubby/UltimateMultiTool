// Immutable record: one IP address instance per file if they are the same.
namespace AbadUltimateTool.IpParser;

// <summary>One IP address plus the context in which it appears
public sealed class IpUsage
{
    public string Ip { get; }   // "172.16.1.23"
    public string Type { get; }   // "Controller", "CCR", "COM_CHANNEL"
    public string Name { get; }   // "CEND-2", "APP_TDZ_08_1", "Maint_UPS_ComC"
    public string FilePath { get; }   // absolute path

    public IpUsage(string ip, string type, string name, string filePath)
    {
        Ip = ip;
        Type = type;
        Name = name;
        FilePath = filePath;
    }
}
