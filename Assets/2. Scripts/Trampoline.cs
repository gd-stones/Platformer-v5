using UnityEngine;

namespace StonesGaming
{
    public class Trampoline : MonoBehaviour
    {
        public float jumpHeight = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.Contains("Player"))
            {
                var engine = other.GetComponent<PlatformerEngine>();
                engine.ForceJump(jumpHeight);

                var animator = GetComponent<Animator>();
                animator.Play("Jump");
            }
        }
    }
}