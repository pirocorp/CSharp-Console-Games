namespace Snake;

using System;

public record Position(int Row, int Col)
{
    private static readonly Random RandomNumbersGenerator;

    static Position()
    {
        RandomNumbersGenerator = new Random();
    }

    public int Row { get; set; } = Row;

    public int Col { get; set; } = Col;

    public static Position GetRandomPosition(
        int maxHeight,
        int maxWidth)
        => GetRandomPosition(0, maxHeight, 0, maxWidth);

    public static Position GetRandomPosition(
        int minHeight, 
        int maxHeight,
        int minWidth,
        int maxWidth)
        => new Position(
            RandomNumbersGenerator.Next(minHeight, maxHeight),
            RandomNumbersGenerator.Next(minWidth, maxWidth));
}
