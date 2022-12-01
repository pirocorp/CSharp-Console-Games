namespace Snake.GameObjects.Snake;

using System;

public class SnakeHead : SnakePart
{
    public SnakeHead(Position position) 
        : base(position, ConsoleColor.Gray, ">")
    {
        this.Direction = Direction.Right;
    }

    public Direction Direction { get; set; }

    public override string Representation 
        => this.Direction switch
        {
            Direction.Up => "^",
            Direction.Down => "v",
            Direction.Left => "<",
            Direction.Right => ">",
            _ => throw new ArgumentOutOfRangeException()
        };
}
