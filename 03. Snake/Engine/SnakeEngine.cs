namespace Snake.Engine;

using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

using Snake.GameObjects;
using Snake.Renderers;

public class SnakeEngine
{
    private const int FoodDisappearTime = 8000;
    private readonly IRender renderer;
    private readonly List<RenderableBase> obstacles;

    private int height;
    private int width;

    private int lastFoodTime = 0;
    private int negativePoints = 0;
    private double sleepTime = 100;
    private Direction currentDirection = Direction.Right;

    private Food food = new (0,0);
    private readonly Snake snake;

    public SnakeEngine()
    {
        this.ConsoleInit();

        this.obstacles = new List<RenderableBase>()
        {
            new Obstacle(new Position(12, 12)),
            new Obstacle(new Position(14, 20)),
            new Obstacle(new Position(7, 7)),
            new Obstacle(new Position(19, 19)),
            new Obstacle(new Position(6, 9)),
        };

        this.renderer = new ConsoleRenderer();
        this.snake = new Snake();
    }

    public void Run()
    {
        this.Initialize();

        while (true)
        {
            this.negativePoints++;
            this.ReadInput();

            this.snake.MoveSnake(this.currentDirection);

            if (this.DetectSnakeCollision())
            {
                return;
            }

            this.FeedSnake();
            this.snake.Render(this.renderer);

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

    private void Initialize()
    {
        this.obstacles.ForEach(o => o.Render(this.renderer));

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
        while (this.snake.Elements.Any(x => x == position) 
               || this.obstacles.Any(x => x.Position == position)
               || (this.food.Position == position));

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

    private bool DetectSnakeCollision()
    {
        var snakeNewHeadPosition = this.snake.Position;
        var snakeElements = snake.Elements
            .Take(this.snake.Size - 1)
            .ToArray();

        var collisionDetected
            = snakeElements.Any(x => x == snakeNewHeadPosition)
              || this.obstacles.Any(x => x.Position == snakeNewHeadPosition);

        if (collisionDetected)
        {
            var userPoints = (this.snake.Size - 7) * 100 - negativePoints;
            userPoints = Math.Max(userPoints, 0);

            var gameOver = new GameOverMessage(
                "Game over!",
                $"Your points are: {userPoints}");

            gameOver.Render(this.renderer);
        }

        return collisionDetected; 
    }

    private void FeedSnake()
    {
        if (!this.snake.Feed(this.food))
        {
            return;
        }

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

    private void MoveFood()
    {
        if (Environment.TickCount - this.lastFoodTime < FoodDisappearTime)
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
