namespace PingPong
{
    using Implementations;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        private const int FirstPlayerPadSize = 4;
        private const int SecondPlayerPadSize = 4;

        public static void Main()
        {
            var configuration = GetConfiguration();

            using var host = CreateHostBuilder(configuration).Build();

            var engine = new Engine(host.Services);
            engine.Run();
        }

        private static IConfiguration GetConfiguration()
            => new Configuration()
                .WithFirstPlayerPadSize(FirstPlayerPadSize)
                .WithSecondPlayerPadSize(SecondPlayerPadSize);

        private static IHostBuilder CreateHostBuilder(IConfiguration configuration) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services.AddSingleton(configuration)
                        .AddTransient<IBall, Ball>()
                        .AddTransient<IPlayer, Player>());
    }
}
