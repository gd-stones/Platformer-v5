using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PlayButton : MonoBehaviour
    {
        public void PlayGame()
        {
            Time.timeScale = 1.0f;
            Globals.Score = 0;
            SceneManager.LoadScene(Globals.LevelName);
        }
    }
}