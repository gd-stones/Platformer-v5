using UnityEngine;

namespace StonesGaming
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                animator.Play("Flag Out");
                Globals.Checkpoint = transform.position;
            }
        }
    }
}