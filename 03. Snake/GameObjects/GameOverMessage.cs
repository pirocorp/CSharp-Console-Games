namespace Snake.GameObjects;

using System;

public class GameOverMessage : RenderableBase
{
    public GameOverMessage(params string[] representation) 
        : base(new Position(0, 0), ConsoleColor.Red, string.Join(Environment.NewLine, representation))
    { }
}
