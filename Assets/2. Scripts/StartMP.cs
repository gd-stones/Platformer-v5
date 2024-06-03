using UnityEngine;

namespace StonesGaming
{
    public class StartMP : MonoBehaviour
    {
        public PlatformerEngine engine;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                engine.movingPlatformLayerMask = LayerMask.GetMask("Moving Platforms");
            }
        }
    }
}
