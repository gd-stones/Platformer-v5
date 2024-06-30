using UnityEngine;
using UnityEngine.UI;

namespace StonesGaming
{
    public class DashCooldown : MonoBehaviour
    {
        [SerializeField] Image imageCooldown;
        bool isCooldown = false;
        [SerializeField] float cooldownTime = 2f;
        float cooldownTimer = 0f;
        public bool dash;

        void Start()
        {
            imageCooldown.fillAmount = 0f;
        }

        void Update()
        {
            if (isCooldown)
            {
                ApplyCooldown();
            }
        }

        void ApplyCooldown()
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer < 0f)
            {
                isCooldown = false;
                imageCooldown.fillAmount = 0f;
            }
            else
            {
                imageCooldown.fillAmount = cooldownTimer / cooldownTime;
            }
        }

        public void Dash()
        {
            if (isCooldown) return;

            dash = true;
            isCooldown = true;
            cooldownTimer = cooldownTime;
        }
    }
}