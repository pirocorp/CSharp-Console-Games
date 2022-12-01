namespace Snake.GameObjects.Snake;

using System;

public abstract class SnakePart : GameObject
{
    protected SnakePart(
        Position position, 
        ConsoleColor color, 
        string representation) 
        : base(position, color, representation)
    {
    }
}
