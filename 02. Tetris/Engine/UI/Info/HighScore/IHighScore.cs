namespace Tetris.Engine.UI.Info.HighScore
{
    public interface IHighScore
    {
        int Score { get; }

        void AddScore(int score);
    }
}
