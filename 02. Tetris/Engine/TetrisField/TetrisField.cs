namespace Tetris.Engine.TetrisField
{
    using Tetris.Engine.ConsoleRenderer;
    using Tetris.Engine.TetrisFigureProvider.Figures;

    using static GameConstants;

    public class TetrisField : ITetrisField
    {
        private readonly IRenderer renderer;
        private readonly bool[,] tetrisField;

        public TetrisField(IRenderer renderer)
        {
            this.renderer = renderer;
            this.tetrisField = new bool[TetrisFieldHeight, TetrisFieldWidth];
        }

        public bool this[int row, int col] => this.tetrisField[row, col];

        public void AddFigure(IFigure inputFigure, int figureRow, int figureCol)
        {
            var figure = inputFigure.Figure;

            for (var row = 0; row < figure.GetLength(0); row++)
            {
                for (var col = 0; col < figure.GetLength(1); col++)
                {
                    if (figure[row, col])
                    {
                        this.tetrisField[figureRow + row, figureCol + col] = true;
                    }
                }
            }
        }

        public int GetFullLines()
        {
            var lines = 0;

            for (var row = 0; row < this.tetrisField.GetLength(0); row++)
            {
                if (this.IsRowFull(row))
                {
                    this.MoveAboveRowsDown(row);
                    lines++;
                }
            }

            return lines;
        }

        public void Render()
        {
            for (var row = 0; row < this.tetrisField.GetLength(0); row++)
            {
                var result = string.Empty;

                for (var col = 0; col < this.tetrisField.GetLength(1); col++)
                {
                    result += this.tetrisField[row, col] ? BlockCharacter.ToString() : " ";
                }

                this.renderer.RenderObject(result, row + BorderOffset, BorderOffset, TetrisColor);
            }
        }

        private bool IsRowFull(int row)
        {
            for (var col = 0; col < this.tetrisField.GetLength(1); col++)
            {
                if (!this.tetrisField[row, col])
                {
                    return false;
                }
            }

            return true;
        }

        private void MoveAboveRowsDown(int row)
        {
            for (var rowToMove = row; rowToMove > 0; rowToMove--)
            {
                for (var col = 0; col < this.tetrisField.GetLength(1); col++)
                {
                    this.tetrisField[rowToMove, col] = this.tetrisField[rowToMove - 1, col];
                }
            }
        }
    }
}
