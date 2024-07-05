using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;  
        }

        public void ReturnHome()
        {
            SceneManager.LoadScene("Home");
            Globals.Score = 0;
        }

        public void Resume()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

        public void ReplayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Globals.Score = 0;
            Time.timeScale = 1f;
        }
    }
}
