namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    public class FigureL : FigureBase
    {
        public FigureL(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { false, false, true },
                { true, true, true },
            };
        }
    }
}
