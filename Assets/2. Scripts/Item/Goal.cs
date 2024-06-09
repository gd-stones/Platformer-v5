using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] int nextLevel;
        [SerializeField] AudioClip goalClip;
        bool _isGoal;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isGoal)
            {
                if (other.name.Contains("Player") || other.CompareTag("Player"))
                {
                    var cameraShake = FindObjectOfType<CameraShaker>();
                    cameraShake.Shake();

                    _isGoal = true;
                    var animator = GetComponent<Animator>();
                    animator.Play("Pressed");

                    var audioSource = FindObjectOfType<AudioSource>();
                    audioSource.PlayOneShot(goalClip);

                    int level = PlayerPrefs.GetInt("LevelUnlocked");
                    if (level < nextLevel - 1)
                    {
                        PlayerPrefs.SetInt("LevelUnlocked", nextLevel);
                    }

                    SceneManager.LoadScene("Home");
                }
            }
        }
    }
}