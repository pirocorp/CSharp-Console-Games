namespace Tetris
{
    public class GameConstants
    {
        public const string ApplicationName = "Tetris v1.0";

        public const char BlockCharacter = '▇';

        public const int BorderOffset = 1;

        public const string GameOverMessage = "Game Over! Score: {0}";

        public const string HighScoresPath = "scores.txt";

        public const int InfoFieldWidth = 15;

        public const int TetrisFieldWidth = 10;

        public const int TetrisFieldHeight = 20;

        public const int TetrisInitialSpeedDenominator = 50;

        public const ConsoleColor BorderColor = ConsoleColor.Gray;

        public const ConsoleColor GameOverColor = ConsoleColor.DarkRed;

        public const ConsoleColor FigureColor = ConsoleColor.DarkCyan;

        public const ConsoleColor ScoreColor = ConsoleColor.DarkYellow;

        public const ConsoleColor TetrisColor = ConsoleColor.DarkGray;

        public static readonly int[] ScorePerLines = { 0, 40, 100, 300, 1200 };
    }
}
