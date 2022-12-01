namespace Snake.GameObjects.Snake;

using System;

public class SnakeBodyPart : SnakePart
{
    public SnakeBodyPart(Position position)
        : base(position, ConsoleColor.DarkGray, "*")
    {
    }
}
