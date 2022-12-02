namespace Snake.Renderers;

using Snake.GameObjects;

using System;

public interface IRender
{
    void Render(RenderableBase renderableBase);

    void Render(Position position, ConsoleColor color, string item);

    void WriteMultiLine(Position position, ConsoleColor color, params string[] lines);

    void WriteLine(Position position, ConsoleColor color, string line);

    void Clear(RenderableBase renderableBase);

    void Clear(Position position);
}
