namespace Cars
{
    using System;

    public static class CarsProgram
    {
        public static void Main()
        {
            var gameContext = new DefaultGameContext();
            var game = new CarsGame(gameContext);
            game.Run();
        }
    }
}
