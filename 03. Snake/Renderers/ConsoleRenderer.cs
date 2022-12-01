namespace Snake.Renderers;

using System;

using Snake.GameObjects;

public class ConsoleRenderer : IRender
{
    public void Render(IRenderable gameObject)
        => this.Render(
            gameObject.Position,
            gameObject.Color,
            gameObject.Representation);

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

    public void Clear(IRenderable gameObject)
        => this.Clear(gameObject.Position);

    public void Clear(Position position)
        => Render(position, ConsoleColor.Black, " ");
}
