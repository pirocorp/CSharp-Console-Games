namespace Tetris.Engine.Info
{
    using Tetris.Engine.ConsoleRenderer;
    using Tetris.Engine.Info.HighScore;

    using static GameConstants;

    public class Info : IInfo
    {
        private readonly IHighScore highScore;
        private readonly IRenderer renderer;

        public Info(
            IRenderer renderer,
            IHighScore highScore)
        {
            this.renderer = renderer;
            this.highScore = highScore;
        }

        public void AddScore(int score)
            => this.highScore.AddScore(score);

        public void Render(int score, int frame, int currentFigureRow, int currentFigureCol)
        {
            this.renderer.RenderObject("Score:", 1, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject(score.ToString(), 2, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("High Score:", 4, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject(this.highScore.Score.ToString(), 5, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("Frame:", 7, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject(frame.ToString(), 8, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("Position:", 10, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject($"{currentFigureRow},{currentFigureCol}", 11, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("Keys:", 13, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject($"    ^", 14, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject($"  < v >", 15, 3 + TetrisFieldWidth, ScoreColor);
        }
    }
}
