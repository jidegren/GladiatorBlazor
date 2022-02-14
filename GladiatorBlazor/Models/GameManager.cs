namespace GladiatorBlazor.Models
{
    public class GameManager
    {
        public bool IsRunning { get; set; } = false;
        public async void MainLoop()
        {
            IsRunning = true;

            while (IsRunning)
            {

            }
        }

        public void StartGame()
        {
            if (IsRunning)
            {
                MainLoop();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }

    }
}
