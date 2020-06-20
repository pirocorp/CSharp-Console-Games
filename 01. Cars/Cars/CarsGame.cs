namespace Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class CarsGame
    {
        private const int INITIAL_GAME_SPEED = 33;
        private const int OPPONENT_CAR_GENERATION_SPEED = 2;

        private const int PLAYFIELD_WIDTH = 10;
        private const int PLAYFIELD_HEIGHT = 20;

        private const int CONSOLE_WIDTH = 40;
        private const int CONSOLE_HEIGHT = 20;

        private const int INFO_OFFSET = 5;

        private const int PLAYER_CAR_X_COORDINATE = PLAYFIELD_WIDTH / 2;
        private const int PLAYER_CAR_Y_COORDINATE = PLAYFIELD_HEIGHT - 1;

        private const char PLAYER_CAR_SYMBOL = '@';
        private const ConsoleColor PLAYER_CAR_COLOR = ConsoleColor.Yellow;

        private const char OPPONENT_CAR_SYMBOL = '#';
        private const ConsoleColor OPPONENT_CAR_COLOR = ConsoleColor.Green;
        private const int OPPONENT_CAR_START_Y_COORDINATE = 0;

        private const int NEXT_LEVEL_THRESHOLD = 10;

        private readonly Random _randomGenerator;
        private Queue<ConsoleKey> _pressedKeys;

        private bool _isCollisionDetected;
        private bool _isPlayerActionRegistered;
        private int _livesCount;
        private int _score;
        private int _level;
        private int _gameSpeed;

        private readonly Car _userCar;
        private IList<Car> _opponentsCars;

        /// <summary>
        /// Constructor of Cars Game object
        /// </summary>
        public CarsGame()
        {
            Console.BufferHeight = Console.WindowHeight = CONSOLE_HEIGHT;
            Console.BufferWidth = Console.WindowWidth = CONSOLE_WIDTH;

            Console.CursorVisible = false;

            this._randomGenerator = new Random();

            this._livesCount = 5;
            this._score = 0;
            this._level = 1;
            this._gameSpeed = INITIAL_GAME_SPEED - this._level;

            this._pressedKeys = new Queue<ConsoleKey>();
            this._opponentsCars = new List<Car>();

            this._userCar = new Car(
                PLAYFIELD_WIDTH,
                PLAYFIELD_HEIGHT,
                PLAYER_CAR_X_COORDINATE,
                PLAYER_CAR_Y_COORDINATE,
                PLAYER_CAR_SYMBOL,
                PLAYER_CAR_COLOR);
        }

        /// <summary>
        /// Game loop
        /// </summary>
        public void Run()
        {
            var iteration = 0;

            while (this._livesCount > 0)
            {
                //Player actions (key pressed)
                this.RegisterPlayerInput();
                //Process players actions
                this.ProcessPlayerActions();

                if (iteration % _gameSpeed == 0)
                {
                    //Generate opponent car
                    this.GenerateOpponentCar();
                    //Move cars
                    this.MoveCars(iteration);
                }

                if (iteration % _gameSpeed == 0
                || this._isPlayerActionRegistered)
                {
                    //Collision detection 
                    this.CollisionDetector();
                    //Clear the console
                    Console.Clear();
                    //ReDraw play field
                    this.DrawGameObjects();
                    //Draw info
                    this.DrawInfo();

                    this._isPlayerActionRegistered = false;
                }

                if (this._isCollisionDetected)
                {
                    this._opponentsCars = new List<Car>();
                    this._isCollisionDetected = false;
                }
                
                //Slow down program
                Thread.Sleep(5); //For around 200 FPS
                iteration = (iteration + 1) % 1000;

                var newLevel = (this._score / NEXT_LEVEL_THRESHOLD);
                newLevel = Math.Min(INITIAL_GAME_SPEED - 1, newLevel);

                if (this._level != newLevel)
                {
                    this._level = newLevel;
                    this._gameSpeed = INITIAL_GAME_SPEED - this._level;
                    iteration = 1;
                }
            }

            //Print Game Over Screen
            this.PrintGameOverScreen();
        }

        /// <summary>
        /// Checks if key is pressed and register it
        /// </summary>
        private void RegisterPlayerInput()
        {
            var keys = new Queue<ConsoleKey>();

            while (Console.KeyAvailable)
            {
                var readKey = Console.ReadKey(true);

                keys.Enqueue(readKey.Key);

                this._isPlayerActionRegistered = true;
            }

            this._pressedKeys = keys;
        }

        /// <summary>
        /// Process key pressed by the player
        /// </summary>
        private void ProcessPlayerActions()
        {
            while (this._pressedKeys.Count > 0)
            {
                var currentKey = this._pressedKeys.Dequeue();

                switch (currentKey)
                {
                    case ConsoleKey.LeftArrow:
                        this._userCar.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        this._userCar.MoveRight();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// On every game loop iteration adds new opponent car
        /// </summary>
        private void GenerateOpponentCar()
        {
            if (this._randomGenerator.Next(0, OPPONENT_CAR_GENERATION_SPEED) == 1)
            {
                var car = new Car(
                    PLAYFIELD_WIDTH,
                    PLAYFIELD_HEIGHT,
                    this._randomGenerator.Next(0, PLAYFIELD_WIDTH),
                    OPPONENT_CAR_START_Y_COORDINATE,
                    OPPONENT_CAR_SYMBOL,
                    OPPONENT_CAR_COLOR);

                this._opponentsCars.Add(car);
            }
        }

        /// <summary>
        /// Move opponents cars
        /// </summary>
        private void MoveCars(int iteration)
        {
            var points = this._opponentsCars.Count;

            //Clear cars who are beyond playfield
            this._opponentsCars = this._opponentsCars
                .Where(c => c.Y < PLAYFIELD_HEIGHT - 1)
                .ToList();

            points -= this._opponentsCars.Count;
            this._score += points;

            foreach (var car in this._opponentsCars)
            {
                car.MoveDown();
            }
        }

        /// <summary>
        /// Collision detector
        /// </summary>
        private void CollisionDetector()
        {
            if (this._opponentsCars
                .Any(o => o.X == this._userCar.X
                          && o.Y == this._userCar.Y))
            {
                this._livesCount--;
                this._isCollisionDetected = true;
            }
        }

        /// <summary>
        /// Print given character with given color on given position
        /// </summary>
        /// <param name="x">Left (x coordinate)</param>
        /// <param name="y">Top (y coordinate)</param>
        /// <param name="c">Character to be printed on console</param>
        /// <param name="color">Color of the character</param>
        private void PrintOnPosition(int x, int y, string str, 
            ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        /// <summary>
        /// Print car on console
        /// </summary>
        /// <param name="car">Car</param>
        private void PrintCar(Car car)
        {
            this.PrintOnPosition(car.X, car.Y, 
                car.CarSymbol.ToString(), car.Color);
        }

        /// <summary>
        /// Draws all game objects
        /// </summary>
        /// <remarks>Player car and opponents cars</remarks>
        private void DrawGameObjects()
        {
            this.PrintCar(this._userCar);

            foreach (var car in this._opponentsCars)
            {
                this.PrintCar(car);
            }

            if (this._isCollisionDetected)
            {
                this.PrintOnPosition(
                    this._userCar.X,
                    this._userCar.Y,
                    "X",
                    ConsoleColor.Red);

                Console.Beep();
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Draw additional info display
        /// </summary>
        private void DrawInfo()
        {
            var x = PLAYFIELD_WIDTH + 2 * INFO_OFFSET;
            var y = INFO_OFFSET; 
            this.PrintOnPosition(x, y, $"Lives: {this._livesCount}", ConsoleColor.White);
            this.PrintOnPosition(x, y + 1, $"Score: {this._score}", ConsoleColor.White);
            this.PrintOnPosition(x, y + 2, $"Level: {this._level}", ConsoleColor.White);
        }

        /// <summary>
        /// Prints End Game screen.
        /// </summary>
        private void PrintGameOverScreen()
        {
            this.DrawInfo();
            
            this.PrintOnPosition(12, 12, "Game Over!", ConsoleColor.Red);
            this.PrintOnPosition(2, 14, "Press any key to close application.", ConsoleColor.Red);
            Console.ReadKey();
        }
    }
}
