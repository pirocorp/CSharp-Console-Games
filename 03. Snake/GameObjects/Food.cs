namespace Snake.GameObjects;

using System;

public class Food : RenderableBase
{
    public Food(int maxHeight, int maxWidth)
        : this(Position.GetRandomPosition(maxHeight, maxWidth))
    { }

    public Food(Position position) 
        : base(position, ConsoleColor.Yellow, "@")
    {
    }
}
