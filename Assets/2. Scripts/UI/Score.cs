using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StonesGaming
{
    public class Score : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        private void Update()
        {
            scoreText.text = Globals.Score.ToString();
        }
    }
}