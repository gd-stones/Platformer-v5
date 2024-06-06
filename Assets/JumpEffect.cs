using System.Collections;
using UnityEngine;

namespace StonesGaming
{
    public class JumpEffect : MonoBehaviour
    {
        void OnEnable()
        {
            StartCoroutine(DeactiveEffectJump());
        }

        IEnumerator DeactiveEffectJump()
        {
            yield return new WaitForSeconds(1f);
            SimplePool.Despawn(gameObject);
        }
    }
}