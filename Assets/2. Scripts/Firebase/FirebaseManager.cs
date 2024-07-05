using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                reference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Exception);
            }
        });
    }

    public void SavePlayTime(string userId, int playTime)
    {
        reference.Child("users").Child(userId).Child("playTime").SetValueAsync(playTime).ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                Debug.Log("Play time saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save play time: " + task.Exception);
            }
        });
    }
}