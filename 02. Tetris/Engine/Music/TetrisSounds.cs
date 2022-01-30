namespace Tetris.Engine.Music
{
    using System;
    using System.Media;

    public class TetrisSounds : IDisposable
    {
        private const string TetrisThemeMusicPath = "./Assets/Tetris.wav";
        private const string TetrisFallPath = "./Assets/fall.wav";
        private const string TetrisPath = "./Assets/clear.wav";
        private const string TetrisClearPath = "./Assets/line.wav";

        private readonly SoundPlayer musicPlayer;
        private bool disposedValue;

        public TetrisSounds()
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new InvalidOperationException("OS is not supported.");
            }

            this.musicPlayer = new SoundPlayer(TetrisThemeMusicPath);
        }

        public void PlayMusic() => this.StartMusic();

        public void StopMusic()
        {
            if (!OperatingSystem.IsWindows())
            {
                return;
            }

            this.musicPlayer.Stop();
        }

        public void PlayFall() => this.Play(TetrisFallPath);

        public void PlayTetris() => this.Play(TetrisPath);

        public void PlayClear() => this.Play(TetrisClearPath);

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.musicPlayer.Dispose();
                }

                this.disposedValue = true;
            }
        }

        private void StartMusic()
        {
            if (!OperatingSystem.IsWindows())
            {
                return;
            }

            this.musicPlayer.PlayLooping();
        }

        private void Play(string path)
        {
            if (!OperatingSystem.IsWindows())
            {
                return;
            }

            var player = new SoundPlayer(path);

            player.Play();
        }
    }
}
