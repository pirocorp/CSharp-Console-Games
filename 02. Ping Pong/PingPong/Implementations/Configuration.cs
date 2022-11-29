namespace PingPong.Implementations
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
        }

        public int FirstPlayerPadSize { get; private set; }

        public int SecondPlayerPadSize { get; private set; }

        public IConfiguration WithFirstPlayerPadSize(int padSize)
        {
            this.FirstPlayerPadSize = padSize;

            return this;
        }

        public IConfiguration WithSecondPlayerPadSize(int padSize)
        {
            this.SecondPlayerPadSize = padSize;

            return this;
        }
    }
}
