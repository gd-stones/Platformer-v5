using UnityEngine;

namespace StonesGaming
{
    public class LadderBody : SpriteDebug
    {
        public bool enableRestrictedArea = true;
        public SpriteRenderer restrictedArea = null;
        public bool disableRestrictedTop = true;
        public float topHeight = 0;
        public float bottomHeight = 0;

        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                // some games could want to enable auto bottom/top detection based on
                // restricted area... restrictedArea ? restrictedArea.bounds : _sprite.bounds
                engine.LadderAreaEnter(_sprite.bounds, topHeight, bottomHeight);

                if (enableRestrictedArea)
                {
                    engine.SetRestrictedArea(restrictedArea.bounds, disableRestrictedTop);
                }
                else
                {
                    engine.ClearRestrictedArea();
                }
            }
        }

        public override void OnTriggerStay2D(Collider2D other)
        {
            base.OnTriggerStay2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                engine.LadderAreaEnter(_sprite.bounds, topHeight, bottomHeight);
            }
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);

            PlatformerEngine engine = other.GetComponent<PlatformerEngine>();
            if (engine)
            {
                engine.FreedomAreaExit();
                if (enableRestrictedArea)
                {
                    engine.DisableRestrictedArea();
                    engine.ClearRestrictedArea();
                }
            }
        }
    }
}
