namespace Tetris.Engine.UI
{
    using Tetris.Engine.TetrisFigureProvider.Figures;
    using Tetris.Engine.UI.Border;
    using Tetris.Engine.UI.ConsoleRenderer;
    using Tetris.Engine.UI.Info;
    using Tetris.Engine.UI.TetrisField;

    using static GameConstants;

    public class UserInterface
    {
        private readonly IBorder border;
        private readonly IInfo info;
        private readonly IRenderer renderer;
        private readonly ITetrisField tetrisField;

        public UserInterface(
            IBorder border,
            IInfo info,
            IRenderer renderer,
            ITetrisField tetrisField)
        {
            this.border = border;
            this.info = info;
            this.renderer = renderer;
            this.tetrisField = tetrisField;
        }

        public void AddFigure(IFigure figure, int figureRow, int figureCol)
            => this.tetrisField.AddFigure(figure, figureRow, figureCol);

        public void GameOver(int score)
        {
            this.info.AddScore(score);
            this.renderer.DisplayGameOverMessage(score);
        }

        public int GetFullLines() => this.tetrisField.GetFullLines();

        public void Initialize()
        {
            this.border.Render();
        }

        public void Render(
            IFigure figure,
            int score,
            int frame,
            int level,
            int currentFigureRow,
            int currentFigureCol)
        {
            this.info.Render(
                score,
                frame,
                level,
                currentFigureRow,
                currentFigureCol);

            this.tetrisField.Render();

            var rowOffset = currentFigureRow + BorderOffset;
            var colOffset = currentFigureCol + BorderOffset;

            figure.Render(rowOffset, colOffset);
        }

        public void RequestNewFrame()
        {
            this.renderer.NewFrame();
        }
    }
}
