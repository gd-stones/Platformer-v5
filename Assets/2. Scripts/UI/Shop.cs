using UnityEngine;

namespace StonesGaming
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] GameObject shopPanel;

        public void ShopButton()
        {
            shopPanel.SetActive(true);
        }

        public void ChooseCharacter()
        {
            shopPanel.SetActive(false);
        }
    }
}