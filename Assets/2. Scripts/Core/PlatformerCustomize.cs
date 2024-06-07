using JetBrains.Annotations;
using System.Collections;
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
        [SerializeField] GameObject fireRun1;
        [SerializeField] GameObject fireRun2;
        [SerializeField] GameObject fireRun3;
        [SerializeField] Vector3 firePointRight;
        [SerializeField] Vector3 firePointLeft;
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

        [Header("Health")]
        public static int HealthPlayer = 30;
        public static int DamagePlayer = 10;

        bool toggle = false;
        Vector3 offset = new Vector3(0.4f, 0, 0);

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

            Invoke("OnRetry", 2f);
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
                    if (engine.velocity.sqrMagnitude > 0)
                    {
                        SimplePool.Spawn(fireRun1, transform.position + firePointRight, transform.rotation);
                    }
                    else
                    {
                        SimplePool.Spawn(fire1, transform.position + firePointRight, transform.rotation);
                    }
                }
                else
                {
                    if (engine.velocity.sqrMagnitude > 0)
                    {
                        SimplePool.Spawn(fireRun1, transform.position + firePointLeft, transform.rotation);
                    }
                    else
                    {
                        SimplePool.Spawn(fire1, transform.position + firePointLeft, transform.rotation);
                    }
                }
            }
            else if (Globals.TurnAttack == 1)
            {
                if (Globals.AttackDirection)
                {
                    if (engine.velocity.sqrMagnitude > 0)
                    {
                        SimplePool.Spawn(fireRun2, transform.position + firePointRight, transform.rotation);
                    }
                    else
                    {
                        SimplePool.Spawn(fire2, transform.position + firePointRight, transform.rotation);
                    }
                }
                else
                {
                    if (engine.velocity.sqrMagnitude > 0)
                    {
                        SimplePool.Spawn(fireRun2, transform.position + firePointLeft, transform.rotation);
                    }
                    else
                    {
                        SimplePool.Spawn(fire2, transform.position + firePointLeft, transform.rotation);
                    }
                }
            }
            else if (Globals.TurnAttack == 2)
            {
                if (Globals.AttackDirection)
                {
                    if (engine.velocity.sqrMagnitude > 0)
                    {
                        SimplePool.Spawn(fireRun3, transform.position + firePointRight, transform.rotation);
                    }
                    else
                    {
                        SimplePool.Spawn(fire3, transform.position + firePointRight, transform.rotation);
                    }
                }
                else
                {
                    if (engine.velocity.sqrMagnitude > 0)
                    {
                        SimplePool.Spawn(fireRun3, transform.position + firePointLeft, transform.rotation);
                    }
                    else
                    {
                        SimplePool.Spawn(fire3, transform.position + firePointLeft, transform.rotation);
                    }
                }
            }
        }

        public void Jump()
        {
            SimplePool.Spawn(jumpVfx1, transform.position, transform.rotation);
        }

        public void OnRetry()
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log(2222222222);
        }

        void OnJump()
        {
            if (!isSkipJumpSe)
            {
                audioSource.PlayOneShot(jumpClip);
            }
            isSkipJumpSe = false;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ToggleMovingPlatform"))
            {
                toggle = !toggle;

                if (toggle)
                {
                    engine.movingPlatformLayerMask = LayerMask.GetMask("Moving Platforms");
                }
                else
                {
                    engine.movingPlatformLayerMask = 0;
                }
            }

            if (collision.CompareTag("FireOfBoss"))
            {
                HealthPlayer -= DamagePlayer;
                Globals.HurtFlag = true;
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
            else if (collision.gameObject.CompareTag("Rock"))
            {
                if (Mathf.Abs(engine.velocity.x) > 0)
                {
                    Globals.PushFlag = true;
                    engine.groundSpeed = 0.0001f;
                }
                else
                {
                    engine.groundSpeed = groundSpeed;
                    Globals.PushFlag = false;
                    transform.position -= offset;
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Barrel") || collision.gameObject.CompareTag("Rock"))
            {
                Globals.PushFlag = false;
                engine.groundSpeed = groundSpeed;
            }
        }
    }
}