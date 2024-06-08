using UnityEngine;

namespace StonesGaming
{
    public class ResetFlag : MonoBehaviour
    {
        [SerializeField] GameObject dashEffect;
        [SerializeField] GameObject slideEffect;

        public void SetactiveDash()
        {
            dashEffect.SetActive(true);
        }

        public void DeactiveDash()
        {
            dashEffect.SetActive(false);
        }

        public void SetactiveSlide()
        {
            slideEffect.SetActive(true);
        }

        public void DeactiveSlide()
        {
            slideEffect.SetActive(false);
        }

        public void ResetHurtFlag()
        {
            Globals.HurtFlag = false;
        }

        public void ResetAttackFlag()
        {
            Globals.AttackFlag = false;
        }

        public void ResetLadderFlag()
        {
            Globals.LadderFlag = false;
        }

        public void ResetLadderFlagAndHighJumpFlag()
        {
            Globals.LadderFlag = false;
            Globals.HighJumpFlag = false;
        }

        public void ResetTeleportFlag()
        {
            Globals.TeleportFlag = false;
        }
    }
}