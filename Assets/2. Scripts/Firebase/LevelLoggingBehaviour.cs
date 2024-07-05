using UnityEngine;
using Firebase.Analytics;

public class LevelLoggingBehaviour : MonoBehaviour
{
    void Start()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
    }

    private void OnDestroy()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
    }
}
