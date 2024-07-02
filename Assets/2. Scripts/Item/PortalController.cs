using System.Collections;
using UnityEngine;

namespace StonesGaming
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField] Transform destination;
        [SerializeField] GameObject vfx;
        [SerializeField] BoxCollider2D boxCollider;

        void Start()
        {
            vfx.SetActive(false);
        }

        void Update()
        {
            if (Globals.IsObjectInCameraView(boxCollider))
            {
                vfx.SetActive(true);
            }
            else
            {
                vfx.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameObject player = collision.gameObject;

                if (Vector2.Distance(player.transform.position, transform.position) > 0.5f)
                {
                    StartCoroutine(PortalIn(player));
                }
            }
        }

        IEnumerator PortalIn(GameObject player)
        {
            var engineCustomize = player.GetComponent<PlatformerCustomize>();
            engineCustomize.SetStateEngineCustomize(PlatformerCustomize.EngineCState.Teleport);

            yield return new WaitForSeconds(0.5f);
            player.transform.position = destination.position;
            engineCustomize.ResetStateEngineCustomize();
        }
    }
}