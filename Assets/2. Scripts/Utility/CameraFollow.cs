using UnityEngine;

namespace  StonesGaming
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        
        void Update()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;

            transform.position = pos;
        }
    }
}
