using UnityEngine;

namespace StonesGaming
{
    public class UIManager : MonoBehaviour
    {
        int level;

        void Update()
        {
            level = PlayerPrefs.GetInt("LevelUnlocked");

            if (level < Pointer.pointIndex)
            {
                PlayerPrefs.SetInt("LevelUnlocked", Pointer.pointIndex);
            }
        }
    }
}