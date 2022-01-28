namespace Tetris.Engine.ConsoleRenderer
{
    using System.Text;

    using static GameConstants;

    public class ConsoleRenderer : IRenderer
    {
        private readonly HashSet<string> currentFrame;

        public ConsoleRenderer()
        {
            this.currentFrame = new HashSet<string>();

            ConfigureConsole();
        }

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

        public void RenderFrame() => this.currentFrame.Clear();

        private static void ConfigureConsole()
        {
            Console.Title = ApplicationName;

            if (OperatingSystem.IsWindows())
            {
                var windowHeight = (3 * BorderOffset) + TetrisFieldHeight;
                var windowWidth = (3 * BorderOffset) + TetrisFieldWidth + InfoFieldWidth;

                Console.WindowHeight = windowHeight;
                Console.WindowWidth = windowWidth;

                Console.BufferHeight = windowHeight;
                Console.BufferWidth = windowWidth;
            }

            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}
