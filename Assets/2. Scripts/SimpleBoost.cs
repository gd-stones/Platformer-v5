using UnityEngine;

namespace StonesGaming
{
    public class SimpleBoost : MonoBehaviour
    {
        public float heightReached;
        public float moveUpDuration;
        public EasingFunctions.Functions moveUpEase;
        public float moveDownDuration;
        public EasingFunctions.Functions moveDownEase;
        public float playerSpeedYAtApex;

        private enum State
        {
            None,
            Up,
            Down
        }

        private MovingPlatformEngine _mpEngine;
        private State _state;
        private float _originalY;
        private float _time;
        private PlatformerEngine _player;

        private EasingFunctions.EasingFunc _moveUpFunc;
        private EasingFunctions.EasingFunc _moveDownFunc;

        void Start()
        {
            _mpEngine = GetComponent<MovingPlatformEngine>();
            _mpEngine.onPlatformerEngineContact += PlayerContact;
            _originalY = transform.position.y;

            _moveUpFunc = EasingFunctions.GetEasingFunction(moveUpEase);
            _moveDownFunc = EasingFunctions.GetEasingFunction(moveDownEase);
        }

        private void FixedUpdate()
        {
            if (_state == State.Down)
            {
                _time += Time.fixedDeltaTime;

                _mpEngine.position = new Vector3(
                    _mpEngine.position.x,
                    _moveDownFunc(_originalY + heightReached, _originalY, Mathf.Clamp01(_time / moveDownDuration)),
                    transform.position.z);

                if (_time >= moveDownDuration)
                {
                    _state = State.None;

                    if (_player != null && _player.connectedPlatform == _mpEngine)
                    {
                        _state = State.Up;
                        _time = 0;
                    }
                    else
                    {
                        _player = null;
                    }
                }
            }

            if (_state == State.Up)
            {
                _time += Time.fixedDeltaTime;

                _mpEngine.position = new Vector3(
                    _mpEngine.position.x,
                    _moveUpFunc(_originalY, _originalY + heightReached, Mathf.Clamp01(_time / moveUpDuration)),
                    transform.position.z);

                if (_time >= moveUpDuration)
                {
                    _state = State.Down;
                    _time = 0;

                    if (_player.connectedPlatform == _mpEngine)
                    {
                        _player.DisconnectFromPlatform();
                        _player.velocity += Vector2.up * playerSpeedYAtApex;
                        _player = null;
                    }
                }
            }
        }

        private void PlayerContact(PlatformerEngine player)
        {
            if (_state == State.None)
            {
                _state = State.Up;
                _time = 0;

            }

            _player = player;
        }
    }
}
