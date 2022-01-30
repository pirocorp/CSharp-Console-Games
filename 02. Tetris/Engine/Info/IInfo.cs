namespace Tetris.Engine.Info
{
    public interface IInfo
    {
        void AddScore(int score);

        void Render(int score, int frame, int level, int currentFigureRow, int currentFigureCol);
    }
}
