using UnityEngine;

namespace StonesGaming
{
    public class Pointer : MonoBehaviour
    {
        public static int pointIndex = 0;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Vector2[] points;

        void Update()
        {
            rectTransform.localPosition = points[pointIndex];
        }
    }
}