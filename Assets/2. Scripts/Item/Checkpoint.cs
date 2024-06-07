using TMPro;
using UnityEngine;

namespace StonesGaming
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] Animator animator;
        bool runAnim = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !runAnim)
            {
                runAnim = true;
                animator.Play("Flag Out");
                Globals.Checkpoint = transform.position;
            }
        }
    }
}