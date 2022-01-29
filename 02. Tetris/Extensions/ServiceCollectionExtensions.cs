namespace Tetris.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    using Tetris.Engine;
    using Tetris.Engine.Border;
    using Tetris.Engine.CollisionDetector;
    using Tetris.Engine.ConsoleRenderer;
    using Tetris.Engine.Info;
    using Tetris.Engine.TetrisField;
    using Tetris.Engine.TetrisFigureProvider;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTetrisEngine(this IServiceCollection services)
        {
            services.AddTransient(TetrisEngineFactory);

            return services;
        }

        private static TetrisEngine TetrisEngineFactory(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var border = scope.ServiceProvider.GetRequiredService<IBorder>();
            var collisionDetector = scope.ServiceProvider.GetRequiredService<ICollisionDetector>();
            var info = scope.ServiceProvider.GetRequiredService<IInfo>();
            var tetrisField = scope.ServiceProvider.GetRequiredService<ITetrisField>();
            var figureProvider = scope.ServiceProvider.GetRequiredService<ITetrisFigureProvider>();
            var renderer = scope.ServiceProvider.GetRequiredService<IRenderer>();

            var tetrisEngine = new TetrisEngine(border, collisionDetector, info, tetrisField, figureProvider, renderer);

            return tetrisEngine;
        }
    }
}
