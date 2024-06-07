using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace StonesGaming
{
    public class BossAI : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] GameObject visual;

        [SerializeField] float health = 100;
        [SerializeField] float damage = 10;
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float stopDistance = 1f;
        [SerializeField] BoxCollider2D boxCollider;

        [SerializeField] GameObject fire1;
        [SerializeField] GameObject fire2;
        [SerializeField] Vector3 firePointLeft;
        [SerializeField] Vector3 firePointRight;

        Animator animator;
        bool isHurt;
        bool canMove = true;
        bool isDeath = false;

        void Start()
        {
            animator = visual.GetComponent<Animator>();
        }

        void Update()
        {
            if (health <= 0)
            {
                if (!isDeath)
                {
                    StopAllCoroutines();
                    StartCoroutine(Death());
                }
                return;
            }

            if (isHurt && health > 0)
            {
                isHurt = false;
                canMove = false;

                StartCoroutine(Hurt());
                health -= damage;
                return;
            }

            if (Globals.IsObjectInCameraView(boxCollider) && canMove)
            {
                MoveTowardsPlayerAndAttack();
                FlipTowardsPlayer();
            }
        }

        IEnumerator Hurt()
        {
            animator.Play("Hurt");

            yield return new WaitForSeconds(1f);
            animator.Play("Idle");

            yield return new WaitForSeconds(0.5f);
            canMove = true;
        }

        IEnumerator Death()
        {
            isDeath = true;
            animator.Play("Death");

            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }

        void FlipTowardsPlayer()
        {
            if (player.position.x < transform.position.x && visual.transform.localScale.x > 0)
            {
                Flip();
            }
            else if (player.position.x > transform.position.x && visual.transform.localScale.x < 0)
            {
                Flip();
            }
        }

        public static Vector3 theScale;
        void Flip()
        {
            theScale = visual.transform.localScale;

            theScale.x *= -1;
            visual.transform.localScale = theScale;
        }


        bool isAttack = true;
        void MoveTowardsPlayerAndAttack()
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

            if (distanceToPlayer >= stopDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.position += new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;
                animator.Play("Walk");
            }
            else if (distanceToPlayer < stopDistance && isAttack)
            {
                StartCoroutine(Attack());
            }
        }

        int turn = -1;
        IEnumerator Attack()
        {
            isAttack = false;
            animator.Play("Attack");

            turn++;
            turn %= 2;
            if (turn == 0)
            {
                if (theScale.x == 1)
                {
                    SimplePool.Spawn(fire1, transform.position + firePointRight, transform.rotation);
                }
                else
                {
                    SimplePool.Spawn(fire1, transform.position + firePointLeft, transform.rotation);
                }
            }
            else if (turn == 1)
            {
                if (theScale.x == 1)
                {
                    SimplePool.Spawn(fire2, transform.position + firePointRight, transform.rotation);
                }
                else
                {
                    SimplePool.Spawn(fire2, transform.position + firePointLeft, transform.rotation);
                }
            }

            yield return new WaitForSeconds(1f);
            isAttack = true;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Fire"))
            {
                isHurt = true;
            }
        }
    }
}
