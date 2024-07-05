using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;         
    public string levelMessage;   
    public float displayTime = 3f; 

    void Start()
    {
        if (levelText != null)
        {
            levelText.text = levelMessage;
            StartCoroutine(HideLevelTextAfterDelay(displayTime));
        }
    }

    private IEnumerator HideLevelTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (levelText != null)
        {
            levelText.gameObject.SetActive(false);
        }
    }
}
