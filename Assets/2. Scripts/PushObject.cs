using UnityEngine;

namespace StonesGaming
{
    public class PushObject : MonoBehaviour
    {
        public Transform barrel;
        Vector3 deltaPos;

        private void Awake()
        {
            deltaPos = transform.position - barrel.position;
        }

        void Update()
        {
            transform.position = barrel.position + deltaPos;
        }
    }
}