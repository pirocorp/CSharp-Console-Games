namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    public class FigureJ : FigureBase
    {
        public FigureJ(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { true, false, false },
                { true, true, true },
            };
        }
    }
}
