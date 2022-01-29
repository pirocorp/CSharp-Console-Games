namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    public class FigureS : FigureBase
    {
        public FigureS(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { false, true, true },
                { true, true, false },
            };
        }
    }
}
