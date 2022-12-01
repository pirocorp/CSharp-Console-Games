namespace Snake.Renderers;

using Snake.GameObjects;

using System;

public interface IRender
{
    void Render(IRenderable gameObject);

    void Render(Position position, ConsoleColor color, string item);

    void WriteMultiLine(Position position, ConsoleColor color, params string[] lines);

    void WriteLine(Position position, ConsoleColor color, string line);

    void Clear(IRenderable gameObject);

    void Clear(Position position);
}
