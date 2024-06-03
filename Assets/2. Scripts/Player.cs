using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class Player : MonoBehaviour
    {
        public GameObject playerHitPrefab;
        public AudioClip jumpClip;
        public AudioClip hitClip;
        public bool isSkipJumpSe;

        public void Dead()
        {
            gameObject.SetActive(false);
            var cameraShake = FindObjectOfType<CameraShaker>();
            cameraShake.Shake();

            Invoke("OnRetry", 2);
            Instantiate(playerHitPrefab, transform.position, Quaternion.identity);

            var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(hitClip);
        }

        private void OnRetry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Awake()
        {
            var engine = GetComponent<PlatformerEngine>();
            engine.onJump += OnJump;
        }

        private void OnJump()
        {
            if (isSkipJumpSe)
            {
                isSkipJumpSe = false;
            }
            else
            {
                var audioSource = FindObjectOfType<AudioSource>();
                audioSource.PlayOneShot(jumpClip);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("eeeeeee");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("WWWWWWWWWWWW");
        }
    }
}