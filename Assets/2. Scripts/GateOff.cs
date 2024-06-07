using UnityEngine;

namespace StonesGaming
{
    public class GateOff : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        [SerializeField] GameObject gateOn;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (boss.activeInHierarchy == false)
                {
                    gateOn.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}