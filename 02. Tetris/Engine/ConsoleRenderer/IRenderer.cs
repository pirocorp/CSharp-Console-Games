namespace Tetris.Engine.ConsoleRenderer
{
    public interface IRenderer
    {
        void RenderFrame();

        void RenderObject(
            string text,
            int row = 0,
            int col = 0,
            ConsoleColor color = ConsoleColor.Gray);
    }
}
