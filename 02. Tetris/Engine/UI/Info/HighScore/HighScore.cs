namespace Tetris.Engine.UI.Info.HighScore
{
    using System.Text.RegularExpressions;

    using Tetris.Engine.UI.ConsoleRenderer;

    using static GameConstants;

    public class HighScore : IHighScore
    {
        private readonly IRenderer renderer;

        public HighScore(IRenderer renderer)
        {
            this.renderer = renderer;

            this.LoadHighScore();
        }

        public int Score { get; private set; }

        public void AddScore(int score)
        {
            var text = new List<string>()
            {
                $"[{DateTime.UtcNow.ToLongDateString()}] {Environment.UserName} => {score}",
            };

            Task.Run(async () => await File.AppendAllLinesAsync(HighScoresPath, text));

            this.Score = Math.Max(score, this.Score);
        }

        private void LoadHighScore()
        {
            if (File.Exists(HighScoresPath))
            {
                var scores = File.ReadAllLines(HighScoresPath);

                foreach (var score in scores)
                {
                    var match = Regex.Match(score, @" => (?<score>[0-9]+)");
                    this.Score = Math.Max(int.Parse(match.Groups["score"].Value), this.Score);
                }
            }
        }
    }
}
