using UnityEngine;

namespace StonesGaming
{
    public class SimpleLeftRight : MonoBehaviour
    {
        public float leftRightAmount;
        public float speed;

        private MovingPlatformEngine _mpEngine;
        private float _startingX;

        void Start()
        {
            _mpEngine = GetComponent<MovingPlatformEngine>();
            _startingX = transform.position.x;
            _mpEngine.velocity = -Vector2.right * speed;
        }

        void FixedUpdate()
        {
            if (_mpEngine.velocity.x < 0 && _startingX - transform.position.x >= leftRightAmount)
            {
                transform.position += Vector3.right * ((_startingX - transform.position.x) - leftRightAmount);
                _mpEngine.velocity = Vector2.right * speed;
            }
            else if (_mpEngine.velocity.x > 0 && transform.position.x - _startingX >= leftRightAmount)
            {
                transform.position += -Vector3.right * ((transform.position.x - _startingX) - leftRightAmount);
                _mpEngine.velocity = -Vector2.right * speed;
            }
        }
    }
}
