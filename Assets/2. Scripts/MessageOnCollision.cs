using UnityEngine;

namespace StonesGaming
{
    public class MessageOnCollision : MonoBehaviour
    {
        public Color triggerColor;
        private Color _originalColor;

        void Start()
        {
            _originalColor = GetComponent<SpriteRenderer>().color;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            GetComponent<SpriteRenderer>().color = triggerColor;
            Debug.Log(gameObject.name + " OnTriggerEnter with " + other.name);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            GetComponent<SpriteRenderer>().color = triggerColor;
            Debug.Log(gameObject.name + " OnTriggerStay with " + other.name);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            GetComponent<SpriteRenderer>().color = _originalColor;
            Debug.Log(gameObject.name + " OnTriggerExit with " + other.name);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(gameObject.name + " OnCollisionEnter with " + other.collider.name);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            Debug.Log(gameObject.name + " OnCollisionStay with " + other.collider.name);
        }

        void OnCollisionExit2D(Collision2D other)
        {
            Debug.Log(gameObject.name + " OnCollisionExit with " + other.collider.name);
        }
    }
}