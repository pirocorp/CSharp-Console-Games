namespace Snake.GameObjects;

using System;
using Renderers;

public abstract class GameObject : IRenderable
{
    protected GameObject(
        Position position,
        ConsoleColor color,
        string representation)
    {
        Position = position;
        Color = color;
        Representation = representation;
    }

    public Position Position { get; }

    public ConsoleColor Color { get; }

    public virtual string Representation { get; }

    public void Render(IRender render)
        => render.Render(this);
}
