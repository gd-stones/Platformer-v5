using UnityEngine;

namespace StonesGaming
{
    public class SimpleCollapsable : MonoBehaviour
    {
        public float gravityScaleWhenFalling;
        public float darkenDuration;
        public Color darkenColor;

        private float _darkenTime;
        private Color _originalColor;

        private enum State
        {
            None,
            Darken,
            Falling
        }

        private MovingPlatformEngine _mpEngine;
        private SpriteRenderer _renderer;
        private State _state;

        void Start()
        {
            _mpEngine = GetComponent<MovingPlatformEngine>();
            _mpEngine.onPlatformerEngineContact += PlayerContact;

            _renderer = GetComponent<SpriteRenderer>();
            _originalColor = _renderer.color;
        }

        private void FixedUpdate()
        {
            if (_state == State.Falling)
            {
                _mpEngine.velocity += Physics2D.gravity * gravityScaleWhenFalling * Time.fixedDeltaTime;
            }
            
            if (_state == State.Darken)
            {
                _darkenTime += Time.fixedDeltaTime;
                _renderer.color = Color.Lerp(_originalColor, darkenColor, Mathf.Clamp01(_darkenTime / darkenDuration));
            }

            if (_darkenTime >= darkenDuration)
            {
                _state = State.Falling;
            }
        }

        private void PlayerContact(PlatformerEngine player)
        {
            _mpEngine.onPlatformerEngineContact -= PlayerContact;
            _state = State.Darken;
        }
    }
}
