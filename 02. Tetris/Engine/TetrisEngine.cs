namespace Tetris.Engine
{
    using Tetris.Engine.Border;
    using Tetris.Engine.CollisionDetector;
    using Tetris.Engine.ConsoleRenderer;
    using Tetris.Engine.Info;
    using Tetris.Engine.TetrisField;
    using Tetris.Engine.TetrisFigureProvider;
    using Tetris.Engine.TetrisFigureProvider.Figures;

    using static GameConstants;

    public class TetrisEngine
    {
        private readonly IBorder border;
        private readonly ICollisionDetector collisionDetector;
        private readonly int gameSpeedDenominator;
        private readonly IInfo info;
        private readonly Random random;
        private readonly IRenderer renderer;
        private readonly ITetrisField tetrisField;
        private readonly ITetrisFigureProvider figureProvider;

        private int currentFigureRow;
        private int currentFigureCol;
        private IFigure currentFigure = default!;
        private int frame;
        private GameState gameState;
        private int score;

        public TetrisEngine(
            IBorder border,
            ICollisionDetector collisionDetector,
            IInfo info,
            ITetrisField tetrisField,
            ITetrisFigureProvider figureProvider,
            IRenderer renderer)
        {
            this.border = border;
            this.collisionDetector = collisionDetector;
            this.gameSpeedDenominator = TetrisInitialSpeedDenominator;
            this.info = info;
            this.random = new Random();
            this.renderer = renderer;
            this.tetrisField = tetrisField;
            this.figureProvider = figureProvider;

            this.frame = 0;
            this.score = 0;
            this.GetRandomFigure();
        }

        public void Run()
        {
            this.GameStart();

            while (this.gameState is GameState.Started)
            {
                this.frame++;
                this.ProcessUserInput();
                this.UpdateState();
                this.Render();
                Thread.Sleep(20);
            }

            this.renderer.DisplayGameOverMessage(this.score);
            Console.ReadLine();
        }

        private void Scoring()
        {
            var lines = this.tetrisField.GetFullLines();

            this.score += ScorePerLines[lines];
        }

        private void DrawCurrentFigure()
        {
            var rowOffset = this.currentFigureRow + BorderOffset;
            var colOffset = this.currentFigureCol + BorderOffset;

            this.currentFigure.Render(rowOffset, colOffset);
        }

        private void GetRandomFigure()
        {
            var figure = this.figureProvider.GetRandomFigure();

            this.currentFigureRow = 0;
            this.currentFigureCol = this.random.Next(0, TetrisFieldWidth + 1 - figure.Figure.GetLength(1));

            this.currentFigure = figure;
        }

        private bool GameOver()
        {
            this.gameState = GameState.Stopped;
            this.info.AddScore(this.score);

            return true;
        }

        private void GameStart()
        {
            this.gameState = GameState.Started;
            this.border.Render();
        }

        private bool MoveDown()
        {
            var maxPosition = TetrisFieldHeight - this.currentFigure.Figure.GetLength(0);

            var success = !this.collisionDetector
                .MoveDownCollision(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            if (success)
            {
                this.currentFigureRow = Math.Min(maxPosition, this.currentFigureRow + 1);
            }

            this.frame = this.currentFigureRow == maxPosition || !success ? this.gameSpeedDenominator : 1;

            return success;
        }

        private bool MoveLeft()
        {
            if (this.currentFigureCol <= 0)
            {
                return false;
            }

            var success = !this.collisionDetector
                .MoveLeftCollision(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            if (success)
            {
                this.currentFigureCol--;
            }

            return success;
        }

        private bool MoveRight()
        {
            if (this.currentFigureCol >= TetrisFieldWidth - this.currentFigure.Figure.GetLength(1))
            {
                return false;
            }

            var success = !this.collisionDetector
                .MoveRightCollision(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            if (success)
            {
                this.currentFigureCol++;
            }

            return success;
        }

        private bool Rotate()
        {
            var result = this.currentFigure.TryRotate();

            if (!result)
            {
                return result;
            }

            if (this.currentFigureCol > TetrisFieldWidth - this.currentFigure.Figure.GetLength(1))
            {
                this.currentFigure.UndoRotate();
                return false;
            }

            if (!this.collisionDetector.Collision(this.currentFigure, this.currentFigureRow, this.currentFigureCol))
            {
                return result;
            }

            this.currentFigure.UndoRotate();
            return false;
        }

        private void ProcessUserInput()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            var consoleKey = Console.ReadKey(true);
            var key = consoleKey.Key;

            var success = key switch
            {
                ConsoleKey.Escape => this.GameOver(),
                ConsoleKey.Spacebar or ConsoleKey.UpArrow or ConsoleKey.W => this.Rotate(),
                ConsoleKey.A or ConsoleKey.LeftArrow => this.MoveLeft(),
                ConsoleKey.D or ConsoleKey.RightArrow => this.MoveRight(),
                ConsoleKey.S or ConsoleKey.DownArrow => this.MoveDown(),
                _ => false
            };

            if (success)
            {
                this.renderer.NewFrame();
            }
        }

        private void Render()
        {
            this.info.Render(this.score, this.frame, this.currentFigureRow, this.currentFigureCol);
            this.tetrisField.Render();
            this.DrawCurrentFigure();
        }

        private void UpdateState()
        {
            if (this.frame % this.gameSpeedDenominator != 0)
            {
                return;
            }

            this.frame = 0;
            this.renderer.NewFrame();

            var hasCollision =
                this.collisionDetector
                    .MoveDownCollision(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            if (!hasCollision)
            {
                this.currentFigureRow++;

                return;
            }

            this.tetrisField.AddFigure(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            this.Scoring();

            this.GetRandomFigure();

            var isGameOver = this.collisionDetector
                .GameOverCollision(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            if (isGameOver)
            {
                this.GameOver();
            }
        }
    }
}
