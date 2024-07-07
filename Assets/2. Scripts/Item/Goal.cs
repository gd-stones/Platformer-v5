using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Goal : MonoBehaviour
{
    [SerializeField] int _nextLevel;
    [SerializeField] AudioClip _goalClip;
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
                audioSource.PlayOneShot(_goalClip);

                int level = PlayerPrefs.GetInt("LevelUnlocked");
                if (level <= _nextLevel - 1)
                {
                    PlayerPrefs.SetInt("LevelUnlocked", _nextLevel);
                }

                StartCoroutine(LoadHomeScene());
                AdsManager.Instance.ShowInterstitial(None);
            }
        }
    }

    void None()
    {
    }

    IEnumerator LoadHomeScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Home");
    }
}

