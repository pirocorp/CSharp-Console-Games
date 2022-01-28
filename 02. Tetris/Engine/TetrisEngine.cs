namespace Tetris.Engine
{
    using Tetris.Engine.Border;
    using Tetris.Engine.ConsoleRenderer;
    using Tetris.Engine.Info;
    using Tetris.Engine.TetrisField;
    using Tetris.Engine.TetrisFigureProvider;
    using Tetris.Engine.TetrisFigureProvider.Figures;

    using static GameConstants;

    public class TetrisEngine
    {
        private const int FallingSpeedDenominator = 60;

        private readonly IBorder border;
        private readonly IInfo info;
        private readonly Random random;
        private readonly IRenderer renderer;
        private readonly ITetrisField tetrisField;
        private readonly ITetrisFigureProvider figureProvider;

        private int currentFigureRow;
        private int currentFigureCol;
        private IFigure currentFigure;
        private int frame;
        private GameState gameState;
        private int score;

        public TetrisEngine(
            IBorder border,
            IInfo info,
            ITetrisField tetrisField,
            ITetrisFigureProvider figureProvider,
            IRenderer renderer)
        {
            this.border = border;
            this.info = info;
            this.random = new Random();
            this.renderer = renderer;
            this.tetrisField = tetrisField;
            this.figureProvider = figureProvider;

            this.frame = 0;
            this.score = 0;
            this.currentFigure = this.GetRandomFigure();
        }

        public void Run()
        {
            this.gameState = GameState.Started;

            while (this.gameState is GameState.Started)
            {
                this.frame++;
                this.ProcessUserInput();
                this.UpdateState();
                this.Render();
                Thread.Sleep(20);
            }
        }

        private bool CollisionDetection()
        {
            if (this.currentFigureRow == TetrisFieldHeight - this.currentFigure.Figure.GetLength(0))
            {
                return true;
            }

            return false;
        }

        private void DrawCurrentFigure()
        {
            var rowOffset = this.currentFigureRow + BorderOffset;
            var colOffset = this.currentFigureCol + BorderOffset;

            this.currentFigure.Render(rowOffset, colOffset);
        }

        private IFigure GetRandomFigure()
        {
            var figure = this.figureProvider.GetRandomFigure();

            this.currentFigureRow = 0;
            this.currentFigureCol = this.random.Next(0, TetrisFieldWidth + 1 - figure.Figure.GetLength(1));

            return figure;
        }

        private void MoveLeft()
            => this.currentFigureCol = Math.Max(0, this.currentFigureCol - 1);

        private void MoveRight()
        {
            if (this.currentFigureCol < TetrisFieldWidth - this.currentFigure.Figure.GetLength(1))
            {
                this.currentFigureCol++;
            }
        }

        private void ProcessUserInput()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            var consoleKey = Console.ReadKey();

            switch (consoleKey.Key)
            {
                case ConsoleKey.Escape:
                    this.gameState = GameState.Stopped;
                    break;
                case ConsoleKey.Spacebar:
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    // Rotate
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    this.MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    this.MoveRight();
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    this.frame = 1;
                    this.score++;

                    // TODO: Move Down
                    this.currentFigureRow++;
                    break;
                default:
                    return;
            }

            this.renderer.RenderFrame();
        }

        private void Render()
        {
            this.border.Render();
            this.info.Render(this.score, this.frame, this.currentFigureRow, this.currentFigureCol);
            this.DrawCurrentFigure();
            this.tetrisField.Render();
        }

        private void UpdateState()
        {
            if (this.frame % FallingSpeedDenominator == 0)
            {
                this.frame = 0;

                this.currentFigureRow++;
                this.renderer.RenderFrame();
            }

            if (this.CollisionDetection())
            {
                this.tetrisField.AddFigure(this.currentFigure, this.currentFigureRow, this.currentFigureCol);
                this.currentFigure = this.GetRandomFigure();

                this.renderer.RenderFrame();
            }
        }
    }
}
