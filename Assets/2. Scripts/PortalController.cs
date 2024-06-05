using System.Collections;
using UnityEngine;

namespace StonesGaming
{
    public class PortalController : MonoBehaviour
    {
        public Transform destination;
        public GameObject player;
        public Camera mainCamera;
        public GameObject vfx;

        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            vfx.SetActive(false);
        }

        void Update()
        {
            if (IsObjectInCameraView())
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

        bool IsObjectInCameraView()
        {
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
            return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
    }
}