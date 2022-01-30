namespace Tetris.Engine.UI.ConsoleRenderer
{
    public interface IRenderer
    {
        void DisplayGameOverMessage(int score);

        void NewFrame();

        void RenderObject(
            string text,
            int row = 0,
            int col = 0,
            ConsoleColor color = ConsoleColor.Gray);
    }
}
