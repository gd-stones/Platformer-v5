using UnityEngine;
using System.Collections;

namespace StonesGaming
{
    public class Fire : MonoBehaviour
    {
        public float moveDistance = 8f;
        public float moveDuration = 3f;

        Vector3 startPosition;
        Vector3 endPosition;
        Vector3 velocity = Vector3.zero;

        private void OnEnable()
        {
            startPosition = transform.position;

            if (Globals.AttackDirection)
            {
                endPosition = startPosition + new Vector3(moveDistance, 0, 0);
            }
            else
            {
                endPosition = startPosition + new Vector3(moveDistance, 0, 0) * -1f;
            }
        }

        void Update()
        {
            StartCoroutine(MoveOverTime(startPosition, endPosition, moveDuration));
        }

        IEnumerator MoveOverTime(Vector3 start, Vector3 end, float duration)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.SmoothDamp(transform.position, end, ref velocity, duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = end;
            yield return new WaitForSeconds(0.1f);
            SimplePool.Despawn(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                SimplePool.Despawn(gameObject);
            }
        }
    }
}
