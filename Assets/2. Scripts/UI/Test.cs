using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Get the default value : " + PlayerPrefs.GetInt("NEVERCreateThisKeyBefore"));
    }
}
