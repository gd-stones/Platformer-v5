using UnityEngine;

namespace StonesGaming
{
    public class RestrictedArea : SpriteDebug
    {
        public enum TriggerAction
        {
            DoNothing,
            EnableRestrictedArea,
            EnableRestrictedAreaIfFreemode,
            DisableRestrictedArea,
            DisableRestrictedAreaIfFreemode
        }

        public TriggerAction RestrictedAreaOnEnter = TriggerAction.DoNothing;
        public TriggerAction RestrictedAreaOnExit = TriggerAction.DoNothing;
        public TriggerAction RestrictedAreaOnStay = TriggerAction.DoNothing;

        public bool exitFreeModeOnEnter;
        public bool exitFreeModeOnExit;

        public void DoAction(PlatformerEngine engine, TriggerAction action, bool exitFreeMode)
        {
            switch (action)
            {
                case TriggerAction.EnableRestrictedAreaIfFreemode:
                    if (engine.engineState == PlatformerEngine.EngineState.FreedomState)
                    {
                        engine.EnableRestrictedArea();
                    }
                    break;
                case TriggerAction.EnableRestrictedArea:
                    engine.EnableRestrictedArea();
                    break;
                case TriggerAction.DisableRestrictedAreaIfFreemode:
                    if (engine.engineState == PlatformerEngine.EngineState.FreedomState)
                    {
                        engine.DisableRestrictedArea();
                    }
                    break;
                case TriggerAction.DisableRestrictedArea:
                    engine.DisableRestrictedArea();
                    break;
            }

            if (exitFreeMode)
            {
                if (engine.engineState == PlatformerEngine.EngineState.FreedomState)
                {
                    engine.FreedomStateExit();
                }
            }
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                DoAction(engine, RestrictedAreaOnEnter, exitFreeModeOnEnter);
            }
        }

        public override void OnTriggerStay2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                DoAction(engine, RestrictedAreaOnStay, false);
            }
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                DoAction(engine, RestrictedAreaOnExit, exitFreeModeOnExit);
            }
        }
    }
}
