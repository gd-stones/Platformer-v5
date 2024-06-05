using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace StonesGaming
{
    public class BossAI : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] Camera mainCamera;
        [SerializeField] GameObject visual;

        [SerializeField] float health = 100;
        [SerializeField] float damage = 10;
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float stopDistance = 1f;
        [SerializeField] BoxCollider2D boxCollider;

        Vector3 velocity = Vector3.zero;
        Animator animator;
        bool isHurt;

        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
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

            if (IsObjectInCameraView() && canMove)
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

        bool IsObjectInCameraView()
        {
            if (boxCollider == null)
            {
                Debug.LogWarning("No BoxCollider2D found on " + gameObject.name);
                return false;
            }

            Vector3[] corners = new Vector3[4];
            corners[0] = boxCollider.bounds.min; // Bottom-left
            corners[1] = new Vector3(boxCollider.bounds.min.x, boxCollider.bounds.max.y, boxCollider.bounds.min.z); // Top-left
            corners[2] = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y, boxCollider.bounds.min.z); // Bottom-right
            corners[3] = boxCollider.bounds.max; // Top-right

            foreach (var corner in corners)
            {
                Vector3 screenPoint = mainCamera.WorldToViewportPoint(corner);

                if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
                {
                    return true;
                }
            }

            return false;
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
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer >= stopDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
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
