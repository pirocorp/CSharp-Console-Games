namespace PingPong.Implementations
{
    public class Ball : IBall
    {
        public Ball()
        {
            
        }

        public Ball(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
