using UnityEngine;

namespace StonesGaming
{
    public class SimpleUpDown : MonoBehaviour
    {
        public float upDownAmount;
        public float speed;
        private MovingPlatformEngine _mpEngine;
        private float _startingY;

        void Start()
        {
            _mpEngine = GetComponent<MovingPlatformEngine>();
            _startingY = transform.position.y;
            _mpEngine.velocity = Vector2.up * speed;
        }

        void FixedUpdate()
        {
            if (_mpEngine.velocity.y < 0 && _startingY - _mpEngine.position.y >= upDownAmount)
            {
                _mpEngine.position += Vector2.up * ((_startingY - _mpEngine.position.y) - upDownAmount);
                _mpEngine.velocity = Vector2.up * speed;
            }
            else if (_mpEngine.velocity.y > 0 && _mpEngine.position.y - _startingY >= upDownAmount)
            {
                _mpEngine.position += -Vector2.up * ((_mpEngine.position.y - _startingY) - upDownAmount);
                _mpEngine.velocity = -Vector2.up * speed;
            }
        }
    }
}
