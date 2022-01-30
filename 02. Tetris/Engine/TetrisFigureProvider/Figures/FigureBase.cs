namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.UI.ConsoleRenderer;

    using static GameConstants;

    public abstract class FigureBase : IFigure
    {
        private readonly IRenderer renderer;

        private bool[,]? figure;
        private bool[,]? lastState;

        protected FigureBase(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public bool[,] Figure
        {
            get => this.figure ?? throw new InvalidOperationException(nameof(this.Figure));
            protected set => this.figure = value;
        }

        public void Render(int rowOffset, int colOffset)
        {
            for (var row = 0; row < this.Figure.GetLength(0); row++)
            {
                var line = string.Empty;

                var figureCol = colOffset;
                var figureRow = row + rowOffset;
                var found = false;

                for (var col = 0; col < this.Figure.GetLongLength(1); col++)
                {
                    found = this.Figure[row, col] || found;

                    if (!found)
                    {
                        figureCol++;
                    }

                    if (this.Figure[row, col])
                    {
                        line += BlockCharacter.ToString();
                    }
                }

                this.renderer.RenderObject(line, figureRow, figureCol, FigureColor);
            }
        }

        public virtual bool TryRotate()
        {
            this.lastState = this.figure;

            var sourceRows = this.Figure.GetLength(0);
            var sourceCols = this.Figure.GetLength(1);

            var newFigure = new bool[sourceCols, sourceRows];

            for (var row = 0; row < sourceRows; row++)
            {
                for (var col = 0; col < sourceCols; col++)
                {
                    var targetCol = sourceRows - row - 1;
                    newFigure[col, targetCol] = this.Figure[row, col];
                }
            }

            this.Figure = newFigure;

            return true;
        }

        public void UndoRotate()
        {
            if (this.lastState is null)
            {
                return;
            }

            this.figure = this.lastState;
            this.lastState = null;
        }
    }
}
