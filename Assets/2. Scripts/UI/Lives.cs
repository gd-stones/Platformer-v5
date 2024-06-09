using UnityEngine;

namespace StonesGaming
{
    public class Lives : MonoBehaviour
    {
        [SerializeField] GameObject[] imagesHeart;
        int lives;

        private void Update()
        {
            if (Globals.HurtFlag)
            {
                lives = PlatformerCustomize.health / 10;

                if (lives == 0)
                {
                    imagesHeart[0].SetActive(false);
                    imagesHeart[1].SetActive(false);
                    imagesHeart[2].SetActive(false);
                }
                else if (lives == 1)
                {
                    imagesHeart[0].SetActive(false);
                    imagesHeart[1].SetActive(false);
                    imagesHeart[2].SetActive(true);
                }
                else if (lives == 2)
                {
                    imagesHeart[0].SetActive(false);
                    imagesHeart[1].SetActive(true);
                    imagesHeart[2].SetActive(true);
                }
                else if (lives == 3)
                {
                    imagesHeart[0].SetActive(true);
                    imagesHeart[1].SetActive(true);
                    imagesHeart[2].SetActive(true);
                }
            }
        }
    }
}