namespace Tetris.Engine.TetrisField
{
    using Tetris.Engine.TetrisFigureProvider.Figures;

    public interface ITetrisField
    {
        bool this[int row, int col] { get; }

        void AddFigure(IFigure inputFigure, int figureRow, int figureCol);

        void Render();
    }
}
