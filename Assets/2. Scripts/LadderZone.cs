using UnityEngine;

namespace StonesGaming
{
    public class LadderZone : SpriteDebug
    {
        public PlatformerEngine.LadderZone zone;

        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                engine.SetLadderZone(zone);
            }
        }

        public override void OnTriggerStay2D(Collider2D other)
        {
            base.OnTriggerStay2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                engine.SetLadderZone(zone);
            }
        }

    }
}
