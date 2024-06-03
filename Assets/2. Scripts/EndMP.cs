using UnityEngine;

namespace StonesGaming
{
    public class EndMP : MonoBehaviour
    {
        public PlatformerEngine engine;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                engine.movingPlatformLayerMask = 0;
            }
        }
    }
}
