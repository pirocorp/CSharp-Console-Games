namespace Tetris.Extensions
{
    using Engine.UI.ConsoleRenderer;
    using Engine.UI.Info.HighScore;
    using Microsoft.Extensions.DependencyInjection;
    using Tetris.Engine;
    using Tetris.Engine.CollisionDetector;
    using Tetris.Engine.TetrisFigureProvider;
    using Tetris.Engine.UI;
    using Tetris.Engine.UI.Border;
    using Tetris.Engine.UI.Info;
    using Tetris.Engine.UI.TetrisField;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTetrisEngine(this IServiceCollection services)
        {
            services
                .AddUserInterface()
                .AddEngine();

            return services;
        }

        private static IServiceCollection AddUserInterface(this IServiceCollection services)
        {
            services
                .AddSingleton<IBorder, Border>()
                .AddSingleton<IInfo, Info>()
                .AddSingleton<IHighScore, HighScore>()
                .AddSingleton<ITetrisField, TetrisField>()
                .AddSingleton<IRenderer, ConsoleRenderer>()
                .AddSingleton<UserInterface>();

            return services;
        }

        private static IServiceCollection AddEngine(this IServiceCollection services)
        {
            services
                .AddSingleton<ICollisionDetector, CollisionDetector>()
                .AddSingleton<ITetrisFigureProvider, TetrisFigureProvider>()
                .AddSingleton<TetrisEngine>();

            return services;
        }
    }
}
