using UnityEngine;

namespace StonesGaming
{
    public class Rock : MonoBehaviour
    {
        [SerializeField] float health = 20;
        [SerializeField] float damage = 10;
        [SerializeField] Animator animator;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Fire"))
            {
                health -= damage;

                if (health > 0)
                {
                    animator.Play("Hit");
                }
                else if (health <= 0)
                {
                    animator.Play("Break");
                }
            }
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}