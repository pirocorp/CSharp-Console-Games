namespace Tetris.Engine.Info
{
    using Tetris.Engine.ConsoleRenderer;

    using static GameConstants;

    public class Info : IInfo
    {
        private readonly IRenderer renderer;

        public Info(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public void Render(int score, int frame, int currentFigureRow, int currentFigureCol)
        {
            this.renderer.RenderObject("Score:", 1, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject(score.ToString(), 2, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("Frame:", 4, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject(frame.ToString(), 5, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("Position:", 7, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject($"{currentFigureRow},{currentFigureCol}", 8, 3 + TetrisFieldWidth, ScoreColor);

            this.renderer.RenderObject("Keys:", 10, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject($"    ^", 12, 3 + TetrisFieldWidth, ScoreColor);
            this.renderer.RenderObject($"  < v >", 13, 3 + TetrisFieldWidth, ScoreColor);
        }
    }
}
