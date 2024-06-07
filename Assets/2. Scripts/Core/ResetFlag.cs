using UnityEngine;

namespace StonesGaming
{
    public class ResetFlag : MonoBehaviour
    {
        public void ResetHurtFlag()
        {
            Globals.HurtFlag = false;
        }
    }
}