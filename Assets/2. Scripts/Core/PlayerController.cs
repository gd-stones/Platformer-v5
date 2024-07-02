using UnityEngine;

namespace StonesGaming
{
    /// <summary>
    /// This class is a simple example of how to build a controller that interacts with PlatformerEngine.
    /// </summary>
    [RequireComponent(typeof(PlatformerEngine))]
    public class PlayerController : MonoBehaviour
    {
        private PlatformerEngine _engine;
        private bool _restored = true;
        private bool _enableOneWayPlatforms;
        private bool _oneWayPlatformsAreWalls;

        [SerializeField] PlatformerCustomize _engineCustomize;

        [Header("Mobile Control")]
        [SerializeField] bool isControlOnMobile = true;
        [SerializeField] Joystick _leftJoystick;
        [SerializeField] RightTouchControl _rightTouchControl; // Controls attack or jump actions
        [SerializeField] DashCooldown _dashCooldown;

        void Start()
        {
            _engine = GetComponent<PlatformerEngine>();
        }

        // Before enter en freedom state for ladders
        void FreedomStateSave(PlatformerEngine engine)
        {
            if (!_restored) // do not enter twice
                return;

            _restored = false;
            _enableOneWayPlatforms = engine.enableOneWayPlatforms;
            _oneWayPlatformsAreWalls = engine.oneWayPlatformsAreWalls;
        }

        // After leave freedom state for ladders
        void FreedomStateRestore(PlatformerEngine engine)
        {
            if (_restored) // do not enter twice
                return;

            _restored = true;
            engine.enableOneWayPlatforms = _enableOneWayPlatforms;
            engine.oneWayPlatformsAreWalls = _oneWayPlatformsAreWalls;
        }

        void Update()
        {
            // use last state to restore some ladder specific values
            if (_engine.engineState != PlatformerEngine.EngineState.FreedomState)
            {
                // try to restore, sometimes states are a bit messy because change too much in one frame
                FreedomStateRestore(_engine);
            }

            // Jump?
            // If you want to jump in ladders, leave it here, otherwise move it down
            if (UnityEngine.Input.GetButtonDown(StonesGaming.Input.JUMP) || _rightTouchControl.jump)
            {
                _rightTouchControl.jump = false;

                _engine.Jump();
                _engine.DisableRestrictedArea();

                if (!_engine.IsInAir())
                {
                    _engineCustomize.Jump();
                }
            }

            _engine.jumpingHeld = UnityEngine.Input.GetButton(StonesGaming.Input.JUMP);

            if (isControlOnMobile)
            {
                _engine.jumpingHeld = _rightTouchControl.isInside;
            }

            // XY freedom movement
            if (_engine.engineState == PlatformerEngine.EngineState.FreedomState)
            {
                _engine.normalizedXMovement = UnityEngine.Input.GetAxis(StonesGaming.Input.HORIZONTAL);
                _engine.normalizedYMovement = UnityEngine.Input.GetAxis(StonesGaming.Input.VERTICAL);

                if (isControlOnMobile)
                {
                    _engine.normalizedXMovement = _leftJoystick.Horizontal;
                    _engine.normalizedYMovement = _leftJoystick.Vertical;
                }

                return; // do nothing more
            }

            // X axis movement
            if (Mathf.Abs(UnityEngine.Input.GetAxis(StonesGaming.Input.HORIZONTAL)) > StonesGaming.Globals.INPUT_THRESHOLD ||
                Mathf.Abs(_leftJoystick.Horizontal) > StonesGaming.Globals.INPUT_THRESHOLD)
            {
                _engine.normalizedXMovement = UnityEngine.Input.GetAxis(StonesGaming.Input.HORIZONTAL);

                if (isControlOnMobile)
                {
                    _engine.normalizedXMovement = _leftJoystick.Horizontal;
                }
            }
            else
            {
                _engine.normalizedXMovement = 0;
            }

            if (UnityEngine.Input.GetAxis(StonesGaming.Input.VERTICAL) != 0 ||
                _leftJoystick.Vertical != 0)
            {
                bool up_pressed;
                up_pressed = UnityEngine.Input.GetAxis(StonesGaming.Input.VERTICAL) > 0;

                if (isControlOnMobile)
                {
                    up_pressed = _leftJoystick.Vertical > 0;
                }

                if (_engine.IsOnLadder())
                {
                    if ((up_pressed && _engine.ladderZone == PlatformerEngine.LadderZone.Top) ||
                        (!up_pressed && _engine.ladderZone == PlatformerEngine.LadderZone.Bottom))
                    {
                        // do nothing!
                        _engineCustomize.ResetStateEngineCustomize();
                    }
                    // if player hit up, while on the top do not enter in freeMode or a nasty short jump occurs
                    else
                    {
                        _engineCustomize.SetStateEngineCustomize(PlatformerCustomize.EngineCState.OnLadder);

                        _engine.FreedomStateEnter(); // enter freedomState to disable gravity
                        _engine.EnableRestrictedArea();  // movements is retricted to a specific sprite bounds

                        // now disable OWP completely in a "trasactional way"
                        FreedomStateSave(_engine);
                        _engine.enableOneWayPlatforms = false;
                        _engine.oneWayPlatformsAreWalls = false;

                        // start XY movement
                        _engine.normalizedXMovement = UnityEngine.Input.GetAxis(StonesGaming.Input.HORIZONTAL);
                        _engine.normalizedYMovement = UnityEngine.Input.GetAxis(StonesGaming.Input.VERTICAL);

                        if (isControlOnMobile)
                        {
                            _engine.normalizedXMovement = _leftJoystick.Horizontal;
                            _engine.normalizedYMovement = _leftJoystick.Vertical;
                        }
                    }
                }
            }
            else if (UnityEngine.Input.GetAxis(StonesGaming.Input.VERTICAL) < -StonesGaming.Globals.FAST_FALL_THRESHOLD ||
                    _leftJoystick.Vertical < -StonesGaming.Globals.FAST_FALL_THRESHOLD)
            {
                _engine.fallFast = false;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.E) || _dashCooldown.dash)
            {
                _engine.Dash();
                _dashCooldown.dash = false;
            }

            Globals.AttackDirection = !_engine.facingLeft;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q) || _rightTouchControl.attack)
            {
                _rightTouchControl.attack = false;

                _engineCustomize.Attack();
            }
        }
    }
}