namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    public class FigureO : FigureBase
    {
        public FigureO(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { true, true },
                { true, true },
            };
        }

        public override bool TryRotate() => false;
    }
}
