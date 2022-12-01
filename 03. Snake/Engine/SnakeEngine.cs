namespace Snake.Engine;

using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

using Extensions;
using GameObjects.Snake;
using Snake.GameObjects;
using Snake.Renderers;

public class SnakeEngine
{
    private const int FoodDisappearTime = 8000;
    private readonly IRender renderer = new ConsoleRenderer();
    private readonly List<Obstacle> obstacles = new ();
    private readonly Dictionary<Direction, Position> directions;

    private int height;
    private int width;

    private int lastFoodTime = 0;
    private int negativePoints = 0;
    private double sleepTime = 100;
    private Direction currentDirection = Direction.Right;

    private Food food = new (0,0);
    private readonly Queue<SnakePart> snakeElements = new ();

    public SnakeEngine()
    {
        this.directions = new Dictionary<Direction, Position>
        {
            {Direction.Right, new (0, 1)},
            {Direction.Left, new (0, -1)},
            {Direction.Down, new (1, 0)},
            {Direction.Up, new (-1, 0)},
        };

        this.ConsoleInit();
        this.GameObjectsInit();
    }

    public void Run()
    {
        this.Initialize();

        while (true)
        {
            this.negativePoints++;

            this.ReadInput();

            var oldSnakeHead = this.snakeElements.Last();
            var snakeNewHeadPosition = this.CalculateSnakeNewHeadPosition(oldSnakeHead);

            if (this.DetectSnakeCollision(snakeNewHeadPosition))
            {
                return;
            }

            this.renderer.Render(oldSnakeHead);
            this.snakeElements.Enqueue(new SnakeBodyPart(snakeNewHeadPosition));

            var snakeNewHead = new SnakeHead(snakeNewHeadPosition)
            {
                Direction = this.currentDirection
            };

            this.renderer.Render(snakeNewHead);

            this.FeedSnake(snakeNewHeadPosition);
            this.MoveFood();
            this.food.Render(this.renderer);

            this.sleepTime -= 0.01;
            Thread.Sleep((int)this.sleepTime);
        }
    }

    private void ConsoleInit()
    {
        Console.BufferHeight = Console.WindowHeight;

        this.height = Console.WindowHeight;
        this.width = Console.WindowWidth;
    }

    private void GameObjectsInit()
    {
        for (var i = 0; i <= 5; i++)
        {
            this.snakeElements.Enqueue(new SnakeBodyPart(new Position(0, i)));
        }

        var obstaclesObjects = new List<Obstacle>()
        {
            new(new Position(12, 12)),
            new(new Position(14, 20)),
            new(new Position(7, 7)),
            new(new Position(19, 19)),
            new(new Position(6, 9)),
        };

        this.obstacles.AddRange(obstaclesObjects);
    }

    private void Initialize()
    {
        this.obstacles.ForEach(o => o.Render(this.renderer));
        this.snakeElements.ForeEach(e => e.Render(this.renderer));

        var foodPosition = this.GetFreePosition();
        this.food = new Food(foodPosition);

        this.lastFoodTime = Environment.TickCount;
        this.renderer.Render(this.food);
    }

    private Position GetFreePosition()
    {
        Position position;

        do
        {
            position = Position.GetRandomPosition(this.height, this.width);
        }
        while (this.snakeElements.Any(x => x.Position == position) 
               || this.obstacles.Any(x => x.Position == position)
               || (this.food.Position.Row != position.Row 
                   && this.food.Position.Col != position.Row));

        return position;
    } 

    private void ReadInput()
    {
        if (Console.KeyAvailable)
        {
            this.currentDirection = Console.ReadKey().Key switch
            {
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                _ => this.currentDirection
            };
        }
    }

    private Position CalculateSnakeNewHeadPosition(SnakePart oldSnakeHead)
    {
        var nextDirection = this.directions[currentDirection];

        var snakeNewHeadPosition = new Position(
            oldSnakeHead.Position.Row + nextDirection.Row,
            oldSnakeHead.Position.Col + nextDirection.Col);

        if (snakeNewHeadPosition.Col < 0) snakeNewHeadPosition.Col = this.width - 1;
        if (snakeNewHeadPosition.Row < 0) snakeNewHeadPosition.Row = this.height - 1;
        if (snakeNewHeadPosition.Row >= this.height) snakeNewHeadPosition.Row = 0;
        if (snakeNewHeadPosition.Col >= this.width) snakeNewHeadPosition.Col = 0;

        return snakeNewHeadPosition;
    }

    private bool DetectSnakeCollision(Position snakeNewHeadPosition)
    {
        var collisionDetected
            = this.snakeElements.Any(x => x.Position == snakeNewHeadPosition)
              || this.obstacles.Any(x => x.Position == snakeNewHeadPosition);

        if (collisionDetected)
        {
            var userPoints = (this.snakeElements.Count - 6) * 100 - negativePoints;
            userPoints = Math.Max(userPoints, 0);

            this.renderer.WriteMultiLine(
                new Position(0, 0), 
                ConsoleColor.Red, 
                "Game over!",
                $"Your points are: {userPoints}");
        }

        return collisionDetected; 
    }

    private void FeedSnake(Position snakeNewHeadPosition)
    {
        if (snakeNewHeadPosition.Col == this.food.Position.Col 
            && snakeNewHeadPosition.Row == this.food.Position.Row)
        {
            var foodPosition = this.GetFreePosition();
            this.food = new Food(foodPosition);

            lastFoodTime = Environment.TickCount;
            this.renderer.Render(food);

            this.sleepTime--;

            var obstaclePosition = this.GetFreePosition();
            var obstacle = new Obstacle(obstaclePosition);

            this.obstacles.Add(obstacle);
            obstacle.Render(this.renderer);
        }
        else
        {
            var last = this.snakeElements.Dequeue();
            this.renderer.Clear(last);
        }
    }

    private void MoveFood()
    {
        if (Environment.TickCount - lastFoodTime < FoodDisappearTime)
        {
            return;
        }

        negativePoints += 50;
        this.renderer.Clear(food);

        var foodPosition = this.GetFreePosition();
        this.food = new Food(foodPosition);

        this.lastFoodTime = Environment.TickCount;
    }
}
