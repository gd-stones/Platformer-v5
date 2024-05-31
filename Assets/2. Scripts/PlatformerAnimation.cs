using UnityEngine;

namespace StonesGaming
{
    /// <summary>
    /// This is a very very very simple example of how an animation system could query information from the _engine to set state.
    /// This can be done to explicitly play states, as is below, or send triggers, float, or bools to the animator. Most likely this
    /// will need to be written to suit your game's needs.
    /// </summary>

    public class PlatformerAnimation : MonoBehaviour
    {
        public float jumpRotationSpeed;
        public GameObject visualChild;

        private PlatformerEngine _engine;
        private Animator _animator;
        private bool _isJumping;
        private bool _currentFacingLeft;

        void Start()
        {
            _engine = GetComponent<PlatformerEngine>();
            _animator = visualChild.GetComponent<Animator>();
            _animator.Play("Idle");

            _engine.onJump += SetCurrentFacingLeft;
        }

        void Update()
        {
            if (_engine.engineState == PlatformerEngine.EngineState.Jumping ||
                _isJumping &&
                    (_engine.engineState == PlatformerEngine.EngineState.Falling ||
                                 _engine.engineState == PlatformerEngine.EngineState.FallingFast))
            {
                _isJumping = true;
                _animator.Play("Jump");

                if (_engine.velocity.x <= -0.1f)
                {
                    _currentFacingLeft = true;
                }
                else if (_engine.velocity.x >= 0.1f)
                {
                    _currentFacingLeft = false;
                }

                Vector3 rotateDir = _currentFacingLeft ? Vector3.forward : Vector3.back;
                visualChild.transform.Rotate(rotateDir, jumpRotationSpeed * Time.deltaTime);
            }
            else
            {
                _isJumping = false;
                visualChild.transform.rotation = Quaternion.identity;

                if (_engine.engineState == PlatformerEngine.EngineState.Falling ||
                                 _engine.engineState == PlatformerEngine.EngineState.FallingFast)
                {
                    _animator.Play("Fall");
                }
                else if (_engine.engineState == PlatformerEngine.EngineState.WallSliding ||
                         _engine.engineState == PlatformerEngine.EngineState.WallSticking)
                {
                    _animator.Play("Cling");
                }
                else if (_engine.engineState == PlatformerEngine.EngineState.OnCorner)
                {
                    _animator.Play("On Corner");
                }
                else if (_engine.engineState == PlatformerEngine.EngineState.Slipping)
                {
                    _animator.Play("Slip");
                }
                else if (_engine.engineState == PlatformerEngine.EngineState.Dashing)
                {
                    _animator.Play("Dash");
                }
                else
                {
                    if (_engine.velocity.sqrMagnitude >= 0.1f * 0.1f)
                    {
                        _animator.Play("Walk");
                    }
                    else
                    {
                        _animator.Play("Idle");
                    }
                }
            }

            // Facing
            float valueCheck = _engine.normalizedXMovement;

            if (_engine.engineState == PlatformerEngine.EngineState.Slipping ||
                _engine.engineState == PlatformerEngine.EngineState.Dashing ||
                _engine.engineState == PlatformerEngine.EngineState.Jumping)
            {
                valueCheck = _engine.velocity.x;
            }
            
            if (Mathf.Abs(valueCheck) >= 0.1f)
            {
                Vector3 newScale = visualChild.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
                visualChild.transform.localScale = newScale;
            }
        }

        private void SetCurrentFacingLeft()
        {
            _currentFacingLeft = _engine.facingLeft;
        }
    }
}
