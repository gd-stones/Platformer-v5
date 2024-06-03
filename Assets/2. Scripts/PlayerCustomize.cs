using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PlayerCustomize : MonoBehaviour
    {
        public PlatformerEngine platformerEngine;
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
            platformerEngine.onJump += OnJump;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("StartMP"))
            {
                platformerEngine.movingPlatformLayerMask = LayerMask.GetMask("Moving Platforms");
            }

            if(collision.CompareTag("EndMP"))
            {
                platformerEngine.movingPlatformLayerMask = 0;
            }
        }
    }
}