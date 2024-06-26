using UnityEngine;

namespace StonesGaming
{
    public class SpriteDebug : MonoBehaviour
    {
        public bool debug = false;
        public Color triggerColor;

        protected SpriteRenderer _sprite;
        private Color _originalColor;

        public virtual void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();

            if (!debug)
            {
                _sprite.enabled = false;
            }
            else
            {
                _originalColor = _sprite.color;
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (debug)
            {
                _sprite.color = triggerColor;
            }
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (debug)
            {
                _sprite.color = _originalColor;
            }
        }
    }
}
