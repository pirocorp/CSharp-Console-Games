namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    public class FigureI : FigureBase
    {
        public FigureI(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { true, true, true, true },
            };
        }
    }
}
