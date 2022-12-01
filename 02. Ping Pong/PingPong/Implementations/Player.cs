namespace PingPong.Implementations
{
    public class Player : IPlayer
    {
        private int size;

        public Player(int size)
        {
            this.size = size;
        }

        public Player(int size, int y)
            : this(size)
        {
            this.Y = y;
        }

        public int Y { get; set; }
    }
}
