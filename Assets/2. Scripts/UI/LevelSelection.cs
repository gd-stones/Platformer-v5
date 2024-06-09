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
            PlayerPrefs.SetInt("LevelUnlocked", 1);
            int levelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");

            if (pointIndex < levelUnlocked)
            {
                unlocked = true;
            }
        }

        void Update()
        {
            UpdateLevelImage();
            UpdateLevelStatus();
        }

        void UpdateLevelStatus()
        {
            ////if the current lv is 5, the pre should be 4
            //int prevLevelNum = int.Parse(gameObject.name) - 1;

            //if (PlayerPrefs.GetInt("Lv" + prevLevelNum.ToString()) > 0)//If the firts level star is bigger than 0, second level can play
            //{
            //    unlocked = true;
            //}
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