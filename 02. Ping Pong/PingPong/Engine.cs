namespace PingPong
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    using Microsoft.Extensions.DependencyInjection;

    public class Engine
    {
        private readonly IConfiguration configuration;
        private readonly IBall ball;
        private readonly IPlayer player1;
        private readonly IPlayer player2;


        public Engine(IServiceProvider serviceProvider)
        {
            this.configuration = serviceProvider.GetRequiredService<IConfiguration>();
            this.ball = serviceProvider.GetRequiredService<IBall>();
            this.player1 = serviceProvider.GetRequiredService<IPlayer>();
            this.player2 = serviceProvider.GetRequiredService<IPlayer>();

            this.RemoveScrollBars();
        }

        public void Run()
        {
            while (true)
            {
                // Move first player
                // Move second player (AI)
                // Move ball
                // Redraw all
                // - clear all
                // - draw first
                // - draw second
                // - draw ball
                // - print score
                Thread.Sleep(60);
            }
        }

        private void RemoveScrollBars()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.BufferHeight = Console.WindowHeight;
                Console.BufferWidth = Console.WindowWidth;
            }
        }
    }
}
