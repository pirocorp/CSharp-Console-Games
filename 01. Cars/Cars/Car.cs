namespace Cars
{
    using System;

    public class Car
    {
        private readonly int _playfieldWidth;
        private readonly int _playfieldHeight;

        /// <summary>
        /// Random car constructor
        /// </summary>
        /// <param name="playfieldWidth">Play field width</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="carSymbol">Car symbol</param>
        /// <param name="color">Car color</param>
        public Car(int playfieldWidth, int playfieldHeight,
            int x, int y, char carSymbol, 
            ConsoleColor color = ConsoleColor.Gray)
        {
            this._playfieldWidth = playfieldWidth;
            this._playfieldHeight = playfieldHeight;

            this.X = x;
            this.Y = y;
            this.CarSymbol = carSymbol;
            this.Color = color;
        }

        /// <summary>
        /// Returns X coordinate of the car
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Returns Y coordinate of the car
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Returns Car Symbol
        /// </summary>
        public char CarSymbol { get; }

        /// <summary>
        /// Returns Car Color
        /// </summary>
        public ConsoleColor Color { get; }

        /// <summary>
        /// Moves car one position to the left
        /// </summary>
        public void MoveLeft()
        {
            this.X = Math.Max(0, this.X - 1);
        }

        /// <summary>
        /// Moves car one position to the right
        /// </summary>
        public void MoveRight()
        {
            this.X = Math.Min(this._playfieldWidth - 1, this.X + 1);
        }

        /// <summary>
        /// Moves car down one position
        /// </summary>
        public void MoveDown()
        {
            this.Y = Math.Min(this._playfieldHeight - 1, this.Y + 1);
        }
    }
}
