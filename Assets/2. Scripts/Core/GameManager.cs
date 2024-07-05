using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public FirebaseManager firebaseManager;
    private float playTime;
    private bool isPlaying;

    void Start()
    {
        playTime = 0;
        isPlaying = true;
        StartCoroutine(TrackPlayTime());
    }

    private IEnumerator TrackPlayTime()
    {
        while (isPlaying)
        {
            playTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnApplicationQuit()
    {
        isPlaying = false;
        string userId = SystemInfo.deviceUniqueIdentifier;
        firebaseManager.SavePlayTime(userId, Mathf.FloorToInt(playTime));
    }
}
