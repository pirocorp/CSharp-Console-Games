namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    public interface IFigure
    {
        bool[,] Figure { get; }

        void Render(int rowOffset, int colOffset);
    }
}
