using System.Collections;
using UnityEngine;

namespace  StonesGaming
{
    public class SimpleAI : MonoBehaviour
    {
        public float distanceCheckForJump;
        public float heightToFallFast;
        public float delayForWallJump;

        private PlatformerEngine _engine;
        public float movement { get; private set; }

        void Start()
        {
            _engine = GetComponent<PlatformerEngine>();
            movement = -1;

            // Find objects generally pretty bad but this is a demo :)
            SimpleAI[] ais = FindObjectsOfType<SimpleAI>();

            for (int i = 0; i < ais.Length; i++)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ais[i].GetComponent<Collider2D>());
            }

            _engine.onWallJump +=
                direction =>
                {
                    // Since the _engine needs to be pressing into the wall to wall jump, we switch direction after the jump.
                    movement = Mathf.Sign(direction.x);
                };
        }

        void FixedUpdate()
        {
            _engine.normalizedXMovement = movement;

            if (_engine.engineState == PlatformerEngine.EngineState.OnGround)
            {
                _engine.fallFast = false;
                Vector2 dir = Vector2.right;

                if (movement < 0)
                {
                    dir *= -1;
                }

                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    dir,
                    distanceCheckForJump,
                    Globals.ENV_MASK);

                if (hit.collider != null)
                {
                    _engine.Jump();
                }
            }

            if (_engine.engineState == PlatformerEngine.EngineState.WallSticking)
            {
                StartCoroutine(DelayWallJump());
            }

            if (_engine.engineState == PlatformerEngine.EngineState.Falling)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    -Vector2.up,
                    heightToFallFast,
                    Globals.ENV_MASK);

                Vector2 dir = Vector2.right;

                if (movement < 0)
                {
                    dir *= -1;
                }

                RaycastHit2D hit2 = Physics2D.Raycast(
                    transform.position,
                    dir,
                    distanceCheckForJump,
                    Globals.ENV_MASK);

                if (hit.collider == null && hit2.collider == null)
                {
                    _engine.fallFast = true;
                    
                }
            }
        }

        private IEnumerator DelayWallJump()
        {
            yield return new WaitForSeconds(delayForWallJump);
            _engine.Jump();
        }
    }
}
