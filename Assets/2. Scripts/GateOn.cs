using System.Collections;
using UnityEngine;

namespace StonesGaming
{
    public class GateOn : MonoBehaviour
    {
        [SerializeField] Transform destination;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Transform playerTransform = collision.transform;

                StartCoroutine(SetCameraFade(playerTransform));
            }
        }

        IEnumerator SetCameraFade(Transform playerTransform)
        {
            yield return new WaitForSeconds(0.5f);
            Globals.CameraFadeFlag = true;
            
            yield return new WaitForSeconds(0.5f);
            playerTransform.position = destination.position;
            
            yield return new WaitForSeconds(1f);
            Globals.CameraFadeFlag = true;
        }
    }
}