using UnityEngine;

namespace StonesGaming
{
    public class ResetFlag : MonoBehaviour
    {
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