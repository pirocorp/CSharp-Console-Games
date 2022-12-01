namespace Snake.GameObjects;

using System;

public class Obstacle : GameObject
{
    public Obstacle(
        Position position) 
        : base(position, ConsoleColor.Cyan, "=")
    {
    }
}
 