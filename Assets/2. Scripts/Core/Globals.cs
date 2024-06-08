using UnityEngine;
using System.Collections;

namespace StonesGaming
{
    public class Input
    {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        public const string JUMP = "Jump";
        public const string DASH = "Fire1";
    }

    public class Globals
    {
        // Input threshold in order to take effect. Arbitarily set.
        public const float INPUT_THRESHOLD = 0.5f;
        public const float FAST_FALL_THRESHOLD = 0.5f;

        public const int ENV_MASK = 0x100;
        public const string PACKAGE_NAME = "StonesGaming";
        public const float MINIMUM_DISTANCE_CHECK = 0.01f;

        public static bool LadderFlag = false;
        public static bool HighJumpFlag = false;
        public static bool PushFlag = false;
        public static bool TeleportFlag = false;
        public static bool PortalIn = true;

        public static bool AttackDirection = true; // true <=> right; false <=> left
        public static bool AttackFlag = false;

        public static bool CameraFadeFlag = false;

        public static Vector3 Checkpoint = Vector3.zero;

        public static bool HurtFlag = false;

        public static int GetFrameCount(float time)
        {
            float frames = time / Time.fixedDeltaTime;
            int roundedFrames = Mathf.RoundToInt(frames);

            if (Mathf.Approximately(frames, roundedFrames))
            {
                return roundedFrames;
            }

            return Mathf.CeilToInt(frames);
        }

        public static bool IsObjectInCameraView(Collider2D boxCollider)
        {
            if (boxCollider == null)
            {
                Debug.LogWarning("No BoxCollider2D found on " + boxCollider.gameObject.name);
                return false;
            }

            Vector3[] corners = new Vector3[4];
            corners[0] = boxCollider.bounds.min; // Bottom-left
            corners[1] = new Vector3(boxCollider.bounds.min.x, boxCollider.bounds.max.y, boxCollider.bounds.min.z); // Top-left
            corners[2] = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y, boxCollider.bounds.min.z); // Bottom-right
            corners[3] = boxCollider.bounds.max; // Top-right

            foreach (var corner in corners)
            {
                Vector3 screenPoint = Camera.main.WorldToViewportPoint(corner);

                if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerator SetCameraFade(Transform playerTransform, Vector3 destination)
        {
            yield return new WaitForSeconds(0.5f);
            Globals.CameraFadeFlag = true;

            yield return new WaitForSeconds(0.5f);
            playerTransform.position = destination;

            yield return new WaitForSeconds(1f);
            Globals.CameraFadeFlag = true;
        }
    }
}
