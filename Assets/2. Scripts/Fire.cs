using UnityEngine;
using System.Collections;

namespace StonesGaming
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] float moveDistance = 8f;
        [SerializeField] float moveDuration = 3f;

        Vector3 startPosition;
        Vector3 endPosition;
        Vector3 velocity = Vector3.zero;

        [SerializeField] GameObject ownedBy;

        void OnEnable()
        {
            startPosition = transform.position;

            if (ownedBy.CompareTag("Player"))
            {
                if (Globals.AttackDirection)
                {
                    endPosition = startPosition + new Vector3(moveDistance, 0, 0);
                }
                else
                {
                    endPosition = startPosition + new Vector3(moveDistance, 0, 0) * -1f;
                }
            }
            else if (ownedBy.CompareTag("Boss"))
            {
                if (BossAI.theScale.x == 1)
                {
                    endPosition = startPosition + new Vector3(moveDistance, 0, 0);
                }
                else
                {
                    endPosition = startPosition + new Vector3(moveDistance, 0, 0) * -1f;
                }
            }
        }

        void Update()
        {
            StartCoroutine(MoveOverTime(startPosition, endPosition, moveDuration));
        }

        IEnumerator MoveOverTime(Vector3 start, Vector3 end, float duration)
        {
            float elapsedTime = 0;

            while (elapsedTime < 3)
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
