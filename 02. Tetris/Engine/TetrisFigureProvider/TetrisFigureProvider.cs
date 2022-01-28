namespace Tetris.Engine.TetrisFigureProvider
{
    using Tetris.Engine.TetrisFigureProvider.Figures;
    using Tetris.Engine.ConsoleRenderer;

    public class TetrisFigureProvider : ITetrisFigureProvider
    {
        private readonly Random random;
        private readonly IList<IFigure> tetrisFigures;

        public TetrisFigureProvider(IRenderer renderer)
        {
            this.random = new Random();
            this.tetrisFigures = GetTetrisFigures(renderer).ToList();
        }

        public IFigure GetRandomFigure()
            => this.tetrisFigures[this.random.Next(0, this.tetrisFigures.Count)];

        private static IEnumerable<IFigure> GetTetrisFigures(IRenderer renderer)
            => new List<IFigure>(8)
            {
                new FigureI(renderer),
                new FigureJ(renderer),
                new FigureL(renderer),
                new FigureO(renderer),
                new FigureS(renderer),
                new FigureT(renderer),
                new FigureZ(renderer),
            };
    }
}
