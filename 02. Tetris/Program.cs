namespace Tetris
{
    using Microsoft.Extensions.DependencyInjection;

    using Tetris.Engine;
    using Tetris.Extensions;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var engine = BuildTetrisEngine();
            engine.Run();
        }

        internal static TetrisEngine BuildTetrisEngine()
            => new ServiceCollection()
                .AddTetrisEngine()
                .BuildServiceProvider()
                .GetRequiredService<TetrisEngine>();
    }
}
