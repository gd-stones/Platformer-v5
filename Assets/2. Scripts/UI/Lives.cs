using UnityEngine;

namespace StonesGaming
{
    public class Lives : MonoBehaviour
    {
        [SerializeField] GameObject[] imagesHeart;
        [SerializeField] PlatformerCustomize engineCustomize;
        int lives;

        private void Update()
        {
            if (engineCustomize.IsHurt())
            {
                lives = engineCustomize.CalculateLives();

                for (int i = 0; i < imagesHeart.Length; i++)
                {
                    imagesHeart[i].SetActive(i < lives);
                }
            }
        }
    }
}