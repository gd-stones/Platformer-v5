using UnityEngine;
using Firebase.Analytics;
using UnityEngine.SceneManagement;

public class LevelLoggingBehaviour : MonoBehaviour
{
    void Start()
    {
        string levelName = SceneManager.GetActiveScene().name;
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, new Parameter("level_name", levelName));
    }

    void OnDestroy()
    {
        string levelName = SceneManager.GetActiveScene().name;
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, new Parameter("level_name", levelName));
    }
}
