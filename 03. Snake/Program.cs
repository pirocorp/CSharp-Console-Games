[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Snake;

using Engine;

public static class Program
{
    public static void Main()
    {
        var engine = new SnakeEngine();
        engine.Run();
    }
}
