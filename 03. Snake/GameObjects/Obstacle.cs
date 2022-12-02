namespace Snake.GameObjects;

using System;

public class Obstacle : RenderableBase
{
    public Obstacle(
        Position position) 
        : base(position, ConsoleColor.Cyan, "=")
    {
    }
}
 