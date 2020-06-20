namespace Cars
{
    using System;

    public interface IGameContext
    {
        public int InitialGameSpeed { get; }
    
        public int MaxGameSpeed { get; }

        public int OpponentCarGenerationSpeed { get; }

        public int PlayfieldWidth { get; }

        public int PlayfieldHeight { get; }

        public int ConsoleWidth { get; }

        public int ConsoleHeight { get; }

        public int InfoOffset { get; }

        public int PlayerCarXCoordinate { get; }

        public int PlayerCarYCoordinate { get; }

        public char PlayerCarSymbol { get; }

        public ConsoleColor PlayerCarColor { get; }

        public char OpponentCarSymbol { get; }

        public ConsoleColor OpponentCarColor { get; }

        public int OpponentCarStartYCoordinate { get; }

        public int NextLevelThreshold { get; }

        public int PlayerInitialLives { get; }
    }
}
