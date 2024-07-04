using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PlayButton : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void PlayGame()
        {
            Time.timeScale = 1.0f;
            Globals.Score = 0;
            SceneManager.LoadScene(Globals.LevelName);
        }
    }
}