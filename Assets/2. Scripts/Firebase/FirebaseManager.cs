using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SavePlayTime(string userId, int playTime)
    {
        reference.Child("users").Child(userId).Child("playTime").SetValueAsync(playTime);
    }
}
