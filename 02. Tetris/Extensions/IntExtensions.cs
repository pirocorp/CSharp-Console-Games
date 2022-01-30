namespace Tetris.Extensions
{
    public static class IntExtensions
    {
        public static string PadRight(this int value, int pad)
        {
            var result = value.ToString();

            if (result.Length == pad)
            {
                return result;
            }

            var delta = pad - result.Length;

            return $"{new string(' ', delta)}{result}";
        }
    }
}
