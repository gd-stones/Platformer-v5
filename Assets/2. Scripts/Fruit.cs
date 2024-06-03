using UnityEngine;

namespace StonesGaming
{
	public class Fruit : MonoBehaviour
	{
		public GameObject collectedPrefab;
		public AudioClip collectedClip;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.name.Contains("Player") || other.CompareTag("Player"))
			{
				var collected = Instantiate
				(
					collectedPrefab,
					transform.position,
					Quaternion.identity
				);

				var animator = collected.GetComponent<Animator>();
				var info = animator.GetCurrentAnimatorStateInfo(0);
				var time = info.length;

				Destroy(collected, time);
				Destroy(gameObject);
				var audioSource = FindObjectOfType<AudioSource>();
				audioSource.PlayOneShot(collectedClip);
			}
		}
	}
}