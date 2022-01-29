namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    public class FigureZ : FigureBase
    {
        public FigureZ(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { true, true, false },
                { false, true, true },
            };
        }
    }
}
