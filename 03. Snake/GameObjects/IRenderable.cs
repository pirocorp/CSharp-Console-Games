namespace Snake.GameObjects;

using System;
using Renderers;

public interface IRenderable
{
    public Position Position { get; }

    public ConsoleColor Color { get; }

    public string Representation { get; }

    public void Render(IRender render);
}
