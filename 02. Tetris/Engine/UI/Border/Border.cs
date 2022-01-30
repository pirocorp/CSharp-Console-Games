namespace Tetris.Engine.UI.Border
{
    using System.Text;

    using Tetris.Engine.UI.ConsoleRenderer;

    using static Tetris.GameConstants;

    public class Border : IBorder
    {
        private readonly StringBuilder border;
        private readonly IRenderer renderer;

        public Border(IRenderer renderer)
        {
            this.border = new StringBuilder();
            this.renderer = renderer;

            this.PreRenderBorder();
        }

        public void Render()
        {
            this.renderer.RenderObject(this.border.ToString(), color: BorderColor);
        }

        private void PreRenderBorder()
        {
            this.BuildTopLine();

            for (var i = 0; i < TetrisFieldHeight; i++)
            {
                this.BuildMiddleLine();
            }

            this.BuildBottomLine();
        }

        private void BuildTopLine()
        {
            var line = $"╔{new string('═', TetrisFieldWidth)}╦{new string('═', InfoFieldWidth)}╗";

            this.border.AppendLine(line);
        }

        private void BuildMiddleLine()
        {
            var line = $"║{new string(' ', TetrisFieldWidth)}║{new string(' ', InfoFieldWidth)}║";

            this.border.AppendLine(line);
        }

        private void BuildBottomLine()
        {
            var line = $"╚{new string('═', TetrisFieldWidth)}╩{new string('═', InfoFieldWidth)}╝";

            this.border.AppendLine(line);
        }
    }
}
