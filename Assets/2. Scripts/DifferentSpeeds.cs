using UnityEngine;

namespace StonesGaming
{
    public class DifferentSpeeds : MonoBehaviour
    {
        public KeyCode keyCodeToStart;
        public float distanceToGo;

        private bool _hasStarted;
        private Vector3 _startPosition;
        private PlatformerEngine _engine;

        void Start()
        {
            _engine = GetComponent<PlatformerEngine>();
        }

        void Update()
        {
            if (!_hasStarted && UnityEngine.Input.GetKeyDown(keyCodeToStart))
            {
                _hasStarted = true;
                _startPosition = transform.position;
                _engine.normalizedXMovement = 1f;
            }

            if (_hasStarted)
            {
                if (Vector3.Distance(transform.position, _startPosition) >= distanceToGo)
                {
                    _engine.normalizedXMovement *= -1;
                    _startPosition = transform.position;
                }
            }
        }
    }
}