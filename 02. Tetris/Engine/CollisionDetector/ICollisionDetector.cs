namespace Tetris.Engine.CollisionDetector
{
    using Tetris.Engine.TetrisFigureProvider.Figures;

    public interface ICollisionDetector
    {
        bool GameOverCollision(IFigure figure, int figureRow, int figureCol);

        bool MoveDownCollision(IFigure figure, int figureRow, int figureCol);

        bool MoveLeftCollision(IFigure tetrisFigure, int figureRow, int figureCol);

        bool MoveRightCollision(IFigure tetrisFigure, int figureRow, int figureCol);
    }
}
