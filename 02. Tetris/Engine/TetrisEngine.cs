namespace Tetris.Engine
{
    using Tetris.Engine.CollisionDetector;
    using Tetris.Engine.Music;
    using Tetris.Engine.TetrisFigureProvider;
    using Tetris.Engine.TetrisFigureProvider.Figures;
    using Tetris.Engine.UI;

    using static GameConstants;

    public class TetrisEngine : IDisposable
    {
        private readonly ICollisionDetector collisionDetector;
        private readonly ITetrisFigureProvider figureProvider;
        private readonly Random random;
        private readonly TetrisSounds tetrisSounds;
        private readonly UserInterface userInterface;

        private int currentFigureRow;
        private int currentFigureCol;
        private IFigure currentFigure = default!;
        private int frame;
        private int gameSpeedDenominator;
        private GameState gameState;
        private int level;
        private int lines;
        private int score;
        private bool disposedValue;

        public TetrisEngine(
            ICollisionDetector collisionDetector,
            ITetrisFigureProvider figureProvider,
            UserInterface userInterface)
        {
            this.collisionDetector = collisionDetector;
            this.figureProvider = figureProvider;
            this.random = new Random();
            this.tetrisSounds = new TetrisSounds();
            this.userInterface = userInterface;

            this.frame = 0;
            this.gameSpeedDenominator = TetrisInitialSpeedDenominator;
            this.level = 1;
            this.lines = 0;
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

            this.userInterface.GameOver(this.score);
            Console.ReadLine();
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.tetrisSounds.Dispose();
                }

                this.disposedValue = true;
            }
        }

        private void Scoring()
        {
            var currentLines = this.userInterface.GetFullLines();

            if (currentLines is 0)
            {
                this.tetrisSounds.PlayFall();

                return;
            }

            var maxLevel = TetrisInitialSpeedDenominator / LevelSpeedIncrease;

            this.lines += currentLines;

            var currentLevel = this.lines / LevelChangeThreshold;

            this.level = Math.Min(maxLevel, 1 + currentLevel);
            this.gameSpeedDenominator = TetrisInitialSpeedDenominator - (this.level * LevelSpeedIncrease);

            this.score += ScorePerLines[currentLines] * this.level;

            if (currentLines is 4)
            {
                this.tetrisSounds.PlayTetris();
            }
            else
            {
                this.tetrisSounds.PlayClear();
            }
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
            this.tetrisSounds.StopMusic();

            return true;
        }

        private void GameStart()
        {
            this.gameState = GameState.Started;
            this.userInterface.Initialize();

            if (MusicIsEnabled)
            {
#pragma warning disable CS0162 // Unreachable code detected
                this.tetrisSounds.PlayMusic();
#pragma warning restore CS0162 // Unreachable code detected
            }
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
                this.userInterface.RequestNewFrame();
            }
        }

        private void Render()
        {
            this.userInterface.Render(
                this.currentFigure,
                this.score,
                this.frame,
                this.level,
                this.currentFigureRow,
                this.currentFigureCol);
        }

        private void UpdateState()
        {
            if (this.frame % this.gameSpeedDenominator != 0)
            {
                return;
            }

            this.frame = 0;
            this.userInterface.RequestNewFrame();

            var hasCollision =
                this.collisionDetector
                    .MoveDownCollision(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

            if (!hasCollision)
            {
                this.currentFigureRow++;

                return;
            }

            this.userInterface.AddFigure(this.currentFigure, this.currentFigureRow, this.currentFigureCol);

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
