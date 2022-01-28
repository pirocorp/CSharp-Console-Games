namespace Tetris.Engine.Info
{
    public interface IInfo
    {
        void Render(int score, int frame, int currentFigureRow, int currentFigureCol);
    }
}
