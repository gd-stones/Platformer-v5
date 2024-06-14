using UnityEngine;
using System.Collections;

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

        PlatformerEngine _engine;
        PlatformerCustomize _engineCustomize;
        Animator _animator;
        bool _isJumping;
        bool _currentFacingLeft;

        bool _isSlip;
        bool _isDash;
        [SerializeField] GameObject _dashEffect;
        [SerializeField] GameObject _slipEffect;

        void Start()
        {
            _engine = GetComponent<PlatformerEngine>();
            _engineCustomize = GetComponent<PlatformerCustomize>();

            _animator = visualChild.GetComponent<Animator>();
            _animator.Play("Idle");

            _engine.onJump += SetCurrentFacingLeft;
        }



        void Update()
        {
            if (_engine.IsJumping() || _isJumping && (_engine.IsFalling() || _engine.IsFallingFast()))
            {
                _isJumping = true;

                if (Globals.HighJumpFlag)
                {
                    _animator.Play("High Jump");
                }
                else
                {
                    _animator.Play("Jump");
                }

                if (_engine.velocity.x <= -0.1f)
                {
                    _currentFacingLeft = true;
                }
                else if (_engine.velocity.x >= 0.1f)
                {
                    _currentFacingLeft = false;
                }

                Vector3 rotateDirection = _currentFacingLeft ? Vector3.forward : Vector3.back;
                visualChild.transform.Rotate(rotateDirection, jumpRotationSpeed * Time.deltaTime);
            }
            else
            {
                _isJumping = false;
                visualChild.transform.rotation = Quaternion.identity;

                if (_engine.IsFalling() || _engine.IsFallingFast())
                {
                    _animator.Play("Fall");
                }
                else if (_engine.IsWallSliding() || _engine.IsWallSticking())
                {
                    _animator.Play("Cling");
                }
                else if (_engine.IsOnCorner())
                {
                    _animator.Play("On Corner");
                }
                else if (_engine.IsSlipping())
                {
                    _animator.Play("Slip");

                    if (!_isSlip)
                    {
                        _slipEffect.SetActive(true);
                        _isSlip = true;
                    }
                }
                else if (_engine.IsDashing())
                {
                    _animator.Play("Dash");

                    if (!_isDash)
                    {
                        _dashEffect.SetActive(true);
                        _isDash = true;
                    }
                }
                else if (_engine.IsOnLadder() && Globals.LadderFlag)
                {
                    if (UnityEngine.Input.GetAxis(StonesGaming.Input.VERTICAL) == 0)
                    {
                        _animator.Play("Standing on Ladder");
                    }
                    else
                    {
                        _animator.Play("Climb");
                    }
                }
                else if (Globals.PushFlag)
                {
                    _animator.Play("Push");
                }
                else if (Globals.TeleportFlag)
                {
                    if (Globals.PortalIn)
                    {
                        _animator.Play("Portal In");
                    }
                    else
                    {
                        _animator.Play("Portal Out");
                    }
                }
                else if (Globals.AttackFlag)
                {
                    if (_engineCustomize.turnAttack == 2)
                    {
                        _animator.Play("Attack Extra");
                    }
                    else if (_engine.velocity.sqrMagnitude >= 4f)
                    {
                        _animator.Play("Run Attack");
                    }
                    else if (_engine.velocity.sqrMagnitude >= 0.1f * 0.1f)
                    {
                        _animator.Play("Walk Attack");
                    }
                    else
                    {
                        _animator.Play("Attack");
                    }
                }
                else if (Globals.HurtFlag)
                {
                    if (!_engineCustomize.IsDead())
                    {
                        _animator.Play("Hurt");
                    }
                    else
                    {
                        _animator.Play("Death");
                        _engineCustomize.Revival();
                    }
                }
                else
                {
                    if (_engine.velocity.sqrMagnitude >= 4f)
                    {
                        _animator.Play("Run");
                    }
                    else if (_engine.velocity.sqrMagnitude >= 0.1f * 0.1f)
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

            if (_engine.IsSlipping() || _engine.IsDashing() || _engine.IsJumping())
            {
                valueCheck = _engine.velocity.x;
            }

            if (Mathf.Abs(valueCheck) >= 0.1f)
            {
                Vector3 newScale = visualChild.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
                visualChild.transform.localScale = newScale;
            }

            if (!_engine.IsDashing())
            {
                _dashEffect.SetActive(false);
                _isDash = false;
            }

            if (!_engine.IsSlipping())
            {
                _slipEffect.SetActive(false);
                _isSlip = false;
            }
        }

        void SetCurrentFacingLeft()
        {
            _currentFacingLeft = _engine.facingLeft;
        }
    }
}
