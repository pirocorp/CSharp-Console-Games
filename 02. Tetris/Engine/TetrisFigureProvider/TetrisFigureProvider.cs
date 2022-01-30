namespace Tetris.Engine.TetrisFigureProvider
{
    using Tetris.Engine.TetrisFigureProvider.Figures;

    using Tetris.Engine.UI.ConsoleRenderer;

    public class TetrisFigureProvider : ITetrisFigureProvider
    {
        private readonly Random random;
        private readonly IRenderer renderer;
        private readonly IList<Type> tetrisFigures;

        public TetrisFigureProvider(IRenderer renderer)
        {
            this.random = new Random();
            this.renderer = renderer;
            this.tetrisFigures = GetTetrisFigures().ToList();
        }

        public IFigure GetRandomFigure()
        {
            var type = this.tetrisFigures[this.random.Next(0, this.tetrisFigures.Count)];
            var instance = (IFigure)Activator.CreateInstance(type, this.renderer)!;

            return instance;
        }

        private static IEnumerable<Type> GetTetrisFigures()
            => new List<Type>(8)
            {
                typeof(FigureI),
                typeof(FigureJ),
                typeof(FigureL),
                typeof(FigureO),
                typeof(FigureS),
                typeof(FigureT),
                typeof(FigureZ),
            };
    }
}
