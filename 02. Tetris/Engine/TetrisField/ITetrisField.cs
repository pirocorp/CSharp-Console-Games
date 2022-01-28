namespace Tetris.Engine.TetrisField
{
    using Tetris.Engine.TetrisFigureProvider.Figures;

    public interface ITetrisField
    {
        void AddFigure(IFigure inputFigure, int figureRow, int figureCol);

        void Render();
    }
}
