namespace Cars
{
    using System;

    public class DefaultGameContext : IGameContext
    {
        public int InitialGameSpeed => 33;

        public int MaxGameSpeed => 10;

        public int OpponentCarGenerationSpeed => 2;

        public int PlayfieldWidth => 10;

        public int PlayfieldHeight => 20;

        public int ConsoleWidth => this.PlayfieldWidth + 20;

        public int ConsoleHeight => this.PlayfieldHeight;

        public int InfoOffset => 5;

        public int PlayerCarXCoordinate => this.PlayfieldWidth / 2;

        public int PlayerCarYCoordinate => this.PlayfieldHeight - 1;

        public char PlayerCarSymbol => '@';

        public ConsoleColor PlayerCarColor => ConsoleColor.Yellow;

        public char OpponentCarSymbol => '#';

        public ConsoleColor OpponentCarColor => ConsoleColor.Green;

        public int OpponentCarStartYCoordinate => 0;

        public int NextLevelThreshold => 10;

        public int PlayerInitialLives => 5;
    }
}
