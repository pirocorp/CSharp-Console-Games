namespace Tetris
{
    using Microsoft.Extensions.DependencyInjection;

    using Tetris.Engine;
    using Tetris.Engine.Border;
    using Tetris.Engine.ConsoleRenderer;
    using Tetris.Engine.Info;
    using Tetris.Engine.TetrisField;
    using Tetris.Engine.TetrisFigureProvider;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var services = RegisterServices();
            var engine = services.GetRequiredService<TetrisEngine>();

            engine.Run();
        }

        internal static IServiceProvider RegisterServices()
            => new ServiceCollection()
                .AddTransient<IBorder, Border>()
                .AddTransient<IInfo, Info>()
                .AddTransient<TetrisEngine>()
                .AddTransient<ITetrisField, TetrisField>()
                .AddSingleton<ITetrisFigureProvider, TetrisFigureProvider>()
                .AddSingleton<IRenderer, ConsoleRenderer>()
                .BuildServiceProvider();
    }
}
