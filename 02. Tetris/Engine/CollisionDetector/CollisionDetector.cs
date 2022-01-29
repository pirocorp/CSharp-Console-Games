namespace Tetris.Engine.CollisionDetector
{
    using Tetris.Engine.TetrisField;
    using Tetris.Engine.TetrisFigureProvider.Figures;

    using static GameConstants;

    public class CollisionDetector : ICollisionDetector
    {
        private readonly ITetrisField tetrisField;

        public CollisionDetector(ITetrisField tetrisField)
        {
            this.tetrisField = tetrisField;
        }

        public bool Collision(IFigure tetrisFigure, int figureRow, int figureCol)
        {
            var figure = tetrisFigure.Figure;

            for (var row = 0; row < figure.GetLength(0); row++)
            {
                for (var col = 0; col < figure.GetLength(1); col++)
                {
                    if (figure[row, col]
                        && this.tetrisField[figureRow + row, figureCol + col])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GameOverCollision(IFigure tetrisFigure, int figureRow, int figureCol)
        {
            if (!this.IsAtTheBottom(tetrisFigure, figureRow))
            {
                return
                    this.MoveDown(tetrisFigure, figureRow, figureCol);
            }

            return false;
        }

        public bool MoveDownCollision(IFigure tetrisFigure, int figureRow, int figureCol)
            => this.IsAtTheBottom(tetrisFigure, figureRow)
               || this.MoveDown(tetrisFigure, figureRow, figureCol);

        public bool MoveLeftCollision(IFigure tetrisFigure, int figureRow, int figureCol)
            => this.Collision(tetrisFigure, figureRow, figureCol - 1);

        public bool MoveRightCollision(IFigure tetrisFigure, int figureRow, int figureCol)
            => this.Collision(tetrisFigure, figureRow, figureCol + 1);

        private bool IsAtTheBottom(IFigure tetrisFigure, int figureRow)
        {
            var figure = tetrisFigure.Figure;

            if (figureRow >= TetrisFieldHeight - figure.GetLength(0))
            {
                return true;
            }

            return false;
        }

        private bool MoveDown(IFigure tetrisFigure, int figureRow, int figureCol)
            => this.Collision(tetrisFigure, figureRow + 1, figureCol);
    }
}
