namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.UI.ConsoleRenderer;

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
