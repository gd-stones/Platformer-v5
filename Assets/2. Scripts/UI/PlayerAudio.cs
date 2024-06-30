using UnityEngine;

// Used in adding events to animations
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip runClip;

    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void AttackSound()
    {
        audioSource.PlayOneShot(attackClip);
    }

    private void JumpSound()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    private void HitSound()
    {
        audioSource.PlayOneShot(hitClip);
    }

    private void DashSound()
    {
        audioSource.PlayOneShot(dashClip);
    }

    private void RunSound()
    {
        audioSource.PlayOneShot(runClip);
    }
}
