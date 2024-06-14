using UnityEngine;

namespace StonesGaming
{
    public class RightTouchControl : MonoBehaviour
    {
        public bool isInside = false;
        public bool jump;
        public bool attack;
        [SerializeField] RectTransform trackingArea;
        
        Vector2 startMousePosition;
        Vector2 startTouchPosition;

        private void Update()
        {
            MouseInput();
            TouchInput();
        }

        void MouseInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                startMousePosition = UnityEngine.Input.mousePosition;
                Vector2 mousePosition = RectTransformUtility.WorldToScreenPoint(null, startMousePosition);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(trackingArea, mousePosition, null, out Vector2 localPoint);

                if (trackingArea.rect.Contains(localPoint))
                {
                    Debug.Log("Mouse clicked inside the tracking area");
                    isInside = true;
                }
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                Vector2 mousePosition = RectTransformUtility.WorldToScreenPoint(null, UnityEngine.Input.mousePosition);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(trackingArea, mousePosition, null, out Vector2 localPoint);
                
                if (trackingArea.rect.Contains(localPoint))
                {
                    float distance = Vector2.Distance(startMousePosition, UnityEngine.Input.mousePosition);
                    if (distance > 0.5f)
                    {
                        Debug.Log("Mouse is being held and moved inside the tracking area");
                        jump = true;
                    }
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                Vector2 endMousePosition = UnityEngine.Input.mousePosition;
                Vector2 mousePosition = RectTransformUtility.WorldToScreenPoint(null, endMousePosition);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(trackingArea, mousePosition, null, out Vector2 localPoint);
                
                if (isInside)
                {
                    float distance = Vector2.Distance(startMousePosition, endMousePosition);
                    if (distance <= 0.5f)
                    {
                        Debug.Log("attackingggg");
                        attack = true;
                    }
                    isInside = false;
                }
            }
        }

        void TouchInput()
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(trackingArea, touch.position, null, out Vector2 localPoint);
                bool currentlyInside = trackingArea.rect.Contains(localPoint);

                if (currentlyInside && touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Touch entered the tracking area");
                    isInside = true;
                    startTouchPosition = touch.position;
                }

                if (currentlyInside && touch.phase == TouchPhase.Moved)
                {
                    float distance = Vector2.Distance(startTouchPosition, touch.position);
                    if (distance > 0.5f)
                    {
                        Debug.Log("Touch is moving inside the tracking area");
                        jump = true;
                    }
                }

                if (currentlyInside && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    float distance = Vector2.Distance(startTouchPosition, touch.position);
                    if (distance <= 0.5f)
                    {
                        Debug.Log("attackingggg");
                        attack = true;
                    }
                    isInside = false;
                    Debug.Log("Touch exited the tracking area");
                }
            }
        }
    }
}