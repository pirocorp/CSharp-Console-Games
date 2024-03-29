﻿namespace Tetris.Engine.TetrisFigureProvider.Figures
{
    using Tetris.Engine.UI.ConsoleRenderer;

    public class FigureT : FigureBase
    {
        public FigureT(IRenderer renderer)
            : base(renderer)
        {
            this.Figure = new bool[,]
            {
                { false, true, false },
                { true, true, true },
            };
        }
    }
}
