namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.ConsoleRenderer;

    using static GameConstants;

    public abstract class FigureBase : IFigure
    {
        private readonly IRenderer renderer;

        protected FigureBase(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract bool[,] Figure { get; }

        public void Render(int rowOffset, int colOffset)
        {
            for (var row = 0; row < this.Figure.GetLength(0); row++)
            {
                for (var col = 0; col < this.Figure.GetLongLength(1); col++)
                {
                    if (this.Figure[row, col])
                    {
                        var figureCol = col + colOffset;
                        var figureRow = row + rowOffset;

                        this.renderer.RenderObject(BlockCharacter.ToString(), figureRow, figureCol, TetrisColor);
                    }
                }
            }
        }
    }
}
