namespace Snake.Renderers;

using System;

using Snake.GameObjects;

public class ConsoleRenderer : IRender
{
    public void Render(RenderableBase renderableBase)
        => this.Render(
            renderableBase.Position,
            renderableBase.Color,
            renderableBase.Representation);

    public void Render(
        Position position,
        ConsoleColor color,
        string item)
    {
        Console.SetCursorPosition(position.Col, position.Row);
        Console.ForegroundColor = color;
        Console.Write(item);
    }

    public void WriteMultiLine(
        Position position,
        ConsoleColor color,
        params string[] lines)
    {
        foreach (var line in lines)
        {
            WriteLine(position, color, line);
            position.Row++;
        }
    }

    public void WriteLine(Position position, ConsoleColor color, string line)
        => Render(
            position,
            color,
            $"{line}{Environment.NewLine}");

    public void Clear(RenderableBase renderableBase)
        => this.Clear(renderableBase.Position);

    public void Clear(Position position)
        => Render(position, ConsoleColor.Black, " ");
}
