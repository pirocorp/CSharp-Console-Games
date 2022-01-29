namespace Tetris.Engine.ConsoleRenderer
{
    using System.Text;

    using static GameConstants;

    public class ConsoleRenderer : IRenderer
    {
        private readonly HashSet<string> currentFrame;

        private readonly int windowHeight;
        private readonly int windowWidth;

        public ConsoleRenderer()
        {
            this.currentFrame = new HashSet<string>();

            this.windowHeight = (3 * BorderOffset) + TetrisFieldHeight;
            this.windowWidth = (3 * BorderOffset) + TetrisFieldWidth + InfoFieldWidth;

            this.ConfigureConsole();
        }

        public void DisplayGameOverMessage(int score)
        {
            var rowOffset = (this.windowHeight - 2) / 2;

            var message = string.Format(GameOverMessage, score);
            var messageOffset = (this.windowWidth - message.Length) / 2;
            var renderedMessage = $"{new string(' ', messageOffset)}{message}";

            this.RenderObject(renderedMessage, rowOffset, 0, GameOverColor);

            var top = $"╔{new string('═', this.windowWidth - 2)}╗";
            var bottom = $"╚{new string('═', this.windowWidth - 2)}╝";

            this.RenderObject(top, rowOffset - 1, 0, GameOverColor);
            this.RenderObject("║", rowOffset, 0, GameOverColor);
            this.RenderObject("║", rowOffset, this.windowWidth - 1, GameOverColor);
            this.RenderObject(bottom, rowOffset + 1, 0, GameOverColor);
        }

        public void NewFrame() => this.currentFrame.Clear();

        public void RenderObject(
            string text,
            int row = 0,
            int col = 0,
            ConsoleColor color = ConsoleColor.Gray)
        {
            if (this.currentFrame.Contains($"{row}{col}{color}{text}"))
            {
                return;
            }

            this.currentFrame.Add($"{row}{col}{color}{text}");

            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.ResetColor();
        }

        private void ConfigureConsole()
        {
            Console.Title = ApplicationName;

            if (OperatingSystem.IsWindows())
            {
                Console.WindowHeight = this.windowHeight;
                Console.WindowWidth = this.windowWidth;

                Console.BufferHeight = this.windowHeight;
                Console.BufferWidth = this.windowWidth;
            }

            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}
