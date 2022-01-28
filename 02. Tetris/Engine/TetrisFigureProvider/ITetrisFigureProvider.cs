namespace Tetris.Engine.TetrisFigureProvider
{
    using Tetris.Engine.TetrisFigureProvider.Figures;

    public interface ITetrisFigureProvider
    {
        IFigure GetRandomFigure();
    }
}
