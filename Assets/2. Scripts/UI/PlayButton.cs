using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PlayButton : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(Globals.LevelName);
        }
    }
}