using System.Collections;
using UnityEngine;

namespace StonesGaming
{
    public class PortalController : MonoBehaviour
    {
        public Transform destination;
        public GameObject player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (Vector2.Distance(player.transform.position, transform.position) > 0.5f)
                {
                    StartCoroutine(PortalIn());
                }
            }
        }

        IEnumerator PortalIn()
        {
            Globals.TeleportFlag = true;
            Globals.PortalIn = true;

            yield return new WaitForSeconds(0.5f);
            player.transform.position = destination.position;
            Globals.PortalIn = false;
        }
    }
}