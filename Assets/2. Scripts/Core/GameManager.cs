using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public FirebaseManager firebaseManager;
    private float startTime;
    private bool isPlaying;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        startTime = Time.time;
        isPlaying = true;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            isPlaying = false;
            SavePlayTime();
        }
        else
        {
            startTime = Time.time;
            isPlaying = true;
        }
    }

    void OnApplicationQuit()
    {
        isPlaying = false;
        SavePlayTime();
    }

    private void SavePlayTime()
    {
        float playTime = Time.time - startTime;
        string userId = SystemInfo.deviceUniqueIdentifier;
        firebaseManager.SavePlayTime(userId, Mathf.FloorToInt(playTime));
    }
}