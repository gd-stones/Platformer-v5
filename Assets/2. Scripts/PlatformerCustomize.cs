using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StonesGaming
{
    public class PlatformerCustomize : MonoBehaviour
    {
        [SerializeField] PlatformerEngine engine;
        [SerializeField] AudioSource audioSource;

        [Header("Attack")]
        [SerializeField] GameObject fire1;
        [SerializeField] GameObject fire2;
        [SerializeField] GameObject fire3;
        [SerializeField] Vector3 firePointRight;
        [SerializeField] Vector3 firePointLeft;
        int turn = -1;
        [SerializeField] AudioClip attackClip;

        [Header("Push")]
        [SerializeField] float pushSpeed = 0.5f;
        float groundSpeed;

        [Header("Dead")]
        [SerializeField] GameObject playerHitPrefab;
        [SerializeField] AudioClip hitClip;

        [Header("Jump")]
        [SerializeField] GameObject jumpVfx1;
        [SerializeField] GameObject jumpVfx2;
        [SerializeField] AudioClip jumpClip;
        public bool isSkipJumpSe;

        public static int Health = 2;

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

            audioSource.PlayOneShot(hitClip);
        }

        public void Attack()
        {
            Globals.TurnAttack++;
            Globals.TurnAttack = Globals.TurnAttack % 3;
            audioSource.PlayOneShot(attackClip);

            if (Globals.TurnAttack == 0)
            {
                if (Globals.AttackDirection)
                {
                    SimplePool.Spawn(fire1, transform.position + firePointRight, transform.rotation);
                }
                else
                {
                    SimplePool.Spawn(fire1, transform.position + firePointLeft, transform.rotation);
                }
            }
            else if (Globals.TurnAttack == 1)
            {
                if (Globals.AttackDirection)
                {
                    SimplePool.Spawn(fire2, transform.position + firePointRight, transform.rotation);
                }
                else
                {
                    SimplePool.Spawn(fire2, transform.position + firePointLeft, transform.rotation);
                }
            }
            else if (Globals.TurnAttack == 2)
            {
                if (Globals.AttackDirection)
                {
                    SimplePool.Spawn(fire3, transform.position + firePointRight, transform.rotation);
                }
                else
                {
                    SimplePool.Spawn(fire3, transform.position + firePointLeft, transform.rotation);
                }
            }
        }

        public void Jump()
        {
            SimplePool.Spawn(jumpVfx1, transform.position, transform.rotation);
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