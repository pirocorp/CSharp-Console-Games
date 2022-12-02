namespace Snake.GameObjects;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Extensions;
using Renderers;

public class Snake : IRenderable
{
    private readonly int height;
    private readonly int width;

    private Queue<Position> snakeElements;
    private Dictionary<Direction, Position> directions;

    private bool snakeIsFed;

    public Snake()
    {
        this.InitCollections();

        this.Direction = Direction.Right;

        this.height = Console.WindowHeight;
        this.width = Console.WindowWidth;

        this.Position = this.snakeElements.Last();
    }

    public Direction Direction { get; private set; }

    public int Size => this.snakeElements.Count;

    public Position Position { get; private set; }

    public IEnumerable<Position> Elements => this.snakeElements.AsEnumerable();

    private string HeadRepresentation
        => this.Direction switch
        {
            Direction.Up => "^",
            Direction.Down => "v",
            Direction.Left => "<",
            Direction.Right => ">",
            _ => throw new ArgumentOutOfRangeException()
        }; 

    public void Render(IRender render)
    {
        this.snakeElements
            .ForeEach(e => render.Render(e, ConsoleColor.DarkGray, "*"));

        render.Render(this.Position, ConsoleColor.Gray, this.HeadRepresentation);

        if (this.snakeIsFed)
        {
            return;
        }

        var lastSnakeElement = this.snakeElements.Dequeue();
        render.Clear(lastSnakeElement);
    }

    public bool Feed(Food food)
        => this.snakeIsFed 
            = (this.Position.Col == food.Position.Col)
              && (this.Position.Row == food.Position.Row);

    public Position MoveSnake(Direction direction)
    {
        this.Direction = direction;

        var newPosition = this.CalculateSnakeNewHeadPosition();
        this.Position = newPosition;

        this.snakeElements.Enqueue(newPosition);

        return newPosition;
    }

    private Position CalculateSnakeNewHeadPosition()
    {
        var oldSnakeHead = this.snakeElements.Last();

        var nextDirection = this.directions[this.Direction];

        var snakeNewHeadPosition = new Position(
            oldSnakeHead.Row + nextDirection.Row,
            oldSnakeHead.Col + nextDirection.Col);

        if (snakeNewHeadPosition.Col < 0) snakeNewHeadPosition.Col = this.width - 1;
        if (snakeNewHeadPosition.Row < 0) snakeNewHeadPosition.Row = this.height - 1;
        if (snakeNewHeadPosition.Row >= this.height) snakeNewHeadPosition.Row = 0;
        if (snakeNewHeadPosition.Col >= this.width) snakeNewHeadPosition.Col = 0;

        return snakeNewHeadPosition;
    }

    [MemberNotNull(nameof(directions))]
    [MemberNotNull(nameof(snakeElements))]
    private void InitCollections()
    {
        this.snakeElements = new Queue<Position>();

        for (var i = 0; i <= 5; i++)
        {
            this.snakeElements.Enqueue(new Position(0, i));
        }

        this.directions = new Dictionary<Direction, Position>
        {
            {Direction.Right, new (0, 1)},
            {Direction.Left, new (0, -1)},
            {Direction.Down, new (1, 0)},
            {Direction.Up, new (-1, 0)},
        };
    }
}
