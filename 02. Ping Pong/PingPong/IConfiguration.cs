namespace PingPong
{
    public interface IConfiguration
    {
        int FirstPlayerPadSize { get; }

        int SecondPlayerPadSize { get; }

        IConfiguration WithFirstPlayerPadSize(int padSize);

        IConfiguration WithSecondPlayerPadSize(int padSize);
    }
}
