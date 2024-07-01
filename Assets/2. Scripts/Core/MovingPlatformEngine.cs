using UnityEngine;

namespace StonesGaming
{
    public class MovingPlatformEngine : MonoBehaviour
    {
        public Vector2 velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocitySet = true;
                _velocity = value;
            }
        }
        public System.Action<PlatformerEngine> onPlatformerEngineContact;

        private bool _velocitySet;
        private Vector2 _velocity;

        public Vector2 position
        {
            get { return transform.position; }
            set
            {
                prevPosition = transform.position;
                transform.position = value;
                velocity = Vector2.zero;
                _velocitySet = false;
            }
        }

        public Vector2 prevPosition { get; private set; }

        private void FixedUpdate()
        {
            if (_velocitySet)
            {
                prevPosition = transform.position;
                transform.position += (Vector3)velocity * Time.fixedDeltaTime;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }
        }
    }
}