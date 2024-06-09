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
        }

        public void Resume()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

        public void ReplayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
    }
}
