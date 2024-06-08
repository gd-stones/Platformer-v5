using UnityEngine;
using System.Collections;

namespace StonesGaming
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        [SerializeField] SpriteRenderer gate;
        [SerializeField] Sprite gateOn;
        [SerializeField] Transform destination;

        bool canMove = false;

        private void Update()
        {
            if (boss.activeInHierarchy == false)
            {
                gate.sprite = gateOn;
                canMove = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && canMove)
            {
                StartCoroutine(Globals.SetCameraFade(collision.transform, destination.position));
            }
        }
    }
}