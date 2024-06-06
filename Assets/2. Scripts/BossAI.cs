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

        Animator animator;
        bool isHurt;

        void Start()
        {
            animator = visual.GetComponent<Animator>();
        }

        bool canMove = true;
        void Update()
        {
            if (health <= 0)
            {
                animator.Play("Death");
                gameObject.SetActive(false);
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
                MoveTowardsPlayer();
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

        void Flip()
        {
            Vector3 theScale = visual.transform.localScale;
            theScale.x *= -1;
            visual.transform.localScale = theScale;
        }

        void MoveTowardsPlayer()
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

            if (distanceToPlayer >= stopDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.position += new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;
                animator.Play("Walk");
            }
            else
            {
                animator.Play("Attack");
            }
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
