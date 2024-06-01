using UnityEngine;

namespace StonesGaming
{
    public class SmoothCameraFollow : MonoBehaviour
    {
        [SerializeField] Vector3 offset;
        [SerializeField] float damping;
        [SerializeField] Transform target;
        Vector3 cameraVelocity = Vector3.zero;

        void FixedUpdate()
        {
            //if (Player.IsMovementCamera)
            //{
                Vector3 targetPosition = new Vector3(target.position.x + offset.x, offset.y, target.position.z + offset.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, damping);
            //}
        }
    }
}