[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Snake;

using Engine;

// TODO: Extract Snake class and Add Abstract Factory for Game Objects
public static class Program
{
    public static void Main()
    {
        var engine = new SnakeEngine();
        engine.Run();
    }
}
