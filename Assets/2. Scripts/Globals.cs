using UnityEngine;

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
    }
}
