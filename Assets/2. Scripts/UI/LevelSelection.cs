using UnityEngine;
using UnityEngine.UI;

namespace StonesGaming
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] bool unlocked;
        [SerializeField] Image unlockImage;
        [SerializeField] int pointIndex;
        [SerializeField] string levelName;

        void Start()
        {
            //PlayerPrefs.SetInt("LevelUnlocked", 1);
            int levelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");
            Debug.Log(levelUnlocked);

            if (pointIndex < levelUnlocked)
            {
                unlocked = true;
            }
        }

        void Update()
        {
            UpdateLevelImage();
        }

        void UpdateLevelImage()
        {
            if (!unlocked)
            {
                unlockImage.gameObject.SetActive(true);
            }
            else
            {
                unlockImage.gameObject.SetActive(false);
            }
        }

        public void PressSelection()
        {
            if (unlocked)
            {
                Pointer.pointIndex = pointIndex;
                Globals.LevelName = levelName;
            }
        }
    }
}