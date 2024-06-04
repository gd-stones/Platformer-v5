using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PlatformerCustomize : MonoBehaviour
    {
        public PlatformerEngine engine;
        public GameObject playerHitPrefab;

        public AudioClip jumpClip;
        public AudioClip hitClip;
        public bool isSkipJumpSe;
        public float pushSpeed = 0.5f;
        float groundSpeed;

        void Awake()
        {
            engine.onJump += OnJump;
            groundSpeed = engine.groundSpeed;
        }

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

        void OnRetry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void OnJump()
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

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("StartMP"))
            {
                engine.movingPlatformLayerMask = LayerMask.GetMask("Moving Platforms");
            }

            if (collision.CompareTag("EndMP"))
            {
                engine.movingPlatformLayerMask = 0;
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Barrel"))
            {
                if (Mathf.Abs(engine.velocity.x) > 0)
                {
                    Globals.PushFlag = true;
                    engine.groundSpeed = pushSpeed;
                }
                else
                {
                    engine.groundSpeed = groundSpeed;
                    Globals.PushFlag = false;
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Barrel"))
            {
                Globals.PushFlag = false;
                engine.groundSpeed = groundSpeed;
            }
        }
    }
}